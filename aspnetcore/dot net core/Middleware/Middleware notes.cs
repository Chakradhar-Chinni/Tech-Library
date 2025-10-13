(1) 
  Request Pipeline: Client → Middleware1 → Middleware2 → Middleware3 → Response


(2)
app.Use() - calls next middleWare
app.Run() - doesn't call enxt, executes 

app.Use(async (context, next) => {
    Console.WriteLine("Before Middleware1");
    await next(); // Pass to next middleware
    Console.WriteLine("After Middleware1");
});

app.Run(async context => {
    Console.WriteLine("Terminal middleware");
    await context.Response.WriteAsync("Hello World!");
});


(3)
    Middlewares execute in the order they are defined
    light weight middlewares to be placed at top (cahcing / logging)
    heavy middlewares at the bottom
    

(4)
  | Concept                 | Description                                              |
| ----------------------- | -------------------------------------------------------- |
| `RequestDelegate`       | Delegate representing the next component in the pipeline |
| `HttpContext`           | Gives access to Request & Response data                  |
| `InvokeAsync()`         | The method executed per request                          |
| `await _next(context)`  | Passes control to the next middleware                    |
| **Short-circuiting**    | Skip `_next(context)` → stop pipeline early              |



(5) BUilt in middlewares
  | Middleware            | Purpose                      |
| --------------------- | ---------------------------- |
| `UseRouting`          | Matches request to endpoints |
| `UseAuthentication`   | Validates user identity      |
| `UseAuthorization`    | Enforces access control      |
| `UseCors`             | Enables CORS headers         |
| `UseStaticFiles`      | Serves static files          |
| `UseExceptionHandler` | Centralized error handling   |
| `UseHttpsRedirection` | Redirects HTTP → HTTPS       |


(6)
    Custom Middleware Use Cases

✅ Centralized logging
✅ Global error handling
✅ Request timing/performance tracking
✅ Response header customization
✅ Authentication tokens validation
✅ Rate limiting or IP blocking
✅ Localization and culture setting

(7)
    

  
