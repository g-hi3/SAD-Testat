using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Database;

namespace CarRent.Web.Server
{
   public class WebServer
   {
      private readonly HttpListener _listener = new HttpListener();
      private readonly Func<HttpListenerRequest, string> _responderMethod;

      public WebServer(IReadOnlyCollection<string> prefixes, Func<HttpListenerRequest, string> method)
      {
         if (!HttpListener.IsSupported)
            throw new NotSupportedException("HttpListener is not supported!");

         if (prefixes == null || prefixes.Count == 0)
            throw new ArgumentException("URI prefixes are required");

         if (method == null)
            throw new ArgumentException("responder method required");

         foreach (var nextPrefix in prefixes)
         {
            _listener.Prefixes.Add(nextPrefix);
         }

         _responderMethod = method;
         _listener.Start();
      }

      public WebServer(Func<HttpListenerRequest, string> method, params string[] prefixes)
         : this(prefixes, method)
      {
      }

      public void Run()
      {
         ThreadPool.QueueUserWorkItem(o =>
         {
            Console.WriteLine("WebServer started running.");
            try
            {
               while (_listener.IsListening)
               {
                  ThreadPool.QueueUserWorkItem(c =>
                  {
                     var ctx = c as HttpListenerContext;
                     try
                     {
                        if (ctx == null)
                           return;

                        var response = _responderMethod(ctx.Request);
                        var buf = Encoding.UTF8.GetBytes(response);
                        ctx.Response.ContentLength64 = buf.Length;
                        ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                     }
                     catch (Exception e)
                     {
                        Console.WriteLine("There was an exception during a request!");
                        Console.WriteLine("Source Exception is:");
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.StackTrace);
                     }
                     finally
                     {
                        ctx?.Response.OutputStream.Close();
                     }
                  }, _listener.GetContext());
               }
            }
            catch (Exception e)
            {
               Console.WriteLine("There was an exception during running the WebServer! Source exception is:");
               Console.WriteLine(e.Message);
               Console.WriteLine(e.StackTrace);
            }
         });
      }

      public void Stop()
      {
         _listener.Stop();
         _listener.Close();
      }

      public static void Main()
      {
         var webServer = new WebServer(HandleRequest, "http://localhost:8080/CarRent");
         webServer.Run();
         Console.ReadKey();
         webServer.Stop();
      }

      public static string HandleRequest(HttpListenerRequest httpRequest)
      {
         var relativePath = httpRequest.Url.ToString().Substring(29);
         Console.WriteLine($"Request received!: {relativePath}");
         var apiRequest = Request.FromHttpRequest(httpRequest);
         var requestHandler = new RequestHandler();
         IDictionary<string, string> response;
         using (var context = new CarRentDbContext())
         {
            var crudHandler = new CrudHandler(context);
            response = requestHandler.HandleRequest(apiRequest, crudHandler);
         }

         var stringParts = response.Select(nextProperty => $"\"{nextProperty.Key}\": \"{nextProperty.Value}\"").ToList();
         return "{" + string.Join(',', stringParts) + "}";
      }
   }
}