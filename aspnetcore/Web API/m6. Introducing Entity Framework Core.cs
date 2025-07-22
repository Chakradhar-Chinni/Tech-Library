Introducing Entity Framework Core to the project




<h2> Creating Entities and making table relations using Primary & Foreign key
1. Create New folder Entities

## /Entities/City.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CityInfo.API.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [MaxLength(200)]
        public string? Description { get; set; }

        public ICollection<PointOfInterest> PointsOfInterest { get; set; } = new List<PointOfInterest>();


        public City(string name)
        {
            Name = name;
        }
    }
}


## /Entities/PointOfInterest.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CityInfo.API.Entities
{
    public class PointOfInterest
    {
        [Key] //primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //sql will auto-increment value
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [ForeignKey("CityId")]
        public City? City { get; set; }
        
        public int CityId { get; set; }

        public PointOfInterest(string name)
        {
            Name = name;
        }
    }
}



<h2> Creating DbContext

1. DbContext: represents Session with database
2. Add package : dotnet add package microsoft.entityframeworkcore --version 8.0.0
3. package can be instaled in 3 ways (1) Manage Nuget Packages -GUI (2) Package Manager Console (Install-Package Azure.Identity) (3) CMD line installation (dotnet add package Azure.Identity)
EF core may providers - sql, postgres- check docs
4. Create /DbContexts/CityInfoContext.cs
     - make it a derived class by inheriting DbContext from using Microsoft.EntityFrameworkCore
     - create DbSet for Cities,pointOfInterest
install Microsoft.EntityFrameworkCore.Sqlite (v8.0.0). v8 is compatible with my .NET version of the project
5. Program.cs
    - register the DbContext with Lifetime scope


## /DbContexts/CityInfoContext.cs
using Microsoft.EntityFrameworkCore;
using CityInfo.API.Entities;
namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities {get;set; }
        public DbSet<City> PointOfInterest { get; set; }
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options) { }
    }
}


## Program.cs
builder.Services.AddDbContext<CityInfoContext>(dbContextOptions 
    => dbContextOptions.UseSqlite("Data Source=CityInfo.db"));   //new line 
var app = builder.Build();


<h2> Workign with Migrations
1. Microsoft.EntityFrameworkCore.Tools - for migration add,delete
2. sqlite database can be used with VS using extension
   - Extensions > Manage Extensions > SQLite and SQL Server Compact Toolbox
3. Package manager console > add-migration CityInfoDBInitialMigration
    This will create a new folder "Migrations" with 2 files (1) Migration File - contains operations needed to apply (2)Model Snapshot - a snapshot of current model so EF determines what changes to be made
4. Package Manager console > update-database
   This cmd will update the database with pending migrations
5. Open the database in VS and click on add connection, it will auto fetch .db file
    VS > View > Other > SQLite and SQL Server Compact Toolbox
6. see the tables _EFMigrationshistory, Cities, PointOfInterest are created in DB






<h2> Seeding database with data using OnModelCreating()
1. using OnModelCreating() seed the data to database
2. add-migrations InitialSeeding
3. update-database


## /DbContexts/CityInfoContext.cs

using Microsoft.EntityFrameworkCore;
using CityInfo.API.Entities;
namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {

        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointOfInterest { get; set; }
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                    new City("New York City") { Id = 1, Description = "The Big Apple" },
                    new City("Los Angeles") { Id = 2, Description = "The City of Angels" },
                    new City("Chicago") { Id = 3, Description = "The Windy City" }
                );

            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                new PointOfInterest("Central Park") { Id = 1, CityId = 1, Description = "A large public park in New York City" },
                new PointOfInterest("Hollywood Sign") { Id = 2, CityId = 2, Description = "An iconic landmark in Los Angeles" },
                new PointOfInterest("Willis Tower") { Id = 3, CityId = 3, Description = "A skyscraper in Chicago" },
                new PointOfInterest("Statue of Liberty") { Id = 4, CityId = 1, Description = "A symbol of freedom and democracy" },
                new PointOfInterest("Griffith Observatory") { Id = 5, CityId = 2, Description = "An observatory in Los Angeles" },
                new PointOfInterest("Navy Pier") { Id = 6, CityId = 3, Description = "A popular tourist destination in Chicago" },
                new PointOfInterest("Lincoln Park Zoo") { Id = 7, CityId = 3, Description = "A free zoo in Chicago" },
                new PointOfInterest("The Getty Center") { Id = 8, CityId = 2, Description = "An art museum in Los Angeles" },
                new PointOfInterest("Brooklyn Bridge") { Id = 9, CityId = 1, Description = "A historic bridge in New York City" }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}








<h2> storing Connection Strings safely
1. appsettings is not preferred, because connection strings contain passwords, sometimes it may also have usernames & passwords
2. appsetting goes to source control, so its risky to store connection details
3. in prod region, add it to Sytem > Environment Variables
4. best to use Azure Key Vault - ToDo: implement this 




<h2> SQL Injection Attack

SELECT * FROM Cities WHERE City="Newyork";
Newyork; drop table Cities --
If Sql injection is performed on above string literal, data loss happens
-- is sql comment

 SQL Injectiona attacks can be mitigated by encapsulation and parameterizing SQL commands using DB Parameters

Safe Approaches
 LINQ: 
 .FromSql() (from EF v7.0)
 .FromSqlInterpolated() (before EF v7.0)

 Unsafe
 EF: .FromSqlRaw()

 Learn more: https://learn.microsoft.com/en-us/ef/core/querying/sql-queries?tabs=sqlserver






<h2>Repository Pattern
Decoupling business logic from data access logic




<h2> Implementing Repository Pattern for Data Access using EF Core

Till now, Controller has returned data from in memory object. As DAL is setup using EF Core, lets implement data access directly from database instead of in memory objects

1. Create /Services/ICityInfoRepository.cs
    Interface provides all the method definitions
2. Create /Services/CityInfoRepository.cs
    This class provides implementation for the created Interface


## /Services/ICityInfoRepository.cs
using CityInfo.API.Entities;
namespace CityInfo.API.Services
{
    public interface ICityInfoRepository 
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);
    }
}

## /Services/CityInfoRepository.cs
using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;
        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return await _context.Cities.Include(c => c.PointsOfInterest).Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }

            return await _context.Cities.Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
        {
            return await _context.PointsOfInterest
                            .Where(p => p.CityId == cityId && p.Id == pointOfInterestId)
                            .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await _context.PointsOfInterest
                .Where(p => p.CityId == cityId).ToListAsync();
        }

    }
}

## Program.cs
builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>(); //new line added
var app = builder.Build(); 









<h2> Returning Data from Repository (CitiesController.cs)
1.Create /Models/CityWithoutPointsOfInterestDto.cs
    Cities Table in DB has Id,Name,Description. So, GetCities() will return cities data without PointOfInterest details
    For this reason, CityWithoutPointsOfInterestDto.cs is created without pointofinterest list
2. CitiesController.cs
     ICityInfoRepository is injected to constructor as AddScoped
     It will fetch the data from DB and return
     var cityEntities has the data from DB which might also contain DB metadata or additional columns which is unwanted for the api/GetCities request
     So, results list will hold the properly formatted data. In other words, DB data will be mapped to a DTO. API will return this DTO
     Here City Entity and CityWithoutPointsOfInterestDto are mapped manually. (AutoMapper will be used in next tutorial)
     
    
    /* Here, all cities are ordered by Name as GetCitiesAsync() uses LINQ order by
    GET: https://localhost:7167/api/cities
      [
    {
      "id": 3,
      "name": "Chicago",
      "description": "The Windy City"
    },
    {
      "id": 2,
      "name": "Los Angeles",
      "description": "The City of Angels"
    },
    {
      "id": 1,
      "name": "New York City",
      "description": "The Big Apple"
    }
  ]
*/
  

##/Models/CityWithoutPointsOfInterestDto.cs
namespace CityInfo.API.Models
{
    public class CityWithoutPointsOfInterestDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}

##/Controller/CitiesController.cs
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using CityInfo.API.Services;

namespace CityInfo.API.Controllers
{
    [ApiController]
    //[Route("api/cities")]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        private readonly ICityInfoRepository _cityInfoRepository;
        public CitiesController(ILogger<CitiesController> logger, ICityInfoRepository cityInfoRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities()
        {
            var cityEntities = await _cityInfoRepository.GetCitiesAsync();           
            
            var results = new List<CityWithoutPointsOfInterestDto>();
            foreach(var cityEntity in cityEntities)
            {
                results.Add(new CityWithoutPointsOfInterestDto
                {
                    Id = cityEntity.Id,
                    Description = cityEntity.Description,
                    Name = cityEntity.Name,
                });
            }
         
            _logger.LogInformation("Cities are returned");
            return Ok(results);
        }
    }
}







<h2> Using AutoMapper to map Entities with DTOs - Part 1

- AutoMapper is a Nuget package which helps in mapping Entities with DTOs. Instead of manually mapping, AutoMapper can automatically do this process

Is Mapping necessary? Yes, because Database might contain columns that are unwanted for the request. By mapping Entity to a proper DTO, only the required data will be sent in proper format

1. Tools > Nuget Package Manager Console > Install "AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.0"
2. Program.cs
  register the automapper to Services
3. Create /Profiles folder
4. Create /Profiles/CityProfile.cs
   CityProfile should be derived from Profile. using AutoMapper;
   CreateMap<> from Profile class is used to map entities to DTOs
5. /Controllers/CitiesController.cs
   IMapper is injected in Constructor
   Map() from IMapper is used to Map Entity with DTO and returned
   


## Program.cs
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();


## /Profiles/CityProfile.cs
using AutoMapper; 
namespace CityInfo.API.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.City, Models.CityWithoutPointsOfInterestDto>();
        }
    }
}


## /Controllers/CitiesController.cs
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using CityInfo.API.Services;
using AutoMapper;

namespace CityInfo.API.Controllers
{
    [ApiController]
    //[Route("api/cities")]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ILogger<CitiesController> logger, ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities()
        {
            var cityEntities = await _cityInfoRepository.GetCitiesAsync();
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CityDto>> GetCity(int id)
        {
            //var cityToReturn = _citiesDataStore.Cities.First(c => c.Id == id);
            //if(cityToReturn == null)
            //{
            //    return NotFound();
            //}
            //return Ok(_citiesDataStore.Cities.First(c=> c.Id == id));
            return Ok();
        }
    }
}






<h2> Using AutoMapper to map Entities with DTOs - Part 2
1. Create /Profiles/PointOfInterestProfile.cs
    CreateMap() for Entity.PointOfInterest and Dto
2. /Controllers/CitiesController.cs
    call GetCityAsync() method and return the mapped result
    GetCity() method has 2nd paramater 'bool includePointOfInterest'. For this argument needs to be used as ?includePointOfInterest=true. See below examples

/*
 GET https://localhost:7167/api/cities/2/?includePointOfInterest=false
 {
  "id": 2,
  "name": "Los Angeles",
  "description": "The City of Angels",
  "numberOfPointsOfInterest": 0,
  "pointsOfInterest": []
 }

  GET https://localhost:7167/api/cities/2/?includePointOfInterest=true
  {
    "id": 2,
    "name": "Los Angeles",
    "description": "The City of Angels",
    "numberOfPointsOfInterest": 3,
    "pointsOfInterest": [
      {
        "id": 2,
        "name": "Hollywood Sign",
        "description": "An iconic landmark in Los Angeles"
      },
      {
        "id": 5,
        "name": "Griffith Observatory",
        "description": "An observatory in Los Angeles"
      },
      {
        "id": 8,
        "name": "The Getty Center",
        "description": "An art museum in Los Angeles"
      }
    ]
  }
  
*/



## /Profiles/PointOfInterestProfile.cs
using AutoMapper;
namespace CityInfo.API.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
        }
    }
}



## /Controllers/CitiesController.cs
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using CityInfo.API.Services;
using AutoMapper;

namespace CityInfo.API.Controllers
{
    [ApiController]
    //[Route("api/cities")]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ILogger<CitiesController> logger, ICityInfoRepository cityInfoRepository,IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities()
        {
            var cityEntities = await _cityInfoRepository.GetCitiesAsync();
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCity(int id,bool includePointOfInterest)
        {
            var cityEntity = await _cityInfoRepository.GetCityAsync(id, includePointOfInterest);
            return Ok(_mapper.Map<CityDto>(cityEntity));
        }
    }
}







<h2> Using AutoMapper to map entities - /PointOfInterestController/GetPointsOfInterest(int CityId)

1. CityExistsAsync is added to ICityInfoRepository. Its implementation is provided accordingly
2. .AnyAsync() returns boolean value
3. /Controllers/CitiesController.cs
   CityExistsAsync() checks if city exists and returns pointofinterest details
/*
GET https://localhost:7167/api/cities/1/pointofinterest/
[
  {
    "id": 1,
    "name": "Central Park",
    "description": "A large public park in New York City"
  },
  {
    "id": 4,
    "name": "Statue of Liberty",
    "description": "A symbol of freedom and democracy"
  },
  {
    "id": 9,
    "name": "Brooklyn Bridge",
    "description": "A historic bridge in New York City"
  }
]

*/

## /Services/ICityInfoRepository.cs
using CityInfo.API.Entities;
namespace CityInfo.API.Services
{
    public interface ICityInfoRepository 
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);
        Task<bool> CityExistsAsync(int cityId); //added
    }
}


## /Services/CityInfoRepository.cs
using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;
        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return await _context.Cities.Include(c => c.PointsOfInterest).Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }

            return await _context.Cities.Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
        {
            return await _context.PointsOfInterest
                            .Where(p => p.CityId == cityId && p.Id == pointOfInterestId)
                            .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await _context.PointsOfInterest
                .Where(p => p.CityId == cityId).ToListAsync();
        }
        
        public async Task<bool> CityExistsAsync(int cityId) //added
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId);
        }
    }
}


## /Controllers/CitiesController.cs
namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("/api/cities/{cityId}/pointofinterest")]
    public class PointOfInterestController : ControllerBase
    {

        private readonly ILogger<PointOfInterestController> _logger;
        private readonly ILocalMailService _mailService;
        private readonly CitiesDataStore _citiesDataStore;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointOfInterestController(ILogger<PointOfInterestController> logger,ILocalMailService mailService, CitiesDataStore citiesDataStore
                    ICityInfoRepository cityInfoRepository,IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _citiesDataStore = citiesDataStore;
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId) //modified
        {
            try
            {
                if (! await _cityInfoRepository.CityExistsAsync(cityId))
                {
                    return NotFound();
                }
                var pointsOfInterestForCity = await _cityInfoRepository.GetPointsOfInterestForCityAsync(cityId);
                return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity));
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception Occured while getting points of interest for city with id {cityId}");
                return StatusCode(500, "A problem happened while handling your request");
            }
        }
    }
}








<h2> Creating a Resource - using EF Core

/*
POST: https://localhost:7167/api/cities/1/pointofinterest
Headers: Application/Json
Request Body: {
  "name": "gems collection",
  "description":"many gems"
}

response: 201 Created
{
  "id": 10,
  "name": "gems collection",
  "description": "many gems"
}
*/


## /Controllers/PointOfInterestController.cs
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;
using CityInfo.API.Services;
using Microsoft.VisualBasic.FileIO;
using AutoMapper;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("/api/cities/{cityId}/pointofinterest")]
    public class PointOfInterestController : ControllerBase
    {

        private readonly ILogger<PointOfInterestController> _logger;
        private readonly ILocalMailService _mailService;
        private readonly CitiesDataStore _citiesDataStore;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointOfInterestController(ILogger<PointOfInterestController> logger,ILocalMailService mailService, CitiesDataStore citiesDataStore,
                    ICityInfoRepository cityInfoRepository,IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _citiesDataStore = citiesDataStore;
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

         [HttpPost]
         public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId,PointOfInterestForCreationDto pointOfInterest)
         {
        
             if(! await _cityInfoRepository.CityExistsAsync(cityId))
             {
                 return NotFound();
             }
        
             var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);
             await _cityInfoRepository.AddPointOfInterestForCityAsync(cityId, finalPointOfInterest);        
             await _cityInfoRepository.SaveChangesAsync();        
             var createdPointOfInterestToReturn = _mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);
        
             return CreatedAtRoute("GetPointOfInterest",
             new
             {
                 cityId = cityId,
                 pointOfInterestId = createdPointOfInterestToReturn.Id
             },
                 createdPointOfInterestToReturn);
         }
}


## /Services/ ICityInfoRepository.cs
using CityInfo.API.Entities;
using CityInfo.API.Models;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository 
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);
        Task<bool> CityExistsAsync(int cityId);
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest); //newly added 
        Task<bool> SaveChangesAsync(); //newly added
    }
}


## /Services/CityInfoRepository
using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;
        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return await _context.Cities.Include(c => c.PointsOfInterest).Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }

            return await _context.Cities.Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
        {
            return await _context.PointsOfInterest
                            .Where(p => p.CityId == cityId && p.Id == pointOfInterestId)
                            .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await _context.PointsOfInterest
                .Where(p => p.CityId == cityId).ToListAsync();
        }
        
        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId);
        }

        public async Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest) //newly added
        {
            var city = await GetCityAsync(cityId, false);
            if (city != null)
            {
                city.PointsOfInterest.Add(pointOfInterest);
            }
        }

        public async Task<bool> SaveChangesAsync() //newly added
        {
            return (await _context.SaveChangesAsync() >= 0 );
        }

    }
}



## /Profiles/PointOfInterestProfile.cs
using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
namespace CityInfo.API.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
            CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();
            
        }
    }
}












<h2> Updating Resources 

/*
PUT: https://localhost:7167/api/cities/2/pointofinterest/2

Request Body:
{
 "name":"An iconic landmark in Los Angeles<EF Core>"  
}

Response: 204 No Content


after updating
GET: https://localhost:7167/api/cities/2/pointofinterest/

[
  {
    "id": 2,
    "name": "An iconic landmark in Los Angeles<EF Core>",
    "description": null
  },
]
*/

## /Profiles/PointOfInterestProfile.cs
using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
namespace CityInfo.API.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
            CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();
            CreateMap<Models.PointOfInterestForUpdateDto, Entities.PointOfInterest>();  // newly added
        }
    }
}


## /Controllers/PointOfInterestControllers.cs
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;
using CityInfo.API.Services;
using Microsoft.VisualBasic.FileIO;
using AutoMapper;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("/api/cities/{cityId}/pointofinterest")]
    public class PointOfInterestController : ControllerBase
    {

        private readonly ILogger<PointOfInterestController> _logger;
        private readonly ILocalMailService _mailService;
        private readonly CitiesDataStore _citiesDataStore;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointOfInterestController(ILogger<PointOfInterestController> logger,ILocalMailService mailService, CitiesDataStore citiesDataStore,
                    ICityInfoRepository cityInfoRepository,IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _citiesDataStore = citiesDataStore;
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        [HttpPut("{pointofinterestId}")]
        public async Task<ActionResult> UpdatePointOfInterest(int cityId,int pointOfInterestId, PointOfInterestForUpdateDto pointOfInterest)
        {
            if(! await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }
        
            var pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if(pointOfInterestEntity == null)
            {
                return NotFound();
            }
        
            _mapper.Map(pointOfInterest, pointOfInterestEntity);
        
            await _cityInfoRepository.SaveChangesAsync();
        
            return NoContent();
        }
}











<h2> Partially Updating Resources

/*
PATCH: https://localhost:7167/api/cities/1/pointofinterest/1
Request Body:
[
  {
    "op": "replace",
    "path": "/name",
    "value" : "updated - central park"
  }
]
Response: 204 No Content


before updating
GET: https://localhost:7167/api/cities/1/pointofinterest/

[
  {
    "id": 1,
    "name": "central park",
    "description": "A large public park in New York City"
  },
]

after updating:
GET: https://localhost:7167/api/cities/1/pointofinterest/

[
  {
    "id": 1,
    "name": "updated - central park",
    "description": "A large public park in New York City"
  },
]

*/


## /Profiles/PointOfInterestProfile.cs
using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models;
namespace CityInfo.API.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
            CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();
            CreateMap<Models.PointOfInterestForUpdateDto, Entities.PointOfInterest>();
            CreateMap<Entities.PointOfInterest,Models.PointOfInterestForUpdateDto>(); //newly added
        }
    }
}




## /Controllers/PointOfInterestControllers.cs
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;
using CityInfo.API.Services;
using Microsoft.VisualBasic.FileIO;
using AutoMapper;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("/api/cities/{cityId}/pointofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ILogger<PointOfInterestController> _logger;
        private readonly ILocalMailService _mailService;
        private readonly CitiesDataStore _citiesDataStore;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointOfInterestController(ILogger<PointOfInterestController> logger,ILocalMailService mailService, CitiesDataStore citiesDataStore,
                    ICityInfoRepository cityInfoRepository,IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _citiesDataStore = citiesDataStore;
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPatch("{pointofinterestid}")]
        public async Task<ActionResult> PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId,JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
        
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }
        
            var pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }
        
            var pointOfInterestToPatch = _mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestEntity);
        
            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);
        
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }
        
            _mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);
            await _cityInfoRepository.SaveChangesAsync();
        
            return NoContent();
        }
}




<h2> delete point of interest
/*
DELETE https://localhost:7167/api/cities/1/pointofinterest/4
Response: 204 No Content
*/


## /Services/ICityInfoRepository.cs
using CityInfo.API.Entities;
using CityInfo.API.Models;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository 
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);
        Task<bool> CityExistsAsync(int cityId);
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();
        void DeletePointOfInterest(PointOfInterest pointofInterest); //newly added
    }
}



## /Services/CityInfoRepository.cs

using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;
        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return await _context.Cities.Include(c => c.PointsOfInterest).Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }
            return await _context.Cities.Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
        {
            return await _context.PointsOfInterest
                            .Where(p => p.CityId == cityId && p.Id == pointOfInterestId)
                            .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await _context.PointsOfInterest
                .Where(p => p.CityId == cityId).ToListAsync();
        }
        
        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId);
        }

        public async Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);
            if (city != null)
            {
                city.PointsOfInterest.Add(pointOfInterest);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0 );
        }

        public void DeletePointOfInterest(PointOfInterest pointofInterest) //newly added
        {
            _context.PointsOfInterest.Remove(pointofInterest);
        }
    }
}



## /Controllers/PointOfInterestController.cs
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;
using CityInfo.API.Services;
using Microsoft.VisualBasic.FileIO;
using AutoMapper;
using CityInfo.API.Entities;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("/api/cities/{cityId}/pointofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ILogger<PointOfInterestController> _logger;
        private readonly ILocalMailService _mailService;
        private readonly CitiesDataStore _citiesDataStore;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointOfInterestController(ILogger<PointOfInterestController> logger,ILocalMailService mailService, CitiesDataStore citiesDataStore,
                    ICityInfoRepository cityInfoRepository,IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _citiesDataStore = citiesDataStore;
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
       
       
       [HttpDelete("{pointofinterestid}")]
        public async Task<ActionResult> DeletePointOfInterest(int cityId,int pointofinterestid)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }
        
            var pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointofinterestid);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }
        
            _cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);
        
            _mailService.Send("Point Of Interest deleted", $"{pointOfInterestEntity.Name} is deleted");
        
            return NoContent();
        }
  }
}
