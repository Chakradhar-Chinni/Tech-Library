
<h2> Dependency Injection using ILogger
1. ILogger is built in service in .NET core, so registration is not required
2. Constructor Injection is preferred way of requesting dependencies. If thats not possible use HttpContext.RequestServices
3. Constructor of CitiesController is injected with _logger to use logging methods
4. private readonly ILogger<CitiesController> _logger; This declares a private, read-only field named _logger that holds an instance of ILogger<CitiesController>. The ILogger interface is used for logging messages, 
     helping developers track events, errors, or other information during runtime.
5. _logger = logger ?? throw new ArgumentNullException(nameof(logger));
  This assigns the logger parameter to the _logger field, but with a safeguard:
  - If logger is null, an ArgumentNullException is thrown.
  - nameof(logger) ensures that the exception message references the correct parameter name, improving debugging.
6. appsettings.json
    CitiesController default logging level is Information, so Information & above will be logged
    PointOfInterestController default logging level is Warning, so Warning above only will be logged
    Logging Levels (Lowest to Highest)
      └── 0 - Trace      - Most detailed logs for debugging
          ├── 1 - Debug      - Debugging-level information
          │   ├── 2 - Information - General application flow details
          │   │   ├── 3 - Warning     - Potential issues that may not cause failure
          │   │   │   ├── 4 - Error       - Issues that prevent normal execution
          │   │   │   │   ├── 5 - Critical    - Severe issues that may crash the application
          │   │   │   │   │   └── 6 - None        - Disables logging



## CitiesController.cs
namespace CityInfo.API.Controllers
{
    [ApiController]
    //[Route("api/cities")]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        public CitiesController(ILogger<CitiesController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            _logger.LogInformation("Cities are returned");
            return Ok(CitiesDataStore.Current.Cities);
        }
    }
}

##appsettings.jsom
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "CityInfo.API.Controllers.CitiesController": "Information",
      "CityInfo.API.Controllers.PointOfInterestController": "Warning"
    }
  },
  "AllowedHosts": "*"
}


##program.cs
builder.Logging.ClearProviders(); //disables log config
builder.Logging.AddConsole(); // enables config for console only







<h2> Logging Exceptions
1. return Statement in catchblock will send message to API consumer. So, don't send any implementation details

##CitiesController.cs
[HttpGet]
public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
{
    try
    {
        throw new Exception("Raised Exception");
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

        if(city == null)
        {
            return NotFound();
        }

        _logger.LogInformation("Points of interest are returned for city with id {cityId}", cityId);
        return Ok(city.PointsOfInterest);
    }
    catch(Exception ex)
    {
        _logger.LogCritical($"Exception Occured while getting points of interest for city with id {cityId}");
        return StatusCode(500, "A problem happened while handling your request");
    }
}

//Exception message on Console
crit: CityInfo.API.Controllers.PointOfInterestController[0]
      Exception Occured while getting points of interest for city with id 1




        
        
        
<h2> Globally Handling and Logging Exceptions

Instead of scattering try-catch blocks throughout the code, you can centralize exception handling using middleware.
summary: add builder.services.AddProgramDetails(); in program.cs to show the error messages in a format


lets raise a exception & see the error message of unhandled exception

## CitiesCOntroller.cs
[HttpGet]
public ActionResult<IEnumerable<CityDto>> GetCities()
{
    throw new Exception("Cities Exception");
    _logger.LogInformation("Cities are returned");
    return Ok(CitiesDataStore.Current.Cities);
}

#region Exception Message
 /*
Error: response status is 500
Response body
System.Exception: Cities Exception
   at CityInfo.API.Controllers.CitiesController.GetCities() in C:\dotnet\Pluralsight\CityInfo\CityInfo.API\Controllers\CitiesController.cs:line 20
   at lambda_method2(Closure, Object, Object[])
   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.SyncObjectResultExecutor.Execute(ActionContext actionContext, IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeActionMethodAsync()
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeNextActionFilterAsync()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)
   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)
   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)
   at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context)

HEADERS
=======
Accept: text/plain
Host: localhost:7167
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/136.0.0.0 Safari/537.36
Accept-Encoding: gzip, deflate, br, zstd
Accept-Language: en-GB,en-US;q=0.9,en;q=0.8
Referer: https://localhost:7167/swagger/index.html
sec-ch-ua-platform: "Windows"
sec-ch-ua: "Chromium";v="136", "Google Chrome";v="136", "Not.A/Brand";v="99"
sec-ch-ua-mobile: ?0
sec-fetch-site: same-origin
sec-fetch-mode: cors
sec-fetch-dest: empty
priority: u=1, i
*/
#endregion

 the above exception is a trace message, which is required for developers only, consumers should never see it. THis can be handled using Exception Middleware in .NET core



##
handling exception with .AddProblemDetails() in program.cs
add ons: just add builder.services.AddProgramDetails(); in program.cs to show the error messages in a format



##Program.cs
using Microsoft.AspNetCore.StaticFiles;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true; //return 406 if the client requests a format that is not supported
}).AddNewtonsoftJson()
   .AddXmlDataContractSerializerFormatters();

builder.Services.AddProblemDetails();  /////////////////////////////////////this line is added

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); //endpoint discovery
builder.Services.AddSwaggerGen(); //integrates Swagger
builder.Services.AddSingleton<FileExtensionContentTypeProvider>(); //FileExtensionContentTypeProvider is used to get the content type of a file based on its extension

var app = builder.Build(); // Build the app

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler();
}

app.UseRouting();
app.UseHttpsRedirection(); //Http are re-directed to Https

app.UseAuthorization();

app.MapControllers(); //Maps Http Requests to controllers

app.UseEndpoints(endpoints =>
  {
      endpoints.MapControllers();
  });
app.Run();


Error Message:
/*
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.6.1",
  "title": "System.Exception",
  "status": 500,
  "detail": "Cities Exception",
  "traceId": "00-dddee0f567d8d026dd669a7918f63eea-2304645274d8a374-00",
  "exception": {
    "valueKind": 1
  }
}
*/






<h2> Setting Up Serilog 
1) .NET core logging provider doesnot support additional features like database logging, file logging. & linux containers dont support in-built logging provider. 
2) So, developers often prefer 3rd parties like nlog, serilog etc..
3) Setting up Serilog & removing default logging mechanism
4) Serilog.AspNetCore,  Serilog.Sinks.Console,  Serilog.Sinks.File

##Program.cs
using CityInfo.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/cityinfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
builder.Host.UseSerilog();

/*  
previous code would work as it is without changes
public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        public CitiesController(ILogger<CitiesController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    } */






<h2> Implemeting a Custom Mail Service and using it via Dependency Injection (concrete class implementation)
1. Create /Services/LocalMailService.cs
2. Register the new class in program.cs so, we just have to inject it in the dependency
3. Inject in Constructor of PointOfInterestController.cs and use it in any ActionREsult methods

##/Services/LocalMailService.cs
namespace CityInfo.API.Services
{
    public class LocalMailService
    {
        private string _mailTo = "admin@company.com";
        private string _mailFrom = "noreply@company.com";

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}

##program.cs
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
builder.Services.AddTransient<LocalMailService>();  //new line
var app = builder.Build(); // Build the app


## PointOfInterestController.cs
namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("/api/cities/{cityId}/pointofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ILogger<PointOfInterestController> _logger;
        private readonly LocalMailService _mailService;

        public PointOfInterestController(ILogger<PointOfInterestController> logger,LocalMailService mailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }
       
       [HttpDelete("{pointofinterestid}")]
       public ActionResult DeletePointOfInterest(int cityId,int pointofinterestid)
       {
           var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
           if(city == null)
           {
               return NotFound();
           }
      
           var pointofInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == pointofinterestid);
           if (pointofInterestFromStore == null)
           {
               return NotFound();
           }
      
           city.PointsOfInterest.Remove(pointofInterestFromStore);
      
           _mailService.Send("Point Of Interest deleted", $"{pointofInterestFromStore.Name} is deleted");
      
           return NoContent();
       }
 }






<h2> Registering a Service by Interface

In Previous module, concrete implementation is provided, now turning that to Interface to promote loose coupling
1. Using Interface for Send()
2. LocalMailService implements ILocalMailService
3. CloudMailService implements ILocalMailService
4. Program.cs - in Debug Mode LocalCloudService will be registered else CloudMailService will be registered 
5. In the Controller, Inject the ILocalMailService instead of Concrete implementation

## /Services/ILocalMailService.cs
namespace CityInfo.API.Services
{
    public interface ILocalMailService
    {
        void Send(string subject, string message);
    }
}


##/Services/LocalMailService.cs
namespace CityInfo.API.Services
{
    public class LocalMailService : ILocalMailService
    {
        private string _mailTo = "admin@company.com";
        private string _mailFrom = "noreply@company.com";

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from Local {_mailFrom} to {_mailTo}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}

##/Services/CloudMailService.cs
namespace CityInfo.API.Services
{
    public class CloudMailService : ILocalMailService
    {
        private string _mailTo = "admin@company.com";
        private string _mailFrom = "noreply@company.com";

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from Cloud {_mailFrom} to {_mailTo}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }

    }
}


##Program.cs
#if DEBUG
builder.Services.AddTransient<ILocalMailService, LocalMailService>();
#else
builder.Services.AddTransient<ILocalMailService, CloudMailService>();
#endif
var app = builder.Build(); // Build the app

## /Controllers/PointOfInterestController.cs
namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("/api/cities/{cityId}/pointofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ILogger<PointOfInterestController> _logger;
        private readonly ILocalMailService _mailService;

        public PointOfInterestController(ILogger<PointOfInterestController> logger,ILocalMailService mailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            try
            {
                //throw new Exception("Raised Exception");
                var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

                if(city == null)
                {
                    return NotFound();
                }

                _mailService.Send("Get Points of Interest", $"Points of interest are returned for city with id {cityId}");
                _logger.LogInformation("Points of interest are returned for city with id {cityId}", cityId);
                return Ok(city.PointsOfInterest);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception Occured while getting points of interest for city with id {cityId}");
                return StatusCode(500, "A problem happened while handling your request");
            }
        }







<h2> turning CitiesDataStore to a service interface

1./CitiesDataStore.cs
  - static thing is diabled 
2. Program.cs
    CitiesDataStore is registed into DI Container
3. /Controllers/CitiesController.cs
   - CitiesDataStore is injected into constructor of the class
   - data in CitiesDataStore can be accessed using _citiesDataStore

##/CitiesDataStore.cs
using CityInfo.API.Models;
using CityInfo.API.Services;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        //public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id=1,
                    Name="Newyork",
                    Description="Newyork is a city in the USA with big park",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name= "Water park",
                            Description="A water park having many themes"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name= "Art Library",
                            Description="Art Library with dozens of paintings"
                        }
                    }
                },
                new CityDto()
                {
                    Id=2,
                    Name="Texas",
                    Description="Texas is a city in the USA",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name= "Central Park",
                            Description="Central Park is most visited place"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name= "Empire Building",
                            Description="Empire Building is most historic"
                        }
                    }
                },
                new CityDto()
                {
                    Id=3,
                    Name="California",
                    Description="California is a city in the USA"
                }
            };
        }
    }
}


##Program.cs
builder.Services.AddSingleton<CitiesDataStore>(); //new line - registering the CitiesDataStore to DI Container
var app = builder.Build(); 



##/Controllers/CitiesController.cs
namespace CityInfo.API.Controllers
{
    [ApiController]
    //[Route("api/cities")]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        private readonly CitiesDataStore _citiesDataStore;
        public CitiesController(ILogger<CitiesController> logger, CitiesDataStore citiesDataStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _citiesDataStore = citiesDataStore;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            //throw new Exception("Cities Exception");
            _logger.LogInformation("Cities are returned");
            return Ok(_citiesDataStore.Cities);
        }
   }
}






<h2> Working with configuration Files
1. json,xml, cmd, env, in-memory are various sources to save data
2. lets see how to use appsettings.json
3. /appsettings.json
   - added mailSettings{}
4. /Services/LocalMailService.cs
   - Constructor is injected with IConfiguration which is by default provided by aspnetcore
   - appsettings.json is expected naming convention & the default to obe used to use the IConfiguration 
   - learn more at src code; aspnetcore/src/DefaultBuilder/WebHost.cs builder.ConfigureCOnfiguration()

## /appsettings.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "CityInfo.API.Controllers.CitiesController": "Information",
      "CityInfo.API.Controllers.PointOfInterestController": "Information"
    }
  },
  "mailSettings": {
    "mailTo": "user@company.in",
    "mailFrom":  "admin@company.in"
  },
  "AllowedHosts": "*"
}



## /Services/LocalMailService.cs
namespace CityInfo.API.Services
{
    public class LocalMailService : ILocalMailService
    {
        private string _mailTo = string.Empty;
        private string _mailFrom = string.Empty;

        public LocalMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:mailTo"];
            _mailFrom = configuration["mailSettings:mailFrom"];
        }
        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from Local {_mailFrom} to {_mailTo}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}


## leveraging environment configuration and scoping configuration
1. right click on project > add > Add new item > search setting > select App Settings File
2. create files, appsettings.Development.json and appsettings.Production.json
3. LaunchSettings.Json > "ASPNETCORE_ENVIRONMENT": "Production" uses production.json and "ASPNETCORE_ENVIRONMENT": "Development" uses development json
4. This flexibility is provided default by aspnetcore to easily switch between dev & production

## appsettings.production.json
{
  "mailSettings": {
    "mailTo": "user@company.in",
    "mailFrom": "prod-admin@company.in"
  }
}


## appsettings.development.json
{
  "mailSettings": {
    "mailTo": "user@company.in",
    "mailFrom": "dev-admin@company.in"
  }
}

## appsettings.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "CityInfo.API.Controllers.CitiesController": "Information",
      "CityInfo.API.Controllers.PointOfInterestController": "Information"
    }
  },
  "AllowedHosts": "*"
}
