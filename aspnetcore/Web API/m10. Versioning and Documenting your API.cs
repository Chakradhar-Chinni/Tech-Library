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


