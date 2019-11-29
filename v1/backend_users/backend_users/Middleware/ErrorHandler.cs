using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace backend_users.Middleware
{
    public class ErrorHandler
    {
        
        private readonly RequestDelegate _next;

        public ErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (exception is ArgumentException)
            {
                code = HttpStatusCode.BadRequest;
            }

            if (exception is NotImplementedException)
            {
                code = HttpStatusCode.NotImplemented;
            }

            if (exception is HttpNotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }

            var errorModel = new ErrorModel {Error = exception.Message + " " + exception.InnerException?.Message};
            
            var result = JsonConvert.SerializeObject(errorModel,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;
            return context.Response.WriteAsync(result);
        }
    }
}