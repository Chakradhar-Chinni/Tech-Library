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


