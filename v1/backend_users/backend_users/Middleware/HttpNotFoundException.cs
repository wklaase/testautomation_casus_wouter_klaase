using System;
using Newtonsoft.Json.Linq;

namespace backend_users.Middleware
{
    public class HttpNotFoundException : Exception
    {        
        
        public string ContentType { get; set; } = @"text/plain";

        public HttpNotFoundException(string message) : base(message)
        {
        }

        public HttpNotFoundException(Exception inner) : this(inner.ToString()) { }

        public HttpNotFoundException(JObject errorObject) : this(errorObject.ToString())
        {
            this.ContentType = @"application/json";
        }
      
    }
}