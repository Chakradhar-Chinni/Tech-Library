/* 
 1. Users request api/auth end point to get JWT token by providing valid credentials. (AuthController.cs doesn't use [Authorize] attribute. 
        So, JWT middleware bypasses authentication)
 2. Users include the JWT token in the Authorization header as Bearer token when accessing protected endpoints like api/values.
 3. The middleware validates the token and sets the user context for the request. (program.cs)
 4. Program.cs - JWT middleware walk through
 5. RBAC: app.useAuthorization() middleware
    * At startup, ASP.NET Core uses **reflection** to read attributes like `[Authorize(Roles="Admin")]` and attach them as metadata to endpoints.
    * During a request, the `UseAuthorization()` middleware reads this cached metadata and compares it with the user’s claims from the validated JWT.
    * If the token lacks the required role, the middleware blocks the request with **403 Forbidden** before it reaches the controller.
6. app.UseHttpsRedirection()
    - browser sends http request, then server redirects to https endpoint, client resends https request
    - Man In Middle attack is prevented because attacker cannot decrypt https traffic
    - Netflix example - tested 
    - Error: Socket hung up - HttpRequest is sent using postman, .net app redirects to Https, but can't auto redirect postman to Https
7. Https encryption /Decryption
    - HTTPS encryption/decryption happens at the transport layer (TLS), not in your code.
    - The web server (Kestrel/IIS) automatically decrypts incoming data before it reaches the API.
    - Your controller receives already decrypted, safe, plain-text data to process.
8. Security features: MITM, CSRF, Injection attack
9. app.Run() 
     - called as terminal miiddleware, ends the middleware pipeline (anything written after it won't be executed)
     - launches kestrel and process incoming requests   
     - Kestrel is the internal web server for all ASP.NET Core apps, including on IIS or Azure
 10. Next: RBAC vs Policy-Based Auth (Comparison)
[Authorize(Roles = "Admin")]

builder.Services.AddAuthorization(options => //**new**
{
    options.AddPolicy("MustBeFromChicago", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("City","Chicago");
    });
});

explain these
11. refresh JWT login & workflow
- upload to github
12. integrate with frontend MVC app
13. integrate with Angular
14. Ask chatgpt to test JWT skill
15. refresh token mechanism
CQRS
16. JWT Lifecycle
    - store JWT on HttpOnly cookie - avoids XSS attack (alternatives like Sessions storage, local storage are vulneravle to XSS)
    - on Logout, delete the Httpcookie, so token is no longer accessible to user even if token is still valid
cont. from ChatGpt last response
 */
