namespace Core.Features.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        string _logfilepath = "Logs/Applogs.txt";
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var logMessage = $"{System.DateTime.Now}: {context.Request.Method} {context.Request.Path}\n";
            
            File.AppendAllText(_logfilepath, logMessage);
            
            await _next(context);
        }
    }
}
