
<<h2>> Install Nuget Packages
use latest stable version

1. Microsoft.EntityFrameworkCore // to use ef if app
2. Microsoft.EntityFrameworkCore.Tools // to use migrations
3. Microsoft.EntityFrameworkCore.SqlServer // to use sql



<<h2>> Create DbContext in Project

1. Create new folder /data/ApplicationDbContext.cs
2. ApplicationDbContext derives from DbContext, to use all  EF Core methods
3. constructor takes DbContextOptions<CityInfoContext> as a parameter, which contains configuration like the connection string.
    - It passes the options to the base DbContext class.



## \StudentPortal.Web\Data\ApplicationDbContext.cs

using Microsoft.EntityFrameworkCore;
namespace StudentPortal.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        {

        }
    }
}




<<h2>> Add Entities to DbContext class


## \StudentPortal.Web\Data\ApplicationDbContext.cs

using Microsoft.EntityFrameworkCore;
namespace StudentPortal.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        {
            public DbSet<Students> StudentsTable { get; set; }
            public DbSet<Courses> CoursesTable { get; set; }
        }
    }
}







<<h2>> Program.cs

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
   options.UseSqlServer(
    builder.Configuration["ConnectionStrings: DefaultConnection"]);
});





<<h2>> appsettings.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
  "ConnectionStrings": {
    "DefaultConnection": " Initial Catalog=StudentsDB;Data Source=LocalSystem;persist security info=True;Integrated Security=SSPI; "
  }
}
