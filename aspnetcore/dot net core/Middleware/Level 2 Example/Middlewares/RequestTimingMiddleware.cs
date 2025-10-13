using System.Diagnostics;

namespace Core.Features.Middlewares
{
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestTimingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            
            await _next(context);
            
            stopwatch.Stop();
            //var time = stopwatch.Elapsed;
            var timeconsumed = stopwatch.ElapsedMilliseconds;
            await context.Response.WriteAsync($"\nRequest took: {timeconsumed} ms");
        }
    }
}
