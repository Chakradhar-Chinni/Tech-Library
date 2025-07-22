
## Searching, Filtering, Paging Resources



<h2> Filtering Resources

GET: https://localhost:7167/api/cities?name=Chicago
200 OK
[
  {
    "id": 3,
    "name": "Chicago",
    "description": "The Windy City"
  }
]

Tip: parameters should be passed using ?

GetCities() in citiesController accepts a nullable name. If city name is passed then GetCitiesAsync() in CityInfoRepository.cs will be executed which has .Where() clause
.Where() clause fetches only filtered results from DB which becomes efficient search. So Filtering is applied using .Where

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

        public CitiesController(ILogger<CitiesController> logger, ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities(string? name) //modified
        {
            var cityEntities = await _cityInfoRepository.GetCitiesAsync(name);

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




##/Services/ICityInfoRepository.cs
using CityInfo.API.Entities;
using CityInfo.API.Models;
namespace CityInfo.API.Services
{
    public interface ICityInfoRepository 
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<IEnumerable<City>> GetCitiesAsync(string? name); //added
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);
        Task<bool> CityExistsAsync(int cityId);
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();

        void DeletePointOfInterest(PointOfInterest pointofInterest);
    }
}




##/Services/CityInfoRepository.cs
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
        public async Task<IEnumerable<City>> GetCitiesAsync() //added
        {
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<IEnumerable<City>> GetCitiesAsync(string? name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return await GetCitiesAsync();
            }
            return await _context.Cities
                .Where( c=> c.Name == name)
                .OrderBy(c => c.Name)
                .ToListAsync();
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

        public void DeletePointOfInterest(PointOfInterest pointofInterest)
        {
            _context.PointsOfInterest.Remove(pointofInterest);
        }
    }
}


##/appsettings.development.json
{
  "mailSettings": {
    "mailTo": "user@company.in",
    "mailFrom": "dev-admin@company.in"
  },
  "ConnectionStrings": {
    "CityInfoDBConnectionString": "Data Source=CityInfo.db;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "CityInfo.API.Controllers": "Information",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information" //added
    }
  }
}

modify the setting to Infromation so EF query can be seen in console window (only in dev region)
## Console screen
[20:52:25 DBG] Entity Framework Core 8.0.0 initialized 'CityInfoContext' using provider 'Microsoft.EntityFrameworkCore.Sqlite:8.0.0' with options: None
[20:52:25 DBG] Creating DbConnection.
[20:52:25 DBG] Created DbConnection. (0ms).
[20:52:25 DBG] Opening connection to database 'main' on server 'CityInfo.db'.
[20:52:25 DBG] Opened connection to database 'main' on server 'C:\dotnet\Pluralsight\CityInfo\CityInfo.API\CityInfo.db'.
[20:52:25 DBG] Creating DbCommand for 'ExecuteReader'.
[20:52:25 DBG] Created DbCommand for 'ExecuteReader' (3ms).
[20:52:25 DBG] Initialized DbCommand for 'ExecuteReader' (6ms).
[20:52:25 DBG] Executing DbCommand [Parameters=[@__name_0='?' (Size = 7)], CommandType='Text', CommandTimeout='30']
SELECT "c"."Id", "c"."Description", "c"."Name"
FROM "Cities" AS "c"
WHERE "c"."Name" = @__name_0
ORDER BY "c"."Name"
[20:52:25 INF] Executed DbCommand (5ms) [Parameters=[@__name_0='?' (Size = 7)], CommandType='Text', CommandTimeout='30']
SELECT "c"."Id", "c"."Description", "c"."Name"
FROM "Cities" AS "c"
WHERE "c"."Name" = @__name_0
ORDER BY "c"."Name"
[20:52:25 DBG] Context 'CityInfoContext' started tracking 'City' entity. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see key values.
[20:52:25 DBG] Closing data reader to 'main' on server 'C:\dotnet\Pluralsight\CityInfo\CityInfo.API\CityInfo.db'.
[20:52:25 DBG] A data reader for 'main' on server 'C:\dotnet\Pluralsight\CityInfo\CityInfo.API\CityInfo.db' is being disposed after spending 5ms reading results.
[20:52:25 DBG] Closing connection to database 'main' on server 'C:\dotnet\Pluralsight\CityInfo\CityInfo.API\CityInfo.db'.
[20:52:25 DBG] Closed connection to database 'main' on server 'CityInfo.db' (5ms).
[20:52:25 DBG] List of registered output formatters, in the following order: ["Microsoft.AspNetCore.Mvc.Formatters.HttpNoContentOutputFormatter", "Microsoft.AspNetCore.Mvc.Formatters.StringOutputFormatter", "Microsoft.AspNetCore.Mvc.Formatters.StreamOutputFormatter", "Microsoft.AspNetCore.Mvc.Formatters.NewtonsoftJsonOutputFormatter", "Microsoft.AspNetCore.Mvc.Formatters.XmlDataContractSerializerOutputFormatter"]










<h2> Searching through Resources

/*
GET: https://localhost:7167/api/cities?SearchQuery=City
200 OK
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

GET: https://localhost:7167/api/cities?name=Chicago&searchquery=The

200 OK

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

        public CitiesController(ILogger<CitiesController> logger, ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities(string? name, string? searchQuery) //added searchQuery param
        {
            var cityEntities = await _cityInfoRepository.GetCitiesAsync(name,searchQuery);

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





## /Services/ICityInfoRepository.cs

using CityInfo.API.Entities;
using CityInfo.API.Models;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository 
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<IEnumerable<City>> GetCitiesAsync(string? name,string? searchQuery); //added param
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);
        Task<bool> CityExistsAsync(int cityId);
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();

        void DeletePointOfInterest(PointOfInterest pointofInterest);
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

        public async Task<IEnumerable<City>> GetCitiesAsync(string? name,string? searchQuery) //modified the method logic
        {
            if(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(searchQuery))
            {
                return await GetCitiesAsync();
            }

            var collection = _context.Cities as IQueryable<City>;

            if(!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }

            if(!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a=> a.Name.Contains(searchQuery)
                || (a.Description != null && a.Description.Contains(searchQuery)));
            }

            return await collection.OrderBy(c => c.Name).ToListAsync();
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

        public void DeletePointOfInterest(PointOfInterest pointofInterest)
        {
            _context.PointsOfInterest.Remove(pointofInterest);
        }
    }
}








<h2> Deferred Execution

Query execution occurs after query is constructed
IQueryable<T> - for querying external data sources. Execution is deferred until query is iterated over loop/ToList, TOArray, ToDictionary, Singleton queries
IEnumerable<T> - for querying in-memory data objects








<h2> Paging through Resources

1. collection resource often hrows quite large
2. Paging helps avoid performance issues
3. pass the pagination parameters via query string
4. limit the page size, client should enter 1000 page when there are only 10 pages
5. 1st page will be returned by default if pagination parametrs are not provided

/*

In this case, Total results are 3. 
pagesize=1 means three pages are created as 1 page can contain only 1 result
pagesize=2 means two pages are created. Page 1 will have 2 results. Page 2 will have 3rd result

pagesize=1&pagenumber=3 means 3rd page data will be returned with pagesize 1


GET: https://localhost:7167/api/cities?pagesize=1&pagenumber=1
[
  {
    "id": 3,
    "name": "Chicago",
    "description": "The Windy City"
  }
]


GET: https://localhost:7167/api/cities?pagesize=2&pagenumber=1
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
  }
]

GET: https://localhost:7167/api/cities?pagesize=1&pagenumber=2
[
  {
    "id": 2,
    "name": "Los Angeles",
    "description": "The City of Angels"
  }
]

*/


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
        const int maxCitiesPageSize = 20; //added

        public CitiesController(ILogger<CitiesController> logger, ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities(string? name, string? searchQuery, int pageNumber=1, int pageSize=10) //enhanced
        {
            if(pageSize>maxCitiesPageSize)
            {
                pageSize = maxCitiesPageSize;
            }
            var cityEntities = await _cityInfoRepository.GetCitiesAsync(name,searchQuery,pageNumber,pageSize);

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



## /Services/ICityInfoRepository.cs
using CityInfo.API.Entities;
using CityInfo.API.Models;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository 
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<IEnumerable<City>> GetCitiesAsync(string? name,string? searchQuery, int pageNumber, int pageSize);
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);
        Task<bool> CityExistsAsync(int cityId);
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();

        void DeletePointOfInterest(PointOfInterest pointofInterest);
    }
}




##/Services/CityInfoRepository.cs
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

        public async Task<IEnumerable<City>> GetCitiesAsync(string? name,string? searchQuery, int pageNumber, int pageSize)
        {
            //if(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(searchQuery))
            //{
            //    return await GetCitiesAsync();
            //}

            var collection = _context.Cities as IQueryable<City>;

            if(!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }

            if(!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a=> a.Name.Contains(searchQuery)
                || (a.Description != null && a.Description.Contains(searchQuery)));
            }

            return await collection.OrderBy(c => c.Name)
                .Skip(pageSize * (pageNumber-1))  //.Skip(1) if pageNumber is 2, then 1st page records are skipped. pagesize=1, pagenumber=2
                .Take(pageSize)
                .ToListAsync();
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

        public void DeletePointOfInterest(PointOfInterest pointofInterest)
        {
            _context.PointsOfInterest.Remove(pointofInterest);
        }
    }
}


## //console logger - observe db query formation

[16:07:28 DBG] Creating DbConnection.
[16:07:28 DBG] Created DbConnection. (0ms).
[16:07:28 DBG] Opening connection to database 'main' on server 'CityInfo.db'.
[16:07:28 DBG] Opened connection to database 'main' on server 'C:\dotnet\Pluralsight\CityInfo\CityInfo.API\CityInfo.db'.
[16:07:28 DBG] Creating DbCommand for 'ExecuteReader'.
[16:07:28 DBG] Created DbCommand for 'ExecuteReader' (0ms).
[16:07:28 DBG] Initialized DbCommand for 'ExecuteReader' (1ms).
[16:07:28 DBG] Executing DbCommand [Parameters=[@__p_0='?' (DbType = Int32)], CommandType='Text', CommandTimeout='30']
SELECT "c"."Id", "c"."Description", "c"."Name"
FROM "Cities" AS "c"
ORDER BY "c"."Name"
LIMIT @__p_0 OFFSET @__p_0
[16:07:28 INF] Executed DbCommand (6ms) [Parameters=[@__p_0='?' (DbType = Int32)], CommandType='Text', CommandTimeout='30']
SELECT "c"."Id", "c"."Description", "c"."Name"
FROM "Cities" AS "c"
ORDER BY "c"."Name"
LIMIT @__p_0 OFFSET @__p_0
[16:07:28 DBG] Context 'CityInfoContext' started tracking 'City' entity. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see key values.
[16:07:28 DBG] Closing data reader to 'main' on server 'C:\dotnet\Pluralsight\CityInfo\CityInfo.API\CityInfo.db'.
[16:07:28 DBG] A data reader for 'main' on server 'C:\dotnet\Pluralsight\CityInfo\CityInfo.API\CityInfo.db' is being disposed after spending 1ms reading results.
[16:07:28 DBG] Closing connection to database 'main' on server 'C:\dotnet\Pluralsight\CityInfo\CityInfo.API\CityInfo.db'.
[16:07:28 DBG] Closed connection to database 'main' on server 'CityInfo.db' (1ms).














  <h2> Returning Pagination metadata 

  showin pagenumber, pagezie , previous page, next page in API response
  Header: X-Pagination - add this in controller. After executing API, check headers tab to see the pagination metadata

  /*
    GET: https://localhost:7167/api/cities?pagesize=2&pagenumber=2
    RESPONSE TAB
    [
      {
        "id": 1,
        "name": "New York City",
        "description": "The Big Apple"
      }
    ]
    
    HEADERS TAB
    content-length	63
    content-type	application/json; charset=utf-8
    date	Fri, 13 Jun 2025 11:28:48 GMT
    server	Kestrel
    x-pagination	{"TotalItemCount":3,"TotalPageCount":2,"PageSize":2,"CurrentPage":2}

  */

create
##/Services/PaginationMetadata.cs
namespace CityInfo.API.Services
{
    public class PaginationMetadata
    {

        public int TotalItemCount { get; set; }
        public int TotalPageCount { get; set; }
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }


        public PaginationMetadata(int totalItemCount, int pageSize, int currentPage)
        {

            TotalItemCount = totalItemCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);

        }
    }
}

  
##/Services/ICityInfoRepository.cs

using CityInfo.API.Entities;
using CityInfo.API.Models;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository 
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name,string? searchQuery, int pageNumber, int pageSize); //modified to language construct - Tuple
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);
        Task<bool> CityExistsAsync(int cityId);
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();

        void DeletePointOfInterest(PointOfInterest pointofInterest);
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

        public async Task<(IEnumerable<City>,PaginationMetadata)> GetCitiesAsync(string? name,string? searchQuery, int pageNumber, int pageSize) //modified to language construct - Tuple and added X-pagination header
        {
            //if(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(searchQuery))
            //{
            //    return await GetCitiesAsync();
            //}

            var collection = _context.Cities as IQueryable<City>;

            if(!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }

            if(!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a=> a.Name.Contains(searchQuery)
                || (a.Description != null && a.Description.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collextionToReturn = await collection.OrderBy(c => c.Name)
                .Skip(pageSize * (pageNumber-1))  //.Skip(1) if pageNumber is 2, then 1st page records are skipped. pagesize=1, pagenumber=2
                .Take(pageSize)
                .ToListAsync();

            return (collextionToReturn, paginationMetadata);
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

        public void DeletePointOfInterest(PointOfInterest pointofInterest)
        {
            _context.PointsOfInterest.Remove(pointofInterest);
        }
    }
}
