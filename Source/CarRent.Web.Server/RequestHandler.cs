using System.Collections;
using System.Collections.Generic;
using System.Net;
using Database;

namespace CarRent.Web.Server
{
    public enum RequestType
    {
        Get,
        Create,
        Update,
        Delete,
        Default
    }
    
    public class Request
    {
        public RequestType Type;
        public string EntityName;
        public IDictionary<string, string> Arguments = new Dictionary<string, string>();

        private Request()
        {
        }
        
        public static Request FromHttpRequest(HttpListenerRequest httpRequest)
        {
            var url = httpRequest.Url;
            var path = url.AbsolutePath.ToLower().Substring(8);
            
            var apiRequest = new Request();

            if (path.StartsWith("/get/"))
            {
                apiRequest.Type = RequestType.Get;
                path = path.Substring(4);
            }
            else if (path.StartsWith("/create/"))
            {
                apiRequest.Type = RequestType.Create;
                path = path.Substring(6);
            }
            else if (path.StartsWith("/update/"))
            {
                apiRequest.Type = RequestType.Update;
                path = path.Substring(7);
            }
            else if (path.StartsWith("/delete/"))
            {
                apiRequest.Type = RequestType.Delete;
                path = path.Substring(7);
            }

            var nextSlash = path.IndexOf('/', 2);
            apiRequest.EntityName = path.Substring(2, nextSlash - 2);

            path = path.Substring(apiRequest.EntityName.Length + 3);
            foreach (var nextArgument in path.Split(';'))
            {
                var argumentParts = nextArgument.Split(":");
                apiRequest.Arguments[argumentParts[0]] = argumentParts[1];
            }

            return apiRequest;
        }
    }

    public interface ICreatable
    {
        void Create(string entityName, IDictionary<string, string> arguments);
    }

    public interface IReadable
    {
        IEnumerable Read(string entityName, IDictionary<string, string> arguments);
    }

    public interface IUpdateable
    {
        void Update(string entityName, IDictionary<string, string> arguments);
    }

    public interface IDeleteable
    {
        void Delete(string entityName, IDictionary<string, string> arguments);
    }

    public interface ICrudHandler : ICreatable, IReadable, IUpdateable, IDeleteable
    {
        
    }
    
    public class RequestHandler
    {
        public IDictionary<string, string> HandleRequest(Request request, ICrudHandler handler)
        {
            IDictionary<string, string> response = new Dictionary<string, string>();
            if (request.Type == RequestType.Create)
            {
                handler.Create(request.EntityName, request.Arguments);
                response["result"] = "success";
            }

            if (request.Type == RequestType.Get)
            {
                IEnumerable foundObjects = handler.Read(request.EntityName, request.Arguments);
                var objectArray = "[";
                var isFirst = true;
                foreach (var nextObject in foundObjects)
                {
                    if (isFirst)
                    {
                        objectArray += nextObject;
                        isFirst = false;
                    }
                    else
                    {
                        objectArray += "," + nextObject;
                    }
                }

                response["result"] = "success";
                response["selected"] = objectArray + "]";
            }

            if (request.Type == RequestType.Update)
            {
                handler.Update(request.EntityName, request.Arguments);
                response["result"] = "success";
            }

            if (request.Type == RequestType.Delete)
            {
                handler.Delete(request.EntityName, request.Arguments);
            }

            if (request.Type == RequestType.Default)
            {
                response["result"] = "failure";
                response["message"] = "Unknown request type";
            }
            
            return response;
        }
    }
}