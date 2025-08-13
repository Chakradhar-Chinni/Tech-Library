<<h2>> Index

Routing in ASP.NET Core
Routing in ASP.NET Core MVC
Configuring Routes
Navigating with Tag Helpers


<<h2>> Routes and Navigation - theory

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




 



<<h2>> Routes
Output Image: Pie List
https://localhost:7241/pie/list







<<h2>> Routes and Navigation - Demo

Output Image: Pie Details 

1. /Controllers/Piecontroller.cs
   created new action method Details
2. /Views/Details.cshtml

## /Controllers/PieController.cs

using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(ICategoryRepository categoryRepository, IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        
        public IActionResult List()
        {
            PieListViewModel pieListViewModel = new PieListViewModel(_pieRepository.AllPies,"Chesse Cakes");
            return View(pieListViewModel);
        }

        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
                if(pie==null)
                {
                    return NotFound();
                }
            return View(pie);
        }
    }
}

## /Views/Pie/Details.cshtml
@model Pie

<h3 class="my-5">
    @Model.Name
</h3>

<div class="row gx-5">
    <img alt="@Model.Name" src="@Model.ImageUrl" class="img-fluid col-5" />
    <div class="col-7">
        <h4>@Model.ShortDescription</h4>
        <p>@Model.LongDescription</p>
        <h3 class="pull-right">@Model.Price.ToString("c")</h3>
        <div class="addToCart">
            <p class="button">

            </p>
        </div>
    </div>
</div>












<<h2>> Map Controller Route    
(1)
app.MapDefaultControllerRoute();
 - Follows MVC Conventions
 - BaseUrl/Controller/ActionMethod
 - suitable for small, medium apps



(2)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pie}/{action=List}"
    );
 - default: if controller or action segment is missing, default end point is hit
 - error page shown for unknown controller or action method
 - for valid controller or action segment, end point is hit accordingly
 - allows custom routes
 - define explicit route pattern 
 - requires more config, highly customizable
 - suitable for large apps with custom routing logic


 
