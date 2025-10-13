namespace Core.Features.Middlewares
{
    public class FirstMiddleware
    {
        private readonly RequestDelegate _next;
        public FirstMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("First MiddleWare: Before next()");
            await _next(context);
            Console.WriteLine("First MiddleWare: After next()");
        }
    }
}
