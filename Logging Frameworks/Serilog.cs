<h2> SeriLog with Static Instance (enough for small-medium apps)


1. Install both NugetPackages from Nuget Package Manager Console. 
2. Setup Logger in Program.cs (#region code)
3. Now, Serilog is ready to be used in any .cs files. using Serilog is required other .cs files

Nuget Package 1: Serilog
Nuget Package 2: Serilog.Sinks.File

##Program.cs
using Serilog;
namespace SP.OnPrem.Application
{
    class SPOnPrem
    {
        static void Main()
        {
            #region Serilog Setup
            Log.Logger = new LoggerConfiguration()
                             //.WriteToConsole()
                             .WriteTo.File("logs\\logs.txt", rollingInterval: RollingInterval.Day)
                             .CreateLogger();
            #endregion

            Log.Information("Started Sp.OnPremise Application");

            ApiAuthentication apiauthentication = new ApiAuthentication();
            apiauthentication.FetchToken();
        }
    }
}

##ApiAuthentication.cs
using Serilog;
namespace SP.OnPrem
{
    class ApiAuthentication
    {
        public void FetchToken()
        {
            Log.Information("Token fetching started");
        }
    }
}



<h2> Serilog using Dependency Injection (prefer for large scale apps)

Reason to prefer DI for large apps:
1. With DI, you get type-specific loggers (ILogger<MyClass>), which automatically tag logs with the class name.
2. DI allows you to swap logging implementations (e.g., Serilog, NLog, Console) without changing your business logic.
3. ASP.NET Core and .NET Generic Host use DI as a first-class citizen.









