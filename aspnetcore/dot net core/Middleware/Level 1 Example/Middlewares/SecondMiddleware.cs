namespace Core.Features.Middlewares
{
    public class SecondMiddleware
    {
        private readonly RequestDelegate _next;
        public SecondMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Second MiddleWare: Before next()");
            await _next(context);
            Console.WriteLine("Second MiddleWare: After next()");
        }
    }
}
