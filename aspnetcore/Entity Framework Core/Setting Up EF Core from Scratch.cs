
<<h2>> Install Nuget Packages
use latest stable version

1. Microsoft.EntityFrameworkCore
2. Microsoft.EntityFrameworkCore.Tools




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




