using System.Collections.Generic;
using System.Net;

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
    
    public class RequestHandler
    {
        public IDictionary<string, string> HandleRequest(Request request)
        {
            // TODO: Insert CRUD methods using Entity Framework here :)
            var response = new Dictionary<string, string>(request.Arguments);
            response["__type__"] = request.Type.ToString();
            response["__entity__"] = request.EntityName;
            return response;
        }
    }
}