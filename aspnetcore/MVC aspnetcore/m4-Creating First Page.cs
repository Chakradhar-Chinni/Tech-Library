<<h2>> Agenda

1. MVC Intro
2. Create model, repository
3. Create controller
4. Add View
5. Style View






<<h2>> Introducing MVC PAttern

ASP.NET core is framework for building modern apps
ASP.NET core MVC is built on top of ASP.NET core

Model: 
 - POCO classes
 - contains data & business logic



Note: Implicit using
1. Implicit using directives automatically include commonly used namespaces in your project, so you don’t have to manually add them at the top of every file
2. How? When you create a new project (e.g., using dotnet new), the SDK includes a file called ImplicitUsings.cs behind the scenes, or it configures them via the .csproj file.

3. .csproj file
    <Project Sdk="Microsoft.NET.Sdk.Web">
      <PropertyGroup>
        <TargetFramework>net7.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
      </PropertyGroup>
      </Project>
      
4. default implicit statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


  
  

 <<h2>> Creating the Model and Repository


1. Pie.cs
  public Category Category { get; set; } = default!;
  = default! is null-forgiving operator introduced in c# 8.0
  why? avoid compile warning, 
  At compile time: No warning, because default! suppresses the nullability warning.
  At runtime: If you try to access Category without assigning it, you'll get a NullReferenceException.

2. Category.cs

3. IPieRepository.cs
    - its a Repository Interface defining contracts without showing how operations are implemented
    - 







