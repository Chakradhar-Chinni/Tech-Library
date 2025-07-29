<<h2>>
aspnetcore 8 (compatible with 6,7,8,9)
.net 8
vs 2022



  


  <<h2>> What is asp.net core

## What is ASP.NET Core?
- Open source web framework by Microsoft and the community.
- Used for building modern web applications and services with .NET.
- Fully open source, maintained on GitHub, with community contributions.
- Microsoft provides full support.

## Key Features
- Modular: Composed of many libraries (NuGet packages); developers choose what to include.
- Cross-platform: Runs on Windows, Mac, and Linux.
- High performance and low memory footprint due to modularity.
- Fast development cycle with yearly updates.

## Types of Applications You Can Build
1. **Server-side Rendered Applications**
   - Dynamic content rendered on the server and sent to the client.
   - Examples: Web stores, blogs, enterprise web apps.

2. **Services**
   - Web services (APIs) for other applications or front ends.
   - Commonly REST APIs returning JSON.
   - Can also create gRPC services and microservices.

3. **Client-side Rendered Applications**
   - Traditionally built with JavaScript, but now possible with ASP.NET Core (e.g., Blazor).
   - Applications run in the browser, fetch data as needed.

## ASP.NET Core Architecture
- **Core Platform**: Handles requests, responses, middleware, Kestrel server, model binding, Razor engine, logging, etc.
- **Application Frameworks on Top of Core Platform:**
  - ASP.NET Core MVC (Model-View-Controller)
  - Razor Pages
  - ASP.NET Core APIs (REST, minimal APIs)
  - Blazor (component-based, client-side or server-side)

## Additional Frameworks
- **Entity Framework**: Database interaction.
- **ASP.NET Core Identity**: Authentication.
- **gRPC**: High-performance RPC services.
- **SignalR**: Real-time, bidirectional communication.

## Design Patterns
- **MVC (Model-View-Controller)**: Separation of concerns between data (model), UI (view), and logic (controller).
- **Razor Pages**: Simpler, server-side rendered, good for smaller apps.

## Version History & Support
- .NET and ASP.NET Core have evolved from Windows-only to cross-platform.
- Major versions:
  - ASP.NET Core 1.0 (2016), 2.x, 3.x, .NET 5, 6, 7, 8 (current, as of Nov 2023)
- LTS (Long-Term Support) for even-numbered versions (e.g., .NET 8).
- STS (Standard Term Support) for odd-numbered versions.

## Summary
- ASP.NET Core is a flexible, modular, high-performance, cross-platform framework for building all types of web applications and services.
- Supports a variety of architectures and design patterns.
- Actively developed and supported by Microsoft and the open-source community.

---









  


  <<h2>> Agenda
  1. develop project from scratch
  2. ASP.NET Core, Razor Pages, EF Core, Athentication, Testing, Deployment
  
