<<h2>>
/*
Parameter Binding in ASP.NET Core Web API: [FromQuery], [FromBody], [FromRoute], [FromHeader], [FromForm], and [FromServices] Explained

---

# **ASP.NET Core Web API `[From*]` Attributes – Notes**

---

## **1️⃣ `[FromQuery]`**

* **Source:** Query string in URL (`?key=value`)
* **Use Case:** GET requests, filtering, paging, search
* **Supports:** Simple types or complex models
* **Key Points:**

  * Optional/default values supported
  * Extra query parameters **ignored** by default
  * Can bind a model class

**Example:**

```csharp
public class ProductFilter
{
    public string Category { get; set; }
    public int Page { get; set; } = 1;
}

[HttpGet("filter")]
public IActionResult FilterProducts([FromQuery] ProductFilter filter)
{
    return Ok(filter);
}
```

**Request:**

```
GET /api/products/filter?category=Books&page=2
```

---

## **2️⃣ `[FromBody]`**

* **Source:** HTTP request body (JSON, XML)
* **Use Case:** POST, PUT, PATCH requests (DTOs)
* **Supports:** Complex types only
* **Key Points:**

  * Only **one `[FromBody]`** per action
  * ASP.NET Core automatically **deserializes JSON** to object

**Example:**

```csharp
public class ProductDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}

[HttpPost("create")]
public IActionResult CreateProduct([FromBody] ProductDto product)
{
    return Ok(product);
}
```

**Request Body (JSON):**

```json
{ "Name": "Book", "Price": 250 }
```

---

## **3️⃣ `[FromRoute]`**

* **Source:** Route parameters (URL segments)
* **Use Case:** RESTful resource identifiers
* **Supports:** Simple types
* **Key Points:**

  * Must match **route template**
  * Can combine with `[FromQuery]`

**Example:**

```csharp
[HttpGet("products/{id}")]
public IActionResult GetProduct([FromRoute] int id)
{
    return Ok(new { id });
}
```

**Request:**

```
GET /api/products/5
```

---

## **4️⃣ `[FromHeader]`**

* **Source:** HTTP headers
* **Use Case:** Auth tokens, API keys, metadata
* **Supports:** Simple types
* **Key Points:**

  * Can specify **custom header name**
  * Optional headers can be nullable

**Example:**

```csharp
[HttpGet("auth")]
public IActionResult Get([FromHeader(Name = "X-Api-Key")] string apiKey)
{
    return Ok(new { apiKey });
}
```

**Request Header:** `X-Api-Key: 12345`

---

## **5️⃣ `[FromForm]`**

* **Source:** Form-data (`multipart/form-data`)
* **Use Case:** File uploads, HTML form submissions
* **Supports:** Files and simple fields
* **Key Points:**

  * Works with `IFormFile` for files
  * Multiple fields supported

**Example:**

```csharp
[HttpPost("upload")]
public IActionResult Upload([FromForm] IFormFile file, [FromForm] string description)
{
    return Ok(new { fileName = file.FileName, description });
}
```

---

## **6️⃣ `[FromServices]`** *(Extra, DI binding)*

* **Source:** Dependency Injection container
* **Use Case:** Inject services directly into controller action
* **Supports:** Any registered service

**Example:**

```csharp
[HttpGet("time")]
public IActionResult GetTime([FromServices] IDateTimeProvider dateTimeProvider)
{
    return Ok(dateTimeProvider.Now());
}
```

---

## **7️⃣ Quick Comparison Table**

| Attribute        | Source                | Typical Use           | Notes                     |
| ---------------- | --------------------- | --------------------- | ------------------------- |
| `[FromQuery]`    | URL query string      | GET, filters, paging  | Multiple params allowed   |
| `[FromBody]`     | HTTP request body     | POST, PUT, PATCH DTOs | Only one per action       |
| `[FromRoute]`    | Route segment         | Resource identifiers  | Must match route template |
| `[FromHeader]`   | HTTP headers          | Auth tokens, metadata | Custom header names       |
| `[FromForm]`     | Form-data (multipart) | File uploads, forms   | Works with IFormFile      |
| `[FromServices]` | Dependency Injection  | Service injection     | Any registered service    |

---

✅ **Summary Tips:**

1. `[FromQuery]` → GET, filters, paging
2. `[FromBody]` → POST/PUT/PATCH DTOs, one per action
3. `[FromRoute]` → RESTful IDs in URL
4. `[FromHeader]` → custom headers, tokens
5. `[FromForm]` → files or form fields
6. `[FromServices]` → inject DI services

*/




<<h2>> Content Negotiation
/*
1. Web Api by default uses JSON format to send HTTP Responses
2. XML support is enabled using .AddXmlSerializerFormatters() in program.cs
3. Accept: application/xml or  Accept: application/json is now supported (content negotiation)
4. [Produces("application/xml")] at action or controller level restricts to send response in xml format. 
    - 406 Not Acceptable  http error is shown if client requests application/json
    - this is same for [Produces("application/json")]

//Product.cs
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}


//Program.cs
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddXmlSerializerFormatters(); // Enable XML support

var app = builder.Build();
app.MapControllers();
app.Run();

//Controller
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    // JSON-only endpoint
    [HttpGet("json/{id}")]
    [Produces("application/json")] // Restrict to JSON
    public IActionResult GetJsonProduct(int id)
    {
        var product = new Product { Id = id, Name = "Laptop", Price = 50000 };
        return Ok(product);
    }

    // XML-only endpoint
    [HttpGet("xml/{id}")]
    [Produces("application/xml")] // Restrict to XML
    public IActionResult GetXmlProduct(int id)
    {
        var product = new Product { Id = id, Name = "Book", Price = 250 };
        return Ok(product);
    }
}

*/


<<h2>> APi Versioning
/*
1. Backward compatibility, add new features, serve different users
2. Use semantic versioning - MAJOR.MINOR.PATCH (e.g., 2.5.1).

MAJOR: Incremented for incompatible or breaking API changes
MINOR: Incremented for backward-compatible feature additions
PATCH: Incremented for backward-compatible bug fixes

default api version, deprecated apis
*/


<<h2>> Rate limiting VS Throttling
/*
Ratelimiting: limits the requests a user can make in a time period and reject others (HTTP 429)
Throttling: Controls the processing speed. Allow 1 request per second. If 5 requests arrive simultaneously → process 1/sec, queue others

*/





<h2>> Aliases for action methods
/*
[HttpPost]
[ActionName("StudentAdd")]
public void AddStudents(Student aStudent)
{
    StudentRepository.AddStudent(aStudent);
}

[Authorize] - controller, actionmethod level. only authenticated users can access the resource
[AllowAnonymous] controller, actionmethod level. non-authenticated users can access. /signup /forgotpassword



*/


<<h2> Web API return types

/*

| Feature                 | `IActionResult`          | `IEnumerable<T>` | `ActionResult<T>` |
| ----------------------- | ------------------------ | ---------------- | ----------------- |
| Return type flexibility | ✅ Yes                    | ❌ No             | ✅ Yes             |
| Strong typing           | ❌ No                     | ✅ Yes            | ✅ Yes             |
| Custom HTTP codes       | ✅ Yes                    | ❌ No             | ✅ Yes             |
| Swagger documentation   | Moderate                 | Poor             | Excellent         |
| Best suited for         | APIs with logic & errors | Simple demos     | Real-world APIs   |

*/
