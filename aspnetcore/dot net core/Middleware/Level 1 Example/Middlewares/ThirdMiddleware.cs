namespace Core.Features.Middlewares
{
    public class ThirdMiddleware
    {
        private readonly RequestDelegate _next;
        public ThirdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Third MiddleWare: Handling request (pipeline ends here)");
            if (context.Response.HasStarted == false)
            {
                await context.Response.WriteAsync("Hello from Third Middleware\n");
            }
            //await _next(context);
        }
    }
}
