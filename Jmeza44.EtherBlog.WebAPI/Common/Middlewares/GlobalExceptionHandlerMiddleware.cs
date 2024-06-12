using Jmeza44.EtherBlog.Application.Common.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace Jmeza44.EtherBlog.WebApi.Common.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await HandleNotFoundExceptionAsync(context, ex);
            }
        }

        private static Task HandleNotFoundExceptionAsync(HttpContext context, NotFoundException exception)
        {
            var response = new { message = "Resource not found.", details = exception.Message };
            var jsonResponse = JsonConvert.SerializeObject(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
