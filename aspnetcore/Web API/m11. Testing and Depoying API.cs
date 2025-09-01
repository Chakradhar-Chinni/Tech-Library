<<h2>> Testing API End Points

Non-Microsoft Ways:
HTTP_REPL: 
  - REPL: Read Eval Print Loop
  - a CLI tool to test API end points and inspect responses
.http files:
  - released in .net 8


<<h2>> Installing HTTP_REPL

https://www.nuget.org/packages/Microsoft.dotnet-httprepl#versions-body-tab

cmd > dotnet tool install --global Microsoft.dotnet-httprepl --version 8.0.0
  - this installs globally

- check globally installed dotnet tools
cmd > dotnet tool list -g
Package Id                     Version      Commands
----------------------------------------------------
microsoft.dotnet-httprepl      8.0.0        httprepl



<<h2>> Testing APi with HTTP_REPL

- see available cmds
cmd > httprepl -h
      /*
      Welcome to HttpRepl 8.0!
      ------------------------
      
      Telemetry
      ---------
      The .NET tools collect usage data in order to help us improve your experience. The data is collected by Microsoft and shared with the community. You can opt-out of telemetry by setting the DOTNET_HTTPREPL_TELEMETRY_OPTOUT environment variable to '1' or 'true' using your favorite shell.
      
      Read more about HttpRepl telemetry: https://aka.ms/httprepl-telemetry
      Read more about .NET CLI Tools telemetry: https://aka.ms/dotnet-cli-telemetry
      
      Usage:
        httprepl [<BASE_ADDRESS>] [options]
      
      Arguments:
        <BASE_ADDRESS> - The initial base address for the REPL.
      
      Options:
        -h|--help - Show help information.
      
      Once the REPL starts, these commands are valid:
      
      Setup Commands:
      Use these commands to configure the tool for your API server
      
      connect        Configures the directory structure and base address of the api server
      set header     Sets or clears a header for all requests. e.g. `set header content-type application/json`
      add query-paramAdds a key and value pair to the query string
      clear query-paramClears the query string of all key and values
      
      HTTP Commands:
      Use these commands to execute requests against your application
      
      GET            get - Issues a GET request
      POST           post - Issues a POST request
      PUT            put - Issues a PUT request
      DELETE         delete - Issues a DELETE request
      PATCH          patch - Issues a PATCH request
      HEAD           head - Issues a HEAD request
      OPTIONS        options - Issues a OPTIONS request
      
      Navigation Commands:
      The REPL allows you to navigate your URL space and focus on specific APIs that you are working on
      
      ls             Show all endpoints for the current path
      cd             Append the given directory to the currently selected path, or move up a path when using `cd ..`
      
      Shell Commands:
      Use these commands to interact with the REPL shell
      
      clear          Removes all text from the shell
      echo [on/off]  Turns request echoing on or off, show the request that was made when using request commands
      exit           Exit the shell
      
      REPL Customization Commands:
      Use these commands to customize the REPL behavior
      
      pref [get/set] Allows viewing or changing preferences, e.g. 'pref set editor.command.default 'C:\\Program Files\\Microsoft VS Code\\Code.exe'`
      run            Runs the script at the given path. A script is a set of commands that can be typed with one command per line
      ui             Displays the Swagger UI page, if available, in the default browser
      
      Use `help <COMMAND>` for more detail on an individual command. e.g. `help get`
      For detailed tool info, see https://aka.ms/http-repl-doc
      */



cmd > httprepl https://localhost:7167/
(Disconnected)> connect https://localhost:7167/
Using a base address of https://localhost:7167/
Unable to find an OpenAPI description
For detailed tool info, see https://aka.ms/http-repl-doc


As different versions are enabled in program.cs, httprepl is unable to find openapi(swagger) specification at default location

cmd changes to base url as https://localhost:7167/ > 
-------------

cmd > https://localhost:7167/swagger/2.0/swagger.json/> connect https://localhost:7167 --openapi https://localhost:7167/swagger/2.0/swagger.json
Checking https://localhost:7167/swagger/2.0/swagger.json... Found
Parsing... Successful

Using a base address of https://localhost:7167/
Using OpenAPI description at https://localhost:7167/swagger/2.0/swagger.json
For detailed tool info, see https://aka.ms/http-repl-doc

--------------------------
https://localhost:7167/> ls
.     []
api   []

https://localhost:7167/> cd api
/api    []

https://localhost:7167/api> ls
.    []
..   []
v2   []

https://localhost:7167/api> cd v2
/api/v2    []

https://localhost:7167/api/v2> ls
.        []
..       []
cities   []

https://localhost:7167/api/v2> cd cities
/api/v2/cities    []

https://localhost:7167/api/v2/cities>

- see all end points
View > Other Windows > EndPointsExplorer









<<h2>> Hosting and Deploying
1. Publishing the Application
Publishing means generating the necessary files to run the application. This happens every time you rebuild the project, creating output files in the /bin/debug/net8.0 or /bin/release/net8.0 folder depending on the build configuration.

2. Local Hosting Environment
Locally, your machine acts as the host. The published files are copied to the appropriate folder, and the application is launched as a console app.

3. Process Manager: Kestrel
Kestrel is the default cross-platform HTTP server used to run ASP.NET Core apps locally. On Windows, IIS can be used for more advanced hosting features, but Kestrel is sufficient for most local development scenarios.

4.Application Execution
Once deployed locally, the application starts running and is ready to handle HTTP requests and send responses. This is managed by the process manager (Kestrel or IIS).

5.Next Step: Deployment Options
Now that local deployment is understood, the next step is to explore external hosting and deployment options such as cloud platforms (Azure, AWS), containers (Docker), or on-premise servers.







<<h2>> Considering Deployment and Hosting Options
1.Wide Range of Hosting Options
ASP.NET Core is cross-platform, allowing deployment on Windows, Linux, cloud platforms (Azure, AWS), containers (Docker), or even orchestrated environments like Kubernetes.

2.Key Decision Factors
When choosing a hosting option, consider scalability, maintenance effort, cost, infrastructure control, and organizational preferences.

3.Deployment Models Vary
Options range from simple file-based hosting to advanced containerized deployments with orchestration. You can go from non-scalable setups to auto-scaling, cloud-native architectures.

4.Chosen Approach: Azure App Service
For this course, the focus is on Azure App Service, a popular PaaS (Platform as a Service) that simplifies deployment by abstracting infrastructure concerns like security, reliability, and updates.

5.Next Step: Code Preparation
Before deploying to Azure, some code changes are required to make the application ready for cloud hosting.





<<h2> Dealing with proxies and load balancers

1.Why Forwarded Headers Are Needed
When requests pass through proxies or load balancers, key information like the original scheme (HTTPS), client IP, and host can be lost. These are forwarded via headers like X-Forwarded-For, X-Forwarded-Proto, and X-Forwarded-Host.

2.Purpose of the Middleware
The Forwarded Headers Middleware reads these headers and updates the HttpContext so the application can behave correctly and securely, especially in cloud or containerized environments.

3.Headers Processed

X-Forwarded-For: sets the remote IP address
X-Forwarded-Proto: sets the request scheme (HTTP/HTTPS)
X-Forwarded-Host: sets the host name

learn more: https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.forwardedheadersextensions.useforwardedheaders?view=aspnetcore-8.0

4.Automatic Behavior in Azure App Service
In most cases, Azure App Service and similar platforms automatically add these headers, and the middleware can process them out of the box.

5.Next Step: Adding the Middleware
To ensure proper behavior, you’ll add the middleware in your application’s startup configuration before deploying to Azure.
  







<<h2>> Configuring the forwarded headers middleware

## Program.cs

builder.Services.Configure<ForwardedHeadersOptions>(options => {
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});


var app = builder.Build(); // Build the app

app.UseForwardedHeaders(); //middleware to process X-Forwarded-For and X-Forwarded-Proto headers








