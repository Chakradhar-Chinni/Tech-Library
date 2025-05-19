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
4. CitiesDataStore.cs contains a static property with get method. It provides data globally to every class.
     - Its like a singleton patttern, provides single shared instance
Note: 
  - If constuctor of the class is private then an object cannot be instantiated. The static property approach will work
  - for a static class, object is not required as data is accessible using class name
  - Factory method can be used for controlled object creation, as it would have private constructor and return the object after creation

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



<h2> 
Add new API End Point

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
        
        [HttpGet("{id}")]  //new end point added
        public JsonResult GetCity(int id)
        {
            return new JsonResult(CitiesDataStore.Current.Cities.First(c=> c.Id == id));
        }
    }
}




<h2>
Changing Return type to ActionResult<IEnumerable<CityDto>> from Json Result
1. JsonResult always returns 200 ok with the data, even if the data is not found or redirections or others
2. JsonResult isn't suitable to handle errors and send error messages without extra work
3. ActionResult<> offers flexibility
4. ActionResult<T> is a generic class that combines result object with HTTP Status code
5. Clean Code: It provides helper methods like Ok() NotFound() BadRequest() etc...
6. Compatibility: Very helpful in REST API development as it can return data along with appropriate HttpStatus Code




<h2>
 Implementing HttpStatus Codes 
 
 1xx Information
 2xx Success
 3xx Redirection
 4xx Client Error
 5xx Server Error



using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
namespace CityInfo.API.Controllers
{
    [ApiController]
    //[Route("api/cities")]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CityDto>> GetCity(int id)
        {
            var cityToReturn = CitiesDataStore.Current.Cities.First(c => c.Id == id);

            if(cityToReturn == null)
            {
                return NotFound();
            }            
            
            return Ok(CitiesDataStore.Current.Cities.First(c=> c.Id == id));
        }
    }
}



<h2>
  Returning Child Resources - ActionResult<> to return cities/pointOfInterest ; cities/pointOfInterest/pointofinterestid

Code Explanation
1. Adding PointsOfInterest Collection to CityDto class
2. Create a model class  at /Models/PointsOfInterestDto.cs with Id,Name,Description
3. Update MockData file at /CitiesDataStore.cs to include PointsOfInterest mock data
5. Create controller at /Controller/PointsOfInterestController.cs to return data
    - route is modified at class level, so method can simply use [HttpGet] Attribute

## /Models/CityDto.cs
namespace CityInfo.API.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public int NumberOfPointsOfInterest
        {
            get
            {
                return PointsOfInterest.Count;
            }
        }

        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; } 
                    = new List<PointOfInterestDto>();       
    }
}

## /Models/PointOfInterestDto.cs
namespace CityInfo.API.Models
{
    public class PointOfInterestDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}

## /CitiesDataStore.cs (mock data)
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
                    Description="Newyork is a city in the USA with big park",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name= "Water park",
                            Description="A water park having many themes"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name= "Art Library",
                            Description="Art Library with dozens of paintings"
                        }
                    }
                },
                new CityDto()
                {
                    Id=2,
                    Name="Texas",
                    Description="Texas is a city in the USA",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name= "Central Park",
                            Description="Central Park is most visited place"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name= "Empire Building",
                            Description="Empire Building is most historic"
                        }
                    }
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


## /Controllers/PointOfInterestController.cs
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("/api/cities/{cityId}/pointofinterest")] //base route
    public class PointOfInterestController : ControllerBase
    {
        [HttpGet] //https://localhost:7167/api/cities/1/pointofinterest (returns all pointsofinterest in city 1
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                return NotFound();
            }
            return Ok(city.PointsOfInterest);
        }
    }

    [HttpGet("{pointofinterestid}")] //https://localhost:7167/api/cities/1/pointofinterest/1 (returns 1st point of interest in city 1)
    public ActionResult<IEnumerable<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointOfInterestid)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);    
        if(city == null)
        {
            return NotFound();
        }
    
        //find pointOfInterestId in above city
        var pointOfInterest = city.PointsOfInterest.FirstOrDefault( p => p.Id == pointOfInterestid);
    
        if (pointOfInterest == null)
        {
            return NotFound();
        }
        return Ok(pointOfInterest);    
    }
}



<h2>
Adding Customized Error messages in API errors
place the below code in program.cs file before building the app to show customized error messages
// Send Additional in error messages
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = ctx =>
    {
        ctx.ProblemDetails.Instance = ctx.HttpContext.Request.Path;
        ctx.ProblemDetails.Title = "Custom Error";
        ctx.ProblemDetails.Detail = "Custom error message";
        ctx.ProblemDetails.Extensions.Add("Process Id", Environment.ProcessId);
    };
});




<h2>
  Content Negotiation and Data Formatting
1. Request Header Accept: Application/Json will make the sure API returns data in Json format
  - Accept: Application/XML will still return data in Json format if code is not configured to return XML Data (unfair as client explicitly asked XML)
2. add configuration to return XML Data;
3. handle the code if a unsupported data format is requested

## Program.cs
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true; //return 406 if the client requests a format that is not supported
}).AddXmlDataContractSerializerFormatters()

URI: https://localhost:7167/api/cities/1
## Json Format
{
  "id": 1,
  "name": "Newyork",
  "description": "Newyork is a city in the USA with big park",
  "numberOfPointsOfInterest": 2,
  "pointsOfInterest": [
    {
      "id": 1,
      "name": "Water park",
      "description": "A water park having many themes"
    },
    {
      "id": 2,
      "name": "Art Library",
      "description": "Art Library with dozens of paintings"
    }
  ]
}

## XML Format
<CityDto xmlns:i="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://schemas.datacontract.org/2004/07/CityInfo.API.Models">
    <Description>Newyork is a city in the USA with big park</Description>
    <Id>1</Id>
    <Name>Newyork</Name>
    <PointsOfInterest>
        <PointOfInterestDto>
            <Description>A water park having many themes</Description>
            <Id>1</Id>
            <Name>Water park</Name>
        </PointOfInterestDto>
        <PointOfInterestDto>
            <Description>Art Library with dozens of paintings</Description>
            <Id>2</Id>
            <Name>Art Library</Name>
        </PointOfInterestDto>
    </PointsOfInterest>
</CityDto>





<h2>
Getting a File
1. Create FilesController.cs
2. Registering DI:
    FileExtensionContentTypeProvider is registered as a service into DI Container.
    Constructor of FilesController.cs has a parameter FileExtensionContentTypeProvider. ASP.NET Core automatically resolves this dependency from the DI container when creating an instance of FilesController.
    So, whenever FilesController is instantiated then .NET Core automatically provides an instance of this service to FilesController.cs
    
3. FilesController Constructor is injected with FileExtensionContentTypeProvider to determine MIME type (content type) of files based on their extensions.
   - If the provider is not supplied, an ArgumentNullException is thrown to ensure the dependency is not null.
4. GetFile() takes fileId as parameter and returns the hardcoded file from root directory
  - Uses NotFound() helper method to handle file not found scenario
  - TryGetContentType method of _fileExtensionContentTypeProvider is used to determine the MIME type of file
  - if TryGetContentType method couldn't determine the file type then default content type of "application/octet-stream" is used

## /Controllers/FilesController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider
                 ?? throw new System.ArgumentNullException(nameof(fileExtensionContentTypeProvider));
        }
        [HttpGet("{fileid}")]
        public ActionResult GetFile(string fileId)
        {
            var pathToFile = "FSD-MERN-Stack-brochure.pdf";
            if (!System.IO.File.Exists(pathToFile))
            {
                return NotFound();
            }
            if(!_fileExtensionContentTypeProvider.TryGetContentType(pathToFile, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var bytes = System.IO.File.ReadAllBytes(pathToFile);
            return File(bytes, contentType, Path.GetFileName(pathToFile));
        }
    }
}

##Program.cs
using Microsoft.AspNetCore.StaticFiles;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true; //return 406 if the client requests a format that is not supported
}).AddXmlDataContractSerializerFormatters();
builder.Services.AddEndpointsApiExplorer(); //endpoint discovery
builder.Services.AddSwaggerGen(); //integrates Swagger
builder.Services.AddSingleton<FileExtensionContentTypeProvider>(); //FileExtensionContentTypeProvider is used to get the content type of a file based on its extension

var app = builder.Build(); // Build the app

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseHttpsRedirection(); //Http are re-directed to Https
app.UseAuthorization();
app.MapControllers(); //Maps Http Requests to controllers
app.UseEndpoints(endpoints =>
  {
      endpoints.MapControllers();
  });
app.Run();




<h1>
CRUD Operations,Validation Input

1. Data retrieved through API may not be same as in database. because business rules can perform data transformation
2. Maintain separate DTOs for every operation like create, update, delete
3. Data can be passed to API via many routes, route, binding source, query parameters







<h2> Creating a POST Resource with request body

##/Controllers/PointOfInterestController.cs
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("/api/cities/{cityId}/pointofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if(city == null)
            {
                return NotFound();
            }
            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{pointofinterestid}", Name="GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestid)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                return NotFound();
            }
            //find pointOfInterestId in above city
            var pointOfInterest = city.PointsOfInterest.FirstOrDefault( p => p.Id == pointOfInterestid);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(pointOfInterest);
        }

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId,PointOfInterestForCreationDto pointOfInterest)
        {
            /*
                URI: https://localhost:7167/api/cities/1/pointofinterest
                Request Body:            
                  {
                    "fruit": "Green Museum",
                    "label": "Co greem museum"
                  }
                Headers: Content-Type : application/json
            */
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                return NotFound();
            }

            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);
            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterest);           

            //CreatedAtRoute is helper method of ControllerBase
            return CreatedAtRoute("GetPointOfInterest",
                new {
                     cityId = cityId, 
                     pointOfInterestId = finalPointOfInterest.Id 
                    },
                    finalPointOfInterest);
        }
    }
}

## /Models/PointOfInterestForCreationDto.cs
using Microsoft.AspNetCore.Mvc;
namespace CityInfo.API.Models
{
    public class PointOfInterestForCreationDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}





<h2> Validating Request Body
1. DataAnnotations can be used for validations.
2. PointOfInterestForCreationDto now has validation with [Required] [Maxlength()]
3. ModelState is a dictionary containing data
3. Controller class has commented code, which will ensure DataAnnotations are validated. 
4. [ApiController] attribute at top of class will provide the same functionality of ModelState. So ModelState block can be commented

## /Models/PointOfInterestForCreationDto.cs
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace CityInfo.API.Models
{
    public class PointOfInterestForCreationDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;       
        [MaxLength(10)]
        public string? Description { get; set; }
    }
}

## /Controllers/PointOfInterestController.cs
[ApiController]
[Route("/api/cities/{cityId}/pointofinterest")]

    [HttpPost]
    public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId,PointOfInterestForCreationDto pointOfInterest)
    {
      /* 
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }
      */
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if(city == null)
        {
            return NotFound();
        }
        var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);
        var finalPointOfInterest = new PointOfInterestDto()
        {
            Id = ++maxPointOfInterestId,
            Name = pointOfInterest.Name,
            Description = pointOfInterest.Description
        };

        city.PointsOfInterest.Add(finalPointOfInterest);           

        //helper method of ControllerBase
        return CreatedAtRoute("GetPointOfInterest",
            new {
                 cityId = cityId, 
                 pointOfInterestId = finalPointOfInterest.Id 
                },
                finalPointOfInterest);
    }
}





<h2> Updating Resource using PUT
1. As a general rule, create a new DTO for updating resource
2. Create ActionREsult method to update resources 
3. DTO class has marked Name field with [Required] Data annotation. So, in the request body Name is mandatory but Description is optional
  - by definition, [HTTPPut] is meant for updating entire resource.
  - if description is not provided in the request body, then default value of null will be assigned to description

/* API Data 
  GET:https://localhost:7167/api/cities/2
    {
      "id": 2,
      "name": "Texas",
      "description": "Texas is a city in the USA",
      "numberOfPointsOfInterest": 2,
      "pointsOfInterest": [
        {
          "id": 1,
          "name": "Central Park",
          "description": "Central Park is most visited place"
        },
        {
          "id": 2,
          "name": "Empire Building",
          "description": "Empire Building is most historic"
        }
      ]
    }
URI:  https://localhost:7167/api/cities/2/pointofinterest/1
Request Body: 
{
 "name":"Central Theme Park - updated name using HTTP PUT"  
}
-- After calling API with above request body, description is null as it is not mentioned in Request body
    {
      "id": 2,
      "name": "Texas",
      "description": "Texas is a city in the USA",
      "numberOfPointsOfInterest": 2,
      "pointsOfInterest": [
        {
          "id": 1,
          "name": "Central Theme Park - updated name using HTTP PUT",
          "description": null
        },
        {
          "id": 2,
          "name": "Empire Building",
          "description": "Empire Building is most historic"
        }
      ]
    }
*/

## /Models/PointOfInterestForUpdateDto.cs
using System.ComponentModel.DataAnnotations;
namespace CityInfo.API.Models
{
    public class PointOfInterestForUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;        
        public string? Description { get; set; }
    }
}


## /Controllers/PointOfInterestController.cs
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("/api/cities/{cityId}/pointofinterest")]
      [HttpPut("{pointofinterestId}")] //URI:  https://localhost:7167/api/cities/2/pointofinterest/1
      public ActionResult UpdatePointOfInterest(int cityId,int pointOfInterestId, PointOfInterestForUpdateDto pointOfInterest)
      {
          var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
          if (city == null)
          {
              return NotFound();
          }
      
          var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
          if(pointOfInterestFromStore==null)
          {
              return NotFound();
          }
      
          pointOfInterestFromStore.Name = pointOfInterest.Name;
          pointOfInterestFromStore.Description = pointOfInterest.Description;
    
          return NoContent();
      }
}






<h2> Updating Resource using PATCH (partial updates)
1. Install Nuget Packages Microsoft.AspNetCore.JsonPatch, Microsoft.AspNetCore.Mvc.NewtonsoftJson
2. add NewtonSoft controller to program.cs
3. ModelState will validate the available fields only. Even if Name is required it will pass the model because Name is not avaialble in the object. SO, it wont compare it
4. TryValidateModel(pointOfInterestToPatch) validates every thing. If a required field is missing it will fail the validation.


## Program.cs
      // Add services to the container.
      builder.Services.AddControllers(options =>
      {
          options.ReturnHttpNotAcceptable = true; //return 406 if the client requests a format that is not supported
      }).AddNewtonsoftJson()
         .AddXmlDataContractSerializerFormatters();


## /Models/PointOfInterestForUpdateDto.cs
using System.ComponentModel.DataAnnotations;
namespace CityInfo.API.Models
{
    public class PointOfInterestForUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;        
        public string? Description { get; set; }
    }
}


## /Controllers/PointofInterestController.cs
/*
  PATCH: https://localhost:7167/api/cities/1/pointofinterest/1
  Request Body
              [
                {
                  "op": "remove",
                  "path": "/name",
                  "value" : ""
                }
              ]
*/
namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("/api/cities/{cityId}/pointofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        [HttpPatch("{pointofinterestid}")]
        public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId,JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city==null)
            {
                return NotFound();
            }
        
            var pointOfInterestFromStore = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == pointOfInterestId);
            if(pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            //updatable object
            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };

            //apply patch to updatable object
            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);
          
            if(! ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //validating model after patching
            if(!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }
            
            //update data in store from updatable object
            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;
        
            return NoContent();
        }
    }
  }







<h2> Dependency Injection using ILogger
1. ILogger is built in service in .NET core, so registration is not required
2. Constructor Injection is preferred way of requesting dependencies. If thats not possible use HttpContext.RequestServices
3. Constructor of CitiesController is injected with _logger to use logging methods
4. private readonly ILogger<CitiesController> _logger; This declares a private, read-only field named _logger that holds an instance of ILogger<CitiesController>. The ILogger interface is used for logging messages, 
     helping developers track events, errors, or other information during runtime.
5. _logger = logger ?? throw new ArgumentNullException(nameof(logger));
  This assigns the logger parameter to the _logger field, but with a safeguard:
  - If logger is null, an ArgumentNullException is thrown.
  - nameof(logger) ensures that the exception message references the correct parameter name, improving debugging.
6. appsettings.json
    CitiesController default logging level is Information, so Information & above will be logged
    PointOfInterestController default logging level is Warning, so Warning above only will be logged
    Logging Levels (Lowest to Highest)
      └── 0 - Trace      - Most detailed logs for debugging
          ├── 1 - Debug      - Debugging-level information
          │   ├── 2 - Information - General application flow details
          │   │   ├── 3 - Warning     - Potential issues that may not cause failure
          │   │   │   ├── 4 - Error       - Issues that prevent normal execution
          │   │   │   │   ├── 5 - Critical    - Severe issues that may crash the application
          │   │   │   │   │   └── 6 - None        - Disables logging



## CitiesController.cs
namespace CityInfo.API.Controllers
{
    [ApiController]
    //[Route("api/cities")]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        public CitiesController(ILogger<CitiesController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            _logger.LogInformation("Cities are returned");
            return Ok(CitiesDataStore.Current.Cities);
        }
    }
}

##appsettings.jsom
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "CityInfo.API.Controllers.CitiesController": "Information",
      "CityInfo.API.Controllers.PointOfInterestController": "Warning"
    }
  },
  "AllowedHosts": "*"
}


##program.cs
builder.Logging.ClearProviders(); //disables log config
builder.Logging.AddConsole(); // enables config for console only







<h2> Logging Exceptions
1. return Statement in catchblock will send message to API consumer. So, don't send any implementation details

##CitiesController.cs
[HttpGet]
public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
{
    try
    {
        throw new Exception("Raised Exception");
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

        if(city == null)
        {
            return NotFound();
        }

        _logger.LogInformation("Points of interest are returned for city with id {cityId}", cityId);
        return Ok(city.PointsOfInterest);
    }
    catch(Exception ex)
    {
        _logger.LogCritical($"Exception Occured while getting points of interest for city with id {cityId}");
        return StatusCode(500, "A problem happened while handling your request");
    }
}

//Exception message on Console
crit: CityInfo.API.Controllers.PointOfInterestController[0]
      Exception Occured while getting points of interest for city with id 1




        
        
        
<h2> Globally Handling and Logging Exceptions

Instead of scattering try-catch blocks throughout the code, you can centralize exception handling using middleware.
summary: add builder.services.AddProgramDetails(); in program.cs to show the error messages in a format


lets raise a exception & see the error message of unhandled exception

## CitiesCOntroller.cs
[HttpGet]
public ActionResult<IEnumerable<CityDto>> GetCities()
{
    throw new Exception("Cities Exception");
    _logger.LogInformation("Cities are returned");
    return Ok(CitiesDataStore.Current.Cities);
}

#region Exception Message
 /*
Error: response status is 500
Response body
System.Exception: Cities Exception
   at CityInfo.API.Controllers.CitiesController.GetCities() in C:\dotnet\Pluralsight\CityInfo\CityInfo.API\Controllers\CitiesController.cs:line 20
   at lambda_method2(Closure, Object, Object[])
   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.SyncObjectResultExecutor.Execute(ActionContext actionContext, IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeActionMethodAsync()
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeNextActionFilterAsync()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)
   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)
   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context)

HEADERS
=======
Accept: text/plain
Host: localhost:7167
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/136.0.0.0 Safari/537.36
Accept-Encoding: gzip, deflate, br, zstd
Accept-Language: en-GB,en-US;q=0.9,en;q=0.8
Referer: https://localhost:7167/swagger/index.html
sec-ch-ua-platform: "Windows"
sec-ch-ua: "Chromium";v="136", "Google Chrome";v="136", "Not.A/Brand";v="99"
sec-ch-ua-mobile: ?0
sec-fetch-site: same-origin
sec-fetch-mode: cors
sec-fetch-dest: empty
priority: u=1, i
*/
#endregion

 the above exception is a trace message, which is required for developers only, consumers should never see it. THis can be handled using Exception Middleware in .NET core



##
handling exception with .AddProblemDetails() in program.cs
add ons: just add builder.services.AddProgramDetails(); in program.cs to show the error messages in a format



##Program.cs
using Microsoft.AspNetCore.StaticFiles;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true; //return 406 if the client requests a format that is not supported
}).AddNewtonsoftJson()
   .AddXmlDataContractSerializerFormatters();

builder.Services.AddProblemDetails();  /////////////////////////////////////this line is added

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); //endpoint discovery
builder.Services.AddSwaggerGen(); //integrates Swagger
builder.Services.AddSingleton<FileExtensionContentTypeProvider>(); //FileExtensionContentTypeProvider is used to get the content type of a file based on its extension

var app = builder.Build(); // Build the app

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler();
}

app.UseRouting();
app.UseHttpsRedirection(); //Http are re-directed to Https

app.UseAuthorization();

app.MapControllers(); //Maps Http Requests to controllers

app.UseEndpoints(endpoints =>
  {
      endpoints.MapControllers();
  });
app.Run();


Error Message:
/*
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.6.1",
  "title": "System.Exception",
  "status": 500,
  "detail": "Cities Exception",
  "traceId": "00-dddee0f567d8d026dd669a7918f63eea-2304645274d8a374-00",
  "exception": {
    "valueKind": 1
  }
}
*/






<h2> Setting Up Serilog 
1) .NET core logging provider doesnot support additional features like database logging, file logging. & linux containers dont support in-built logging provider. 
2) So, developers often prefer 3rd parties like nlog, serilog etc..
3) Setting up Serilog & removing default logging mechanism
4) Serilog.AspNetCore,  Serilog.Sinks.Console,  Serilog.Sinks.File

##Program.cs
using CityInfo.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/cityinfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
builder.Host.UseSerilog();

/*  
previous code would work as it is without changes
public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        public CitiesController(ILogger<CitiesController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    } */






<h2> Implemeting a Custom Mail Service and using it via Dependency Injection (concrete class implementation)
1. Create /Services/LocalMailService.cs
2. Register the new class in program.cs so, we just have to inject it in the dependency
3. Inject in Constructor of PointOfInterestController.cs and use it in any ActionREsult methods

##/Services/LocalMailService.cs
namespace CityInfo.API.Services
{
    public class LocalMailService
    {
        private string _mailTo = "admin@company.com";
        private string _mailFrom = "noreply@company.com";

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}

##program.cs
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
builder.Services.AddTransient<LocalMailService>();  //new line
var app = builder.Build(); // Build the app


## PointOfInterestController.cs
namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("/api/cities/{cityId}/pointofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ILogger<PointOfInterestController> _logger;
        private readonly LocalMailService _mailService;

        public PointOfInterestController(ILogger<PointOfInterestController> logger,LocalMailService mailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }
       
       [HttpDelete("{pointofinterestid}")]
       public ActionResult DeletePointOfInterest(int cityId,int pointofinterestid)
       {
           var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
           if(city == null)
           {
               return NotFound();
           }
      
           var pointofInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointofinterestid);
           if (pointofInterestFromStore == null)
           {
               return NotFound();
           }
      
           city.PointsOfInterest.Remove(pointofInterestFromStore);
      
           _mailService.Send("Point Of Interest deleted", $"{pointofInterestFromStore.Name} is deleted");
      
           return NoContent();
       }
 }






<h2> Registering a Service by Interface

In Previous module, concrete implementation is provided, now turning that to Interface to promote loose coupling
1. Using Interface for Send()
2. LocalMailService implements ILocalMailService
3. CloudMailService implements ILocalMailService
4. Program.cs - in Debug Mode LocalCloudService will be registered else CloudMailService will be registered 
5. In the Controller, Inject the ILocalMailService instead of Concrete implementation

## /Services/ILocalMailService.cs
namespace CityInfo.API.Services
{
    public interface ILocalMailService
    {
        void Send(string subject, string message);
    }
}


##/Services/LocalMailService.cs
namespace CityInfo.API.Services
{
    public class LocalMailService : ILocalMailService
    {
        private string _mailTo = "admin@company.com";
        private string _mailFrom = "noreply@company.com";

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from Local {_mailFrom} to {_mailTo}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}

##/Services/CloudMailService.cs
namespace CityInfo.API.Services
{
    public class CloudMailService : ILocalMailService
    {
        private string _mailTo = "admin@company.com";
        private string _mailFrom = "noreply@company.com";

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from Cloud {_mailFrom} to {_mailTo}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }

    }
}


##Program.cs
#if DEBUG
builder.Services.AddTransient<ILocalMailService, LocalMailService>();
#else
builder.Services.AddTransient<ILocalMailService, CloudMailService>();
#endif
var app = builder.Build(); // Build the app

## /Controllers/PointOfInterestController.cs
namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("/api/cities/{cityId}/pointofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ILogger<PointOfInterestController> _logger;
        private readonly ILocalMailService _mailService;

        public PointOfInterestController(ILogger<PointOfInterestController> logger,ILocalMailService mailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            try
            {
                //throw new Exception("Raised Exception");
                var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

                if(city == null)
                {
                    return NotFound();
                }

                _mailService.Send("Get Points of Interest", $"Points of interest are returned for city with id {cityId}");
                _logger.LogInformation("Points of interest are returned for city with id {cityId}", cityId);
                return Ok(city.PointsOfInterest);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception Occured while getting points of interest for city with id {cityId}");
                return StatusCode(500, "A problem happened while handling your request");
            }
        }







<h2> turning CitiesDataStore to a service interface

1./CitiesDataStore.cs
  - static thing is diabled 
2. Program.cs
    CitiesDataStore is registed into DI Container
3. /Controllers/CitiesController.cs
   - CitiesDataStore is injected into constructor of the class
   - data in CitiesDataStore can be accessed using _citiesDataStore

##/CitiesDataStore.cs
using CityInfo.API.Models;
using CityInfo.API.Services;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        //public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id=1,
                    Name="Newyork",
                    Description="Newyork is a city in the USA with big park",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name= "Water park",
                            Description="A water park having many themes"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name= "Art Library",
                            Description="Art Library with dozens of paintings"
                        }
                    }
                },
                new CityDto()
                {
                    Id=2,
                    Name="Texas",
                    Description="Texas is a city in the USA",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name= "Central Park",
                            Description="Central Park is most visited place"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name= "Empire Building",
                            Description="Empire Building is most historic"
                        }
                    }
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


##Program.cs
builder.Services.AddSingleton<CitiesDataStore>(); //new line - registering the CitiesDataStore to DI Container
var app = builder.Build(); 



##/Controllers/CitiesController.cs
namespace CityInfo.API.Controllers
{
    [ApiController]
    //[Route("api/cities")]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        private readonly CitiesDataStore _citiesDataStore;
        public CitiesController(ILogger<CitiesController> logger, CitiesDataStore citiesDataStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _citiesDataStore = citiesDataStore;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            //throw new Exception("Cities Exception");
            _logger.LogInformation("Cities are returned");
            return Ok(_citiesDataStore.Cities);
        }
   }
}






<h2> Working with configuration Files
1. json,xml, cmd, env, in-memory are various sources to save data
2. lets see how to use appsettings.json
3. /appsettings.json
   - added mailSettings{}
4. /Services/LocalMailService.cs
   - Constructor is injected with IConfiguration which is by default provided by aspnetcore
   - appsettings.json is expected naming convention & the default to obe used to use the IConfiguration 
   - learn more at src code; aspnetcore/src/DefaultBuilder/WebHost.cs builder.ConfigureCOnfiguration()

## /appsettings.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "CityInfo.API.Controllers.CitiesController": "Information",
      "CityInfo.API.Controllers.PointOfInterestController": "Information"
    }
  },
  "mailSettings": {
    "mailTo": "user@company.in",
    "mailFrom":  "admin@company.in"
  },
  "AllowedHosts": "*"
}



## /Services/LocalMailService.cs
namespace CityInfo.API.Services
{
    public class LocalMailService : ILocalMailService
    {
        private string _mailTo = string.Empty;
        private string _mailFrom = string.Empty;

        public LocalMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:mailTo"];
            _mailFrom = configuration["mailSettings:mailFrom"];
        }
        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from Local {_mailFrom} to {_mailTo}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}


## leveraging environment configuration and scoping configuration
1. right click on project > add > Add new item > search setting > select App Settings File
2. create files, appsettings.Development.json and appsettings.Production.json
3. LaunchSettings.Json > "ASPNETCORE_ENVIRONMENT": "Production" uses production.json and "ASPNETCORE_ENVIRONMENT": "Development" uses development json
4. This flexibility is provided default by aspnetcore to easily switch between dev & production

## appsettings.production.json
{
  "mailSettings": {
    "mailTo": "user@company.in",
    "mailFrom": "prod-admin@company.in"
  }
}


## appsettings.development.json
{
  "mailSettings": {
    "mailTo": "user@company.in",
    "mailFrom": "dev-admin@company.in"
  }
}

## appsettings.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "CityInfo.API.Controllers.CitiesController": "Information",
      "CityInfo.API.Controllers.PointOfInterestController": "Information"
    }
  },
  "AllowedHosts": "*"
}



Introducing Entity Framework Core to the project




<h2> Creating Entities and making table relations using Primary & Foreign key
1. Create New folder Entities

## /Entities/City.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CityInfo.API.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [MaxLength(200)]
        public string? Description { get; set; }

        public ICollection<PointOfInterest> PointsOfInterest { get; set; } = new List<PointOfInterest>();


        public City(string name)
        {
            Name = name;
        }
    }
}


## /Entities/PointOfInterest.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CityInfo.API.Entities
{
    public class PointOfInterest
    {
        [Key] //primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //sql will auto-increment value
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [ForeignKey("CityId")]
        public City? City { get; set; }
        
        public int CityId { get; set; }

        public PointOfInterest(string name)
        {
            Name = name;
        }
    }
}



<h2> Creating DbContext

1. DbContext: represents Session with database
2. Add package : dotnet add package microsoft.entityframeworkcore --version 8.0.0
3. package can be instaled in 3 ways (1) Manage Nuget Packages -GUI (2) Package Manager Console (Install-Package Azure.Identity) (3) CMD line installation (dotnet add package Azure.Identity)
EF core may providers - sql, postgres- check docs
4. Create /DbContexts/CityInfoContext.cs
     - make it a derived class by inheriting DbContext from using Microsoft.EntityFrameworkCore
     - create DbSet for Cities,pointOfInterest
install Microsoft.EntityFrameworkCore.Sqlite (v8.0.0). v8 is compatible with my .NET version of the project
5. Program.cs
    - register the DbContext with Lifetime scope


## /DbContexts/CityInfoContext.cs
using Microsoft.EntityFrameworkCore;
using CityInfo.API.Entities;
namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities {get;set; }
        public DbSet<City> PointOfInterest { get; set; }
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options) { }
    }
}


## Program.cs
builder.Services.AddDbContext<CityInfoContext>(dbContextOptions 
    => dbContextOptions.UseSqlite("Data Source=CityInfo.db"));   //new line 
var app = builder.Build();


<h2> Workign with Migrations
1. Microsoft.EntityFrameworkCore.Tools - for migration add,delete
2. sqlite database can be used with VS using extension
   - Extensions > Manage Extensions > SQLite and SQL Server Compact Toolbox
3. Package manager console > add-migration CityInfoDBInitialMigration
    This will create a new folder "Migrations" with 2 files (1) Migration File - contains operations needed to apply (2)Model Snapshot - a snapshot of current model so EF determines what changes to be made
4. Package Manager console > update-database
   This cmd will update the database with pending migrations
5. Open the database in VS and click on add connection, it will auto fetch .db file
    VS > View > Other > SQLite and SQL Server Compact Toolbox
6. see the tables _EFMigrationshistory, Cities, PointOfInterest are created in DB






<h2> Seeding database with data using OnModelCreating()
1. using OnModelCreating() seed the data to database
2. add-migrations InitialSeeding
3. update-database


## /DbContexts/CityInfoContext.cs

using Microsoft.EntityFrameworkCore;
using CityInfo.API.Entities;
namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {

        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointOfInterest { get; set; }
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                    new City("New York City") { Id = 1, Description = "The Big Apple" },
                    new City("Los Angeles") { Id = 2, Description = "The City of Angels" },
                    new City("Chicago") { Id = 3, Description = "The Windy City" }
                );

            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                new PointOfInterest("Central Park") { Id = 1, CityId = 1, Description = "A large public park in New York City" },
                new PointOfInterest("Hollywood Sign") { Id = 2, CityId = 2, Description = "An iconic landmark in Los Angeles" },
                new PointOfInterest("Willis Tower") { Id = 3, CityId = 3, Description = "A skyscraper in Chicago" },
                new PointOfInterest("Statue of Liberty") { Id = 4, CityId = 1, Description = "A symbol of freedom and democracy" },
                new PointOfInterest("Griffith Observatory") { Id = 5, CityId = 2, Description = "An observatory in Los Angeles" },
                new PointOfInterest("Navy Pier") { Id = 6, CityId = 3, Description = "A popular tourist destination in Chicago" },
                new PointOfInterest("Lincoln Park Zoo") { Id = 7, CityId = 3, Description = "A free zoo in Chicago" },
                new PointOfInterest("The Getty Center") { Id = 8, CityId = 2, Description = "An art museum in Los Angeles" },
                new PointOfInterest("Brooklyn Bridge") { Id = 9, CityId = 1, Description = "A historic bridge in New York City" }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}








<h2> storing Connection Strings safely
1. appsettings is not preferred, because connection strings contain passwords, sometimes it may also have usernames & passwords
2. appsetting goes to source control, so its risky to store connection details
3. in prod region, add it to Sytem > Environment Variables
4. best to use Azure Key Vault - ToDo: implement this 




<h2> SQL Injection Attack

SELECT * FROM Cities WHERE City="Newyork";
Newyork; drop table Cities --
If Sql injection is performed on above string literal, data loss happens
-- is sql comment

 SQL Injectiona attacks can be mitigated by encapsulation and parameterizing SQL commands using DB Parameters

Safe Approaches
 LINQ: 
 .FromSql() (from EF v7.0)
 .FromSqlInterpolated() (before EF v7.0)

 Unsafe
 EF: .FromSqlRaw()

 Learn more: https://learn.microsoft.com/en-us/ef/core/querying/sql-queries?tabs=sqlserver











































