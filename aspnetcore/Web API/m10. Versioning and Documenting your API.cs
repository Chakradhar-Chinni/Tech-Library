<<h2>> Versioning your API
  - As, API keeps evolving. set of users might still require old API while others require updated ones. Versioning helps here.
  - URI versioning is most used these days

1. URI Versioning (Path-Based)
   - Version is part of the URL.
   - Example:       
     GET /api/v1/cities
     GET /api/v2/cities
     

2. Query String Versioning
   - Version is passed as a query parameter.
   - Example:       
     GET /api/cities?version=1
     

3. Custom Header Versioning
   - A custom HTTP header is used to specify the version.
   - Example:       
     X-API-Version: 1
     

4. Accept Header Versioning (Media Type Versioning)
   - Version is embedded in the `Accept` header using MIME types.
   - Example:       
     Accept: application/vnd.cityinfo.v1+json
     

5. Version in Accept Header Value
   - Similar to media type versioning, but more flexible.
   - Example:       
     Accept: application/json; version=1
     


       



<<h2>> Supporting Versioing
1. Install Nuget Package asp.versioning.mvc
2. temporarily comment [Authorize] on controllers 



## Program.cs

using Asp.Versioning;

builder.Services.AddApiVersioning(setupAction =>
{
    setupAction.ReportApiVersions = true;
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
    setupAction.DefaultApiVersion = new ApiVersion(1, 0);
}).AddMvc();


After hitting any endpoint, Response headers looks like below

content-length	185
content-type	application/json; charset=utf-8
date	Fri, 29 Aug 2025 15:13:05 GMT
server	Kestrel
x-pagination	{"TotalItemCount":3,"TotalPageCount":1,"PageSize":10,"CurrentPage":1}
api-supported-versions	1.0

  /*
note: if setupAction.ReportApiVersions = true; is only given, then following error occurs

{
  "type": "https://docs.api-versioning.org/problems#unspecified",
  "title": "Unspecified API version",
  "status": 400,
  "detail": "An API version is required, but was not specified."
}
*/







  

<<h2>> Versioning your API

1. CitiesController.cs
  [ApiVersion] is added
  as default version is assumed when version is not specified in URI, both URLs works

2. PointOfInterestcontroller.cs
  - two Api Versions are added
  - onlu mentioned api versions can access, other api versions cannot access and gives error as "Unspecified Api Version". for detail error look at previous module

3. Files Controller.cs
  - ApiVersion is marked as deprecated

- look at response headers for all three files

/*
https://localhost:7167/api/cities?api-version=1
https://localhost:7167/api/cities
Response Headers
content-length	185
content-type	application/json; charset=utf-8
date	Sat, 30 Aug 2025 08:09:46 GMT
server	Kestrel
x-pagination	{"TotalItemCount":3,"TotalPageCount":1,"PageSize":10,"CurrentPage":1}
api-supported-versions	1.0
*/

## /Controllers/CitiesController.cs
using Asp.Versioning;
namespace CityInfo.API.Controllers
{
    [ApiController]
    //[Route("api/cities")]
    //[Authorize]
    [Route("api/[controller]")]
    [ApiVersion(1)]
    public class CitiesController : ControllerBase
    {


/*
https://localhost:7167/api/cities/2/pointofinterest?api-version=2
Response Headers
content-length	246
content-type	application/json; charset=utf-8
date	Sat, 30 Aug 2025 08:09:59 GMT
server	Kestrel
api-supported-versions	2.0, 3.0
*/

## /Controllers/PointOfInterestController.cs
using Asp.Versioning;
namespace CityInfo.API.Controllers
{
    [ApiController]
    //[Authorize(Policy= "MustBeFromChicago")]
    [Route("/api/cities/{cityId}/pointofinterest")]
    [ApiVersion(2)]
    [ApiVersion(3)]
    public class PointOfInterestController : ControllerBase
    {



/*
https://localhost:7167/api/files/1?api-version=0.5
Response Headers
content-length	2951831
content-type	application/pdf
date	Sat, 30 Aug 2025 08:10:02 GMT
server	Kestrel
content-disposition	attachment; filename=FSD-MERN-Stack-brochure.pdf; filename*=UTF-8''FSD-MERN-Stack-brochure.pdf
api-deprecated-versions	0.5
*/

## /Controllers/FilesController.cs
using Asp.Versioning;

namespace CityInfo.API.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/files")]
    [ApiVersion(0.5,Deprecated=true)]
    public class FilesController : ControllerBase









<<h2>> Supporting Versioned Routes

1. previous module api route is https://localhost:7167/api/files/1?api-version=0.5
2. Instead of using api-version attribure, /api/v1 can be implemented which is modern and better approach
3. [Route] is enhanced to include Version number, remaning things are same as previous module

## /Controllers/CitiesController.cs
namespace CityInfo.API.Controllers
{
    [ApiController]
    //[Route("api/cities")]
    //[Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion(1)]
    public class CitiesController : ControllerBase


## /Controllers/PointOfInterestController.cs
namespace CityInfo.API.Controllers
{
    [ApiController]
    //[Authorize(Policy= "MustBeFromChicago")]
    [Route("/api/v{version:apiVersion}/cities/{cityId}/pointofinterest")]
    [ApiVersion(2)]
    [ApiVersion(3)]
    public class PointOfInterestController : ControllerBase

## /Controllers/FilesController.cs
namespace CityInfo.API.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/v{version:apiVersion}/files")]
    [ApiVersion(0.5,Deprecated=true)]
    public class FilesController : ControllerBase
    {













<<h2>> Documenting your API with API/Swagger

1. OpenAPI (Swagger) Provides a Standardized Way to Describe APIs : OpenAPI is a specification format (JSON/YAML) that defines what an API does and how to interact with it. Swagger is a suite of tools built around       OpenAPI, including Swagger UI for documentation and Swagger Editor for creating specs.
2. Swashbuckle.AspNetCore Automates OpenAPI Integration in ASP.NET Core : Swashbuckle generates the OpenAPI spec automatically and embeds Swagger UI into your project, fulfilling both the need for a spec and a documentation/testing interface.





<<h2>> Incorporating XML Comments on Actions

1. Program.cs
  - support for xml comments added
2. CityWithoutPointsOfInterestDto.cs
  - enter 3 slashes /// 
  - xml comments is auto generated with 3 slashes
  - Swagger Documentation shows the comments at schemas
3. CitiesController.cs
  - XML comments are added usign 3 slashes on action method
  - Swagger Documentation shows the comments at API endpoints


##Program.cs
using System.Reflection;
builder.Services.AddSwaggerGen(setupAction =>
{
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
    setupAction.IncludeXmlComments(xmlCommentsFullPath);
}); //integrates Swagger

right click on Project Name > properties > Build > Output > enable check box on Documentation File > provide "CityInfo.API.xml" under XML documentation file path


## /Models/CityWithoutPointsOfInterestDto.cs
namespace CityInfo.API.Models
{
    /// <summary>
    /// A city without its points of interest
    /// </summary>
    public class CityWithoutPointsOfInterestDto
    {
        /// <summary>
        /// the id of the city
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// the name of the city
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The description of the city
        /// </summary>
        public string? Description { get; set; }
    }
}


## /Controllers/CitiesControllers.cs

/// <summary>
/// Get a city by id
/// </summary>
/// <param name="id">The id of the city to get</param>
/// <param name="includePointOfInterest">Whether or not include PointsOfInterest</param>
/// <returns>A city with or without pointsofinterest</returns>

[HttpGet("{id}")]
public async Task<ActionResult<IEnumerable<CityDto>>> GetCity(int id,bool includePointOfInterest)
{
    var cityEntity = await _cityInfoRepository.GetCityAsync(id, includePointOfInterest);
     
    return Ok(_mapper.Map<CityDto>(cityEntity));
}










<<h2>> Describing Response Types and Status Codes

1. CitiesController.cs
   - In Swagger documentation endpoints shows the below,
   - [ProducesResponseType] attribute shows the possible status code that may be returend by the end point
   - <response code> over-rides the default description to "Returns the requested city" from OK (this is optional)

## /Controllers/CitiesControllers.cs
 /// <summary>
 /// Get a city by id
 /// </summary>
 /// <param name="id">The id of the city to get</param>
 /// <param name="includePointOfInterest">Whether or not include PointsOfInterest</param>
 /// <response code="200">Returns the requested city</response>
 /// <returns>A city with or without pointsofinterest</returns>

 [HttpGet("{id}")]
 [ProducesResponseType(StatusCodes.Status404NotFound)]
 [ProducesResponseType(StatusCodes.Status400BadRequest)]
 [ProducesResponseType(StatusCodes.Status200OK)]
 public async Task<ActionResult<IEnumerable<CityDto>>> GetCity(int id,bool includePointOfInterest)
 {
     var cityEntity = await _cityInfoRepository.GetCityAsync(id, includePointOfInterest);
      
     return Ok(_mapper.Map<CityDto>(cityEntity));
 }













<<h2>> Supporting Different documentation Versions
1. Install Nuget Package Asp.Versioning.Mvc.ApiExplorer
2. Enable Version Substitution in URIs: Configured AddApiExplorer with SubstituteApiVersionInUrl = true to automatically fill version numbers in URIs, removing the need for manual parameters.
3. Support Multiple Swagger Specs: Used IApiVersionDescriptionProvider to dynamically generate Swagger specifications for each API version, ensuring all versions are documented.
4. Configure Swagger UI for Versions: Updated UseSwaggerUI to include endpoints for each version using DescribeApiVersions, allowing users to switch between API versions in the UI.


 ## Program.cs
builder.Services.AddApiVersioning(setupAction =>
{
    setupAction.ReportApiVersions = true;
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
    setupAction.DefaultApiVersion = new ApiVersion(1, 0);
}).AddMvc()
.AddApiExplorer(setupAction=>
{
    setupAction.SubstituteApiVersionInUrl = true;
});

var ApiVersionDescriptionProvider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

builder.Services.AddSwaggerGen(setupAction =>
{
    foreach(var description in ApiVersionDescriptionProvider.ApiVersionDescriptions)
    {
        setupAction.SwaggerDoc(
            $"{description.GroupName}",
            new()
            {
                Title="City Info API",
                Version="description.ApiVersion.ToString()",
                Description="Through this version you can access cities and their points of Interest"
            });
    }
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
    setupAction.IncludeXmlComments(xmlCommentsFullPath);
}); //integrates Swagger




var app = builder.Build(); // Build the app



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setupAction =>
    {
        var descriptions = app.DescribeApiVersions();
        foreach(var description in descriptions)
        {
            setupAction.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
   // app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler();
}































 
