HTTP Request would send serialized data and receive the same as HTTPResponse
Serialized data can be in Json / XML / any other format



<h2>
REST API with Web API
1. Leverage HTTP Protocol
2. Each piece of data is available at unique location. For Example, /products ; /product(1)
3. HTTP Methods are mapped to actions. For example, Get,Post,PUT
4. HTTP Status Codes determine the outcome. 400, 204, 302
5. Response can also contain pointers on what to do next



<h2>
Hierarchy of Web API with front end
1. a solution file will contain web api .sln and front end .sln(may be razor pages)
2. Swagger is also called as OPEN API. Swagger provides SwaggerUI which acts as API documentation
3. use CORS to block unauthorized requests / cross-origin requests
4. [HttpGet] [HttpPost] use these attributes in ApiController
5. A Web API can be developed using Controller Pattern or Minimal API Pattern
6. gRPC : g Remore Procedure Call
  a. Standard like REST API
  b. RPC - its supported outside of .NET core too
  c. Contract First - gives list of methods the API has
  d. Focus on Performance
  e. Protocol BUffers
7. REST API responses are human readable, while gRPC are not as they use binaries which makes gRPC faster than REST
8. For internal org. prefer gRPC, for external prefer REST. a web api can contain gRPC and REST



<h2>
aspnet core web api fundamentals
tech stack: DAL: EF Core, Dependency Injection, securing API
course structure: web api fundamentails, web api deep dive, minimal APIs, async API
web api fundamentails: aspnet core topics, building API with in-memory data, adding EF Core, security/versioning/documenting/deployment



<h2>
aspnetcore bigpicture
aspnet core is cross-platform. Open source. supports cloud development
Approaches to build web API: MVC, Minimal API
.net core was later renamed to .net



<h2>
Create a new web api project using Visual Studio
Open VS > ASP.NET Core Web API > choose .net8 > Create
Intial Project structure: .sln will contain
 - appsettings.json has some defualt configurations for allowed hosts=all
 - controllers/ - has weatherforecastController.cs  with some dummy data. simply delete it
 - /WeatherForecast.cs - this is class file for Controller. Simply delete it
 - /CityInfo.API.http - used for testing API. prefer Postman to test APIs
 - program.cs is starting point of app. It uses toplevel statements. So namespace, main method are not required. 
    - Uses DI, maps controllers, uses Swagger UI
launchsettings.json : HTTP, HTTPS, IIS profiles are offered. Each profile has configuration for port number,URL
"launchBrowser": true  - inside launchsettings.json, change this to false, so browser doesn't open after starting application


  
<h2>
Running project using CLI
open cmd > go to project path and give the following commands
'dotnet run' by default uses HttpProfile
'dotnet run --launch-profile https' indicating https profile explicitly in cmd




<h2>
Request pipeline and middleware
1. Middleware Chain:
   - The pipeline is composed of middleware components. Middleware is like a checkpoint where requests pass through for processing.
   - Examples include routing, authentication, error handling, and logging.

2. Order Matters:
   - Middleware is executed in the order you define in your code. The sequence affects how requests and responses flow.
   - For example, if authentication middleware comes after routing, it won't protect the routes—it has to come before.

3. Request Flow:
   - When a request arrives, it moves through the pipeline. Each middleware can:
     - Process the request and pass it to the next middleware.
     - Short-circuit the pipeline (e.g., return an error response directly).

4. Response Flow:
   - After the endpoint is executed, the response travels back through the pipeline, allowing middleware to modify the outgoing response.

### Visualize the request pipeline
Think of the pipeline as a water slide:
1. The request enters at the top.
2. It flows through various turns (middleware).
3. At the bottom, an endpoint handles the request, and the response goes back up the slide.


  


 <h2>
 Middleware redirection to Hello World
- In Program.cs use the following code to re direct all HttpRequests to Hello World (useful for testing purposes)
 
var app = builder.Build(); // Build the app
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection(); //Http are re-directed to Https
//app.UseAuthorization();
//app.MapControllers(); //Maps Http Requests to controllers

app.Run(async (context) =>
{
    await context.Response.WriteAsync("Hello World!");
});
app.Run();




<h2>
Working with Enviroments - Development, Staging, UAT, Production or any
- In launchsettings.json, profiles would use  "ASPNETCORE_ENVIRONMENT": "Development"
- Change the above property to Production and notice Swagger won't run as Swagger is only for development regions (check if condition in program.cs)


  
<h1>
Creating API and returning resources
MVC Pattern, Returning Resources, API Interaction, Content Negotiation, Getting a file



<h2>
MVC Pattern
- A software development Pattern used in many languages
- promotes loose coupling, Improves separation of concerns, Improves testability, promotes reuse
Model: business logic, rules, app data
View: Showing data
COntroller; Maps Model with View

In context of Web API, consumer of API hits controller which in turn 




<h2>
Creating API End Point and Returning resources
Routing Docs: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-9.0
-Routing matches request URL to an action on Controller
app.UseRouting() - selects a end point
app.UseEndpoints() - executes the endpoint selected by app.UseRouting()

-Two types of Routing
  - Attribute based routing  (preferred for API developemnt)
  - Convention based routing


- Create an endpoint using [HttpGet] 
Code Explanation
- defining the route at method level
1. Manually create CitiesController.cs file under Controllers Folder
2. CitiesController class inherits ControllerBase from the package Microsoft.AspNetCore.Mvc 
3.GetCities() returns all the cities in Json format as return type is JsonResult
 a. JsonResult formats the data into jsonformat 
4.[HttpGet("/api/cities")] is api end point for all cities. https://localhost:7169/api/cities

using Microsoft.AspNetCore.Mvc;
namespace CityInfo.API.Controllers
{
    public class CitiesController : ControllerBase
    {
        [HttpGet("/api/cities")]
        public JsonResult GetCities()
        {
            return new JsonResult(
                new List<object>
                {
                  new{  id=1,Name="Newyork" },
                  new{  id=2,Name="Texas" }
                });
        }
    }
}

- Create the endpoint usign [ApiController]
Code Explanation
1. defining the route at Controller Level. So, multiple endpoints can be managed
2. using [ApiController] route is defined at Controller Level
3. GetCities() need not provide the endpoint as /api/cities. It would just say the the type of HttpMethod

using Microsoft.AspNetCore.Mvc;
namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    //[Route("api/[controller]")]
     // Cities would be replaced here as CitiesController is matching. THis approach is also preferred
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetCities()
        {
            return new JsonResult(
                new List<object>
                {
                  new{  id=1,Name="Newyork" },
                  new{  id=2,Name="Texas" }
                });
        }
    }
} 


- Inspect Chrome Network tab & understand Request Headers
 - learn more about chrome dev tools

Route: /Cities
Endpoint: GET /Cities



<h2> 
Testing APIs through Postman / Bruno
- prefer to test using these tools
- As Self signed certificate will be used during development, Postman,Bruno doesnot allow to execute the request
- settings > disable SSL . this step allows to exeucute localhost APIs. (in other words, its like development tools)



<h2>
Creating Data Models and mockdata class

Code Explanation
1. Create /Models folder
2. Create a CityDto model under Models/CityDto.cs
3. create /CitiesDataStore.cs and create mock data list using citydto

## Models/CityDto.cs
namespace CityInfo.API.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}

## Mock data class - /CitiesDataStore.cs
using CityInfo.API.Models;
namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id=1, 
                    Name="Newyork", 
                    Description="Newyork is a city in the USA with big park"
                },
                new CityDto()
                {
                    Id=2,
                    Name="Texas",
                    Description="Texas is a city in the USA"
                },
                new CityDto()
                {
                    Id=3,
                    Name="California",
                    Description="California is a city in the USA"
                }
            };
        }
    }
}

## Controllers/CitiesController.cs
using Microsoft.AspNetCore.Mvc;
namespace CityInfo.API.Controllers
{
    [ApiController]
    //[Route("api/cities")]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetCities()
        {
            return new JsonResult(CitiesDataStore.Current.Cities);
        }
    }
}




  
