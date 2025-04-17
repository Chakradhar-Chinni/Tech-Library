HTTP Request would send serialized data and receive the same. 
Serialized data can be in Json / XML / any other format

<h2> REST API with Web API
1. Leverage HTTP Protocol
2. Each piece of data is available at unique location. For Example, /products ; /product(1)
3. HTTP Methods are mapped to actions. For example, Get,Post,PUT
4. HTTP Status Codes determine the outcome. 400, 204, 302
5. Response can also contain pointers on what to do next

<h2> Hierarchy of Web API with front end
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
7. REST API responses are human readable. gRPC are not as they use binaries which makes gRPC faster the REST
8.for internal org. prefer gRPC, for external prefer REST. a web api can contain gRPC and REST
