<<h2>>
Index:
Addng EF to app
Adding Migrations
Seeding data









<<h2>> Adding EF Core to the Applciation

Install Nuget Packages
use latest stable version

1. Microsoft.EntityFrameworkCore // to use ef if app
2. Microsoft.EntityFrameworkCore.Tools // to use migrations
3. Microsoft.EntityFrameworkCore.SqlServer // to use sql




## \Models\BethanysPieShopDbContext.cs

using Microsoft.EntityFrameworkCore;
namespace BethanysPieShop.Models
{
       public class BethanysPieShopDbContext : DbContext
        {
            public BethanysPieShopDbContext(DbContextOptions<BethanysPieShopDbContext> options) : base(options)
            {
            }
                public DbSet<Category> Categories { get; set; }
                public DbSet<Pie> Pies { get; set; }
        }
}
