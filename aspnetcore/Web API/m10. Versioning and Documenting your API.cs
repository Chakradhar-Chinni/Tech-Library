<h2> Versioning your API
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
     
