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







<<h2>> Creating the Repository

1. Create PieRepository, CategoryRepository
2. Program.cs
       changed to 


## Models\PieRepository.cs
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;
        public PieRepository(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            _bethanysPieShopDbContext = bethanysPieShopDbContext;
        }

        public IEnumerable<Pie> AllPies
        {
            get
            {
                return _bethanysPieShopDbContext.Pies.Include(c => c.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _bethanysPieShopDbContext.Pies.Include(c=>c.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie? GetPieById(int PieId)
        {
            return _bethanysPieShopDbContext.Pies.Include(c => c.Category).FirstOrDefault(p=>p.PieId==PieId);
        }

        public IEnumerable<Pie> SearchPies(string searchQuery)
        {
            return _bethanysPieShopDbContext.Pies.Include(c => c.Category).Where(p => p.Name.Contains(searchQuery));
        }       
    }
}



## Models\CategoryRepository.cs
namespace BethanysPieShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;
        
        public CategoryRepository(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            _bethanysPieShopDbContext = bethanysPieShopDbContext;
        }

        public IEnumerable<Category> AllCategories =>       
          _bethanysPieShopDbContext.Categories.OrderBy(p => p.CategoryName);      
    }
}



##Progam.cs
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
