using System;
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
        private static readonly CarRentDbContext context = new CarRentDbContext();
        
        public IDictionary<string, string> HandleRequest(Request request)
        {
            string query = "";
            switch (request.Type)
            {
                case RequestType.Create:
                    query += "INSERT INTO " + request.EntityName;
                    break;
                
                case RequestType.Get:
                    query += "SELECT * FROM " + request.EntityName;
                    break;
                
                case RequestType.Update:
                    query += "UPDATE " + request.EntityName;
                    break;
                
                case RequestType.Delete:
                    query += "DELETE FROM " + request.EntityName;
                    break;
                
                case RequestType.Default:
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var isFirst = true;
            foreach (var nextArgument in request.Arguments)
            {
                if (nextArgument.Key.StartsWith("__"))
                    continue;

                if (isFirst)
                {
                    query += " WHERE ";
                    isFirst = false;
                }
                else
                {
                    query += " AND ";
                }

                query += nextArgument.Key + " = " + nextArgument.Value;
            }
            // should execute query here I guess.
            return new Dictionary<string, string>();
        }
    }
}