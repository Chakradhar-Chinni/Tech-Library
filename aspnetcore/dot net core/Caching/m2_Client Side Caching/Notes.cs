- Good for static or public data
- Reduces bandwidth
- Hard Refresh ctrl+shift+R forces to hit server instead of using cached data
-[Response Attribute]
	- Duration - cache time in seconds
	- Location - Where it is cached (Any, Client, None)
	- No store - If true, caching is disabled
- When NOT to Use Response Caching
	- Endpoints with authorization (JWT, cookies, etc.). except for general or non-personalized data
	- Highly dynamic data
	- Sensitive data
	- Request-specific results
- Debugging:
	Send request in swagger > Dev Tools > Network Tab > click on item > Headers, Preview, Response, Initiatior, Timing
	  Response Headers: Cache control: private,max-age=120
- at client side cached data is available at : C:\Users\<username>\AppData\Local\Google\Chrome\User Data\Default\Cache
- defining memory limit is not allowed as browsers wont accept it
- eviction rules are not possible, only cache expiry time can be controlled




Pseudo code
 
Program.cs 
	builder.Services.AddResponseCaching();
	var app = builder.Build();
	app.UseResponseCaching();


ProductController.cs
	[ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)] // 60 seconds
	public IActionResult Get() => Ok(data);
