<<h2>> Creating New Project
from VS templates > Choose  > ASP.NET COre Web APP (MVC)

  Explore the initial files created

  main method is replaced by top level statements from .net 6, c#10

  
  
  
  <<h2>> undestnad Kestrel server role




<<h2>> Creating New Project

  1. create ASP.NET Core Empty project
  2. idea is to create everything from scratch with out using pre-defined templates


<<h2>> COnfiguring the application

## Program.cs
var builder = WebApplication.CreateBuilder(args);  //can access appsettings, kestrel, wwwroot, services collection
builder.Services.AddControllersWithViews(); //lets to use MVC
var app = builder.Build(); // builds the app
app.UseStaticFiles(); // middle ware that looks at wwwroot folder

if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); //exception middleware for dev regions
}

app.MapDefaultControllerRoute(); // middleware for routes
app.Run();

