 
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
