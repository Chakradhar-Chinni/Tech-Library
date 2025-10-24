### 🧩 **C# Language Interview Preparation Checklist**

1. Data types – value types vs reference types
2. Variables, constants, `var`, `dynamic`, and `object`
3. Type casting – implicit, explicit, `as`, `is`, pattern matching
4. Operators – arithmetic, logical, comparison, `??`, `?:`, `??=`, `=>`
5. String operations – immutability, interpolation, `StringBuilder`
6. Arrays and multidimensional arrays
7. Collections – List, Dictionary, HashSet, Queue, Stack
8. Enums and their usage
9. Structs – when and how to use
10. Tuples and Deconstruction
11. Records – immutability and value-based equality
12. Nullable types and null-coalescing operators (`??`, `?.`)
13. Exception handling – `try`, `catch`, `finally`, `throw`, custom exceptions
14. Namespaces and assemblies
15. Access modifiers – `public`, `private`, `protected`, `internal`, `protected internal`
16. Classes and objects
17. Constructors and constructor chaining
18. Static members and static classes
19. Inheritance – base and derived classes
20. Method overloading vs overriding
21. `virtual`, `override`, `sealed`, `abstract`, and `new` keywords
22. Encapsulation and data hiding
23. Abstraction – abstract classes and interfaces
24. Interfaces – multiple inheritance and default interface methods
25. Polymorphism – compile-time and runtime
26. Partial classes and methods
27. Nested classes
28. Properties – auto, read-only, computed properties
29. Indexers
30. `this` and `base` keywords
31. Static constructors and destructors
32. Copy constructor, shallow copy vs deep copy
33. Boxing and unboxing
34. Value types vs reference types in memory
35. Garbage collection and IDisposable pattern
36. `using` statement and resource cleanup
37. Generics – classes, methods, constraints
38. Delegates – singlecast and multicast
39. `Func<>`, `Action<>`, and `Predicate<>` delegates
40. Events and event handlers
41. Anonymous methods
42. Lambda expressions
43. Extension methods
44. LINQ – `Select`, `Where`, `OrderBy`, `GroupBy`, `Join`, `Aggregate`
45. Deferred execution in LINQ
46. Anonymous types
47. Attributes and reflection
48. Iterators – `yield return`, `yield break`
49. Dynamic keyword and `ExpandoObject`
50. Async and await – task-based asynchronous pattern
51. Tasks vs Threads
52. `lock`, `Monitor`, `Semaphore`, `Mutex`
53. Thread safety and race conditions
54. `Thread`, `ThreadPool`, `Task.Run`, `Parallel.For`
55. CancellationToken
56. Exception handling in async methods
57. Memory management – stack vs heap
58. Value equality vs reference equality (`Equals`, `==`, `GetHashCode`)
59. Immutable types and readonly fields
60. Constant vs readonly difference
61. Operator overloading
62. Indexer overloading
63. Reflection basics – `Type`, `MethodInfo`, `PropertyInfo`
64. Attributes – creating and applying custom attributes
65. Unsafe code and pointers (basics only)
66. Nullable reference types (C# 8+)
67. Pattern matching (C# 8–12 features)
68. Records vs classes
69. Target-typed `new()` expressions
70. Init-only setters (`init` keyword)
71. Switch expressions
72. Local functions
73. Default interface implementations
74. Covariance and contravariance (`in`, `out` keywords)
75. Expression-bodied members
76. `nameof`, `checked`, and `unchecked` keywords
77. `var` vs explicit typing
78. Discards (`_`) in pattern matching and tuples
79. Deconstruction syntax
80. Top-level statements (C# 9+)

---

### 🌐 **.NET Core / ASP.NET Core Interview Preparation Checklist**

1. .NET Core vs .NET Framework – differences and advantages
2. .NET 5/6/7/8 unified platform basics
3. Runtime and SDK – understanding `dotnet` CLI
4. Project types – Console, Web API, MVC, Blazor, Worker Service
5. Startup.cs / Program.cs structure (hosting, services, middleware)
6. Dependency Injection – built-in IoC container
7. Service lifetimes – Singleton, Scoped, Transient
8. Middleware – request pipeline, custom middleware creation
9. Routing – attribute routing vs conventional routing
10. Controllers – API controllers vs MVC controllers
11. Action methods – parameters, model binding, model validation
12. Filters – authorization, exception, action, result, resource filters
13. Model validation – Data Annotations, Fluent Validation
14. Logging – `ILogger`, third-party logging (Serilog, NLog)
15. Configuration – `appsettings.json`, environment variables, options pattern
16. Environment-based configuration (`Development`, `Staging`, `Production`)
17. Hosting – Kestrel vs IIS vs Nginx/Apache
18. Health checks
19. Versioning – URL versioning, header versioning
20. CORS – configuration and security implications
21. Authentication – JWT, cookie, OAuth, OpenID Connect
22. Authorization – role-based, policy-based, claims-based
23. HTTPS redirection and HSTS
24. Exception handling middleware
25. Response caching, output caching
26. Rate limiting / throttling
27. Compression middleware (gzip, Brotli)
28. Dependency Injection patterns – repository, service layer, unit of work
29. Entity Framework Core – DbContext, DbSet, migrations
30. EF Core – LINQ queries, eager/lazy/explicit loading
31. Transactions in EF Core
32. Concurrency handling in EF Core
33. Async programming in .NET Core (async/await in APIs)
34. Task-based asynchronous pattern
35. Background services – `IHostedService`, `BackgroundService`
36. SignalR basics (real-time communication)
37. gRPC in .NET Core
38. Minimal APIs – structure, routing, endpoints
39. Content negotiation – JSON, XML
40. Versioning and Swagger/OpenAPI documentation
41. API testing – Postman, Swagger UI, integration testing
42. Middleware order and dependency injection order
43. Dependency Injection with Options pattern (`IOptions`, `IOptionsSnapshot`)
44. Environment-specific secrets – `UserSecrets`
45. Deployment – Dockerizing .NET Core apps
46. CI/CD pipelines – GitHub Actions, Azure DevOps basics
47. Application insights / monitoring
48. Static files, file uploads, and downloads
49. Localization and globalization
50. Security best practices – input validation, XSS, CSRF, SQL injection prevention
51. Custom exception handling and logging strategies
52. Rate limiting and throttling using middleware
53. Unit testing controllers and services with xUnit / MSTest / NUnit
54. Integration testing Web API endpoints
55. Performance optimization – caching strategies, async IO, response compression

---


---

### 🌐 **ASP.NET MVC Interview Preparation Checklist**

1. MVC architecture – Model, View, Controller responsibilities
2. Routing – conventional routing vs attribute routing
3. Route parameters, default values, constraints
4. Controllers – creation, action methods, return types
5. ActionResult types – ViewResult, PartialViewResult, JsonResult, RedirectToAction, FileResult, ContentResult
6. Views – Razor syntax, layout pages, sections, partial views
7. Strongly typed views vs weakly typed views
8. ViewData, ViewBag, TempData – differences and use cases
9. Model binding – form data, query string, route data
10. Model validation – Data Annotations, client-side validation
11. HTML Helpers – `Html.TextBoxFor`, `Html.DropDownListFor`, `Html.EditorFor`
12. Tag Helpers – built-in and custom
13. Forms – GET vs POST, anti-forgery tokens
14. Filters – Authorization, Action, Result, Exception, Custom filters
15. Dependency Injection in MVC – service registration and usage
16. Partial views and View Components
17. Layouts and sections in Razor views
18. TempData vs Session vs Cookies
19. Bundling and minification (CSS/JS)
20. Areas in MVC – modular application structure
21. Routing for areas and multiple controllers
22. AJAX in MVC – `Ajax.BeginForm`, `jQuery.ajax` with MVC endpoints
23. JSON serialization – returning JSON from controller actions
24. Exception handling – `HandleError` attribute, custom error pages
25. Filters pipeline and execution order
26. Action filters vs Result filters vs Exception filters
27. Url helpers – `Url.Action`, `Url.Content`, `Url.RouteUrl`
28. Authentication – Forms authentication, Identity basics
29. Authorization – role-based and custom
30. Global.asax events – Application_Start, Application_Error, Application_End
31. Session management – storing and retrieving session data
32. TempData for passing data between requests
33. Partial views vs RenderAction vs ViewComponents
34. Model-View-ViewModel (MVVM) concepts in MVC context
35. Razor syntax – inline C#, loops, conditionals, expressions
36. HTML5 and CSS3 integration with MVC views
37. Client-side validation – jQuery Validation integration
38. Server-side validation – ModelState checks
39. Action method overloading limitations
40. File upload and download implementation
41. Caching – output cache, data cache, memory cache
42. Performance optimization in MVC apps
43. Logging – using `ILogger` or other logging frameworks
44. Security best practices – XSS, CSRF, SQL injection prevention
45. Route constraints and custom constraints
46. Areas and modularization in large applications
47. Deployment – IIS hosting basics
48. Razor Pages vs MVC controllers – differences and use cases
49. Partial view vs child action – pros and cons
50. Unit testing MVC controllers and actions

---


---

### 🌐 **ASP.NET Web API Interview Preparation Checklist**

1. Web API vs MVC – differences and use cases
2. HTTP basics – methods (GET, POST, PUT, PATCH, DELETE), status codes
3. REST principles – statelessness, resources, URIs
4. Controllers – ApiController vs ControllerBase
5. Routing – attribute routing vs conventional routing
6. Route parameters and query string binding
7. Model binding – from body, query, route, header (`[FromBody]`, `[FromQuery]`, `[FromRoute]`, `[FromHeader]`)
8. Content negotiation – JSON, XML, custom formatters
9. Media formatters – JSON.NET, System.Text.Json
10. Versioning – URL, query string, header-based
11. Action results – `Ok()`, `NotFound()`, `Created()`, `BadRequest()`, `NoContent()`
12. Exception handling – `try-catch`, `ExceptionFilterAttribute`, global exception middleware
13. Dependency Injection – services in controllers
14. Logging – `ILogger` and structured logging
15. Validation – model validation, `ModelState.IsValid`, custom validation attributes
16. Asynchronous programming – `async`/`await` in controller actions
17. Task-based vs thread-based async methods
18. Caching – response caching, memory caching, distributed caching
19. Throttling and rate limiting
20. Security – authentication (JWT, API key), authorization (roles, policies, claims)
21. HTTPS and SSL enforcement
22. Custom filters – action filters, authorization filters, exception filters
23. Swagger/OpenAPI – documentation and testing endpoints
24. Middleware pipeline – request and response flow
25. HATEOAS basics – hypermedia links in responses
26. OData support – filtering, sorting, pagination
27. Routing constraints and custom route handlers
28. Response formatting – custom response objects, envelope patterns
29. Versioning strategies – multiple API versions coexisting
30. Unit testing Web API controllers – xUnit, MSTest, Moq
31. Integration testing – in-memory test server, WebApplicationFactory
32. Logging request and response bodies
33. Global error handling – `UseExceptionHandler` middleware
34. Rate limiting and throttling via middleware
35. Health checks and monitoring
36. File upload/download via Web API
37. Async streaming – IAsyncEnumerable, file streaming
38. JSON patch – partial updates via `PATCH`
39. SignalR integration for real-time APIs (optional)
40. Best practices – RESTful conventions, response structure, error handling

---

---

### 🔹 **LINQ Interview Preparation Checklist**

1. LINQ basics – what it is and why to use it
2. LINQ query syntax vs method syntax
3. `IEnumerable` vs `IQueryable` – differences and use cases
4. Deferred execution vs immediate execution
5. LINQ operators – filtering (`Where`)
6. Projection – `Select`, `SelectMany`
7. Sorting – `OrderBy`, `OrderByDescending`, `ThenBy`, `ThenByDescending`
8. Grouping – `GroupBy`
9. Joining – `Join`, `GroupJoin`, `Left Join` equivalent
10. Aggregation – `Count`, `Sum`, `Average`, `Min`, `Max`
11. Partitioning – `Take`, `TakeWhile`, `Skip`, `SkipWhile`
12. Element operators – `First`, `FirstOrDefault`, `Last`, `LastOrDefault`, `Single`, `SingleOrDefault`, `ElementAt`
13. Quantifiers – `Any`, `All`, `Contains`
14. Set operators – `Distinct`, `Union`, `Intersect`, `Except`
15. Conversion operators – `ToList`, `ToArray`, `ToDictionary`, `Cast`, `OfType`
16. LINQ with objects, arrays, collections, DataTables, XML, JSON
17. Complex types – projection into anonymous types
18. LINQ with nullable types
19. LINQ with nested collections (`SelectMany`)
20. Deferred execution implications – modifying source collection after query definition
21. `AsEnumerable` vs `AsQueryable`
22. LINQ and performance considerations
23. Expression trees – how LINQ queries are converted to expressions
24. LINQ to SQL basics
25. LINQ to Entities / EF Core – translating LINQ to SQL
26. Custom LINQ methods (extension methods)
27. Exception handling in LINQ queries
28. Combining multiple LINQ operators in a single query
29. Pagination using `Skip` and `Take`
30. Lambda expressions in LINQ queries

---

---

### 🗄️ **Entity Framework Core Interview Preparation Checklist**

1. EF Core vs EF 6 – differences and advantages
2. Code First vs Database First vs Model First approaches
3. DbContext – purpose and lifecycle
4. DbSet – CRUD operations
5. Entity configuration – Data Annotations vs Fluent API
6. Primary keys and composite keys
7. Relationships – one-to-one, one-to-many, many-to-many
8. Navigation properties – eager, lazy, and explicit loading
9. Migrations – adding, updating, and removing migrations
10. Updating database schema via migrations
11. Seeding data – initial data population
12. Change tracking – `Add`, `Update`, `Remove`, `Attach`
13. Entity states – Added, Modified, Deleted, Unchanged, Detached
14. LINQ to Entities – queries and execution
15. Query execution – deferred vs immediate execution
16. AsNoTracking – improving read-only query performance
17. Transactions – `BeginTransaction`, `Commit`, `Rollback`
18. Concurrency handling – optimistic concurrency, RowVersion
19. Raw SQL queries – `FromSqlRaw`, `ExecuteSqlRaw`
20. Global query filters – soft delete, multi-tenancy
21. Shadow properties – properties not in entity class but mapped to database
22. Owned entity types – value objects in EF Core
23. Complex types – embedding entities in other entities
24. DbContext pooling for performance
25. Lazy loading proxies – enabling and configuring
26. EF Core logging – SQL output, logging queries
27. Asynchronous operations – `ToListAsync`, `FirstOrDefaultAsync`, `SaveChangesAsync`
28. EF Core interceptors – command, connection, transaction interceptors
29. Caching strategies – first-level cache (DbContext), second-level cache (optional)
30. Performance tuning – batching, no-tracking queries, compiled queries
31. Indexes and constraints via Fluent API
32. Cascade delete behavior – `Cascade`, `SetNull`, `Restrict`
33. Database providers – SQL Server, SQLite, PostgreSQL, MySQL
34. EF Core conventions – table names, column names, relationships
35. Handling migrations in production – downtime-free strategies
36. Integration with ASP.NET Core – dependency injection of DbContext
37. Unit testing EF Core – in-memory database vs mocking
38. Query splitting – `SplitQuery` for complex includes
39. Bulk operations – EF Core limitations and third-party libraries
40. Tracking vs No-tracking queries and their use cases

---




