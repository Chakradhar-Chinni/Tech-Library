<<h2>> Index

Routing in ASP.NET Core
Routing in ASP.NET Core MVC
Configuring Routes
Navigating with Tag Helpers


<<h2>> ROutes and Navigation

 Traditional Web Request Handling
A request is made for a physical file (e.g., index.html).
The web server searches for the file on disk.
If found, the file is returned to the client as the response.

ASP.NET Core MVC Request Handling
Requests are not mapped to physical files.
Instead, they are mapped to action methods on controllers.
The response is typically a view (.cshtml file), but the URL targets the action method, not the view file


Types of Routing

1. Convention-Based Routing

Defining Route:
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}"
);

URL: bethanyspieshop.com/Pie/List
Pie: Controller
List: Action

2. Extended Convention-Based Routing

Route:
"{controller}/{action}/{id?}"

URL: Pie/Details/1 

Summary:

1. **Routing decouples URLs from physical files.**  
2. **Endpoints are defined in `Program.cs`.**  
3. **Middleware (`UseRouting`, `UseEndpoints`) powers the routing system.**  
4. **Convention-based routing uses patterns to match URLs to controller actions.**  
5. **`MapDefaultControllerRoute` simplifies common routing patterns.**  
6. **Attribute-based routing is ideal for APIs.**


  
