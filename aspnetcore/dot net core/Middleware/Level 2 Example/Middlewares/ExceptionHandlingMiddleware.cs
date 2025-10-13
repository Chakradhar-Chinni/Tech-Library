using System.Net;
using System.Text.Json;

namespace Core.Features.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                Console.WriteLine("Exception Middleware before next()");
                await _next(context);
                Console.WriteLine("Exception Middleware after next()");
            }
            catch(Exception ex)
            {
              await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var errorResponse = new
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message,
                Path = context.Request.Path,
                Method = context.Request.Method,
                Timestamp = DateTime.UtcNow
            };

            var json = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                WriteIndented = true
            });
                                                            
            return context.Response.WriteAsync(json);
        }
    }
}
