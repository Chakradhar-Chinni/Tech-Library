-- Entity Framework Core for Data Access
 Scaffold - reverese engineering the database
 Never store connection strings into source control

Whats EF???
Microsoft's official Data access for .NET development
Evolution of DotNet Framework. Improved very fast
Its cross-platform  - linux, windows, mac
Its an ORM. Object Relational Mapping. These helps to relate database tables to classes. allows CRUD
Regular ORMs directly connect tables and classes. EFCore maps domain classes with database schema
EF Core can connect to various data sources like RDBMS, Azure Cosmos etc...


- Query workflow in EF
for example, context.Authors.ToList()
a.Express and Execute query
b.EF Core converts the query to SQL Provider. (internally, EF uses hashing and stores the converted query for avoiding repetitions)
c.Query is sent to Sql database using the configuration details
d.Sql database sends the result to EF as rows & columns
e.EF Core Materialize the database results into objects (converting Rowsoolumns to objects)
f. DB context adds tracking details to DbContext instance. This step is also expensive especially when tracking is not necessary
DB context performs all above steps
in-short: write ef query > convert ef to sql > send to sql database > receive the result from sql database > materialize the result > add tracking


- Query ways using EF
LINQ Method Syntax
Linq Query Syntax
Triggering Enumeration - This way is not recommended because of performance considerations. Here,DB connection will be kept open till everything is processed. For example, after fetching a record from db, it need to be validated, sent to some micro-service, till all these operations are completed next value from loop cannot be fetched and db conn stays open for longer time)


	
- Filtering queries in Secure way
1. Instead of opening a pubContext in every method, open a connection at program.cs and use the variable
using PubContext _context = new();
QueryFilters();
void QueryFilters()
{
    var authordata = _context.Authors.Where(a => a.FirstName == "Josie").ToList();
}

2. Parameter creation
a. Don't pass search value directly in query, pass the value using a variable
b. If value is passed directly, it leads to SQL Injection attacks, so use variable to pass the data as EF core will parameterize it
In the above example, instead of passing "Josie". use string firstname="Josie" and pass the variable firstname



- Filtering
a. Paging - use .Skip(100).Take(100) - skip first 100 records and fetch next 100 records
b. Ef Core methods and Linq methods can be used to write queries.
c. All Linq Methods will be translated to EF. So, this is additional step
d. Its best to use hybrid approach by using Linq and EF. use Linq for readability, maintenance, strong typing. To address performance considerations use EF 
 methods when necessary



- Aggregation
First() Single() Last() Count() Min() Max() Average() Sum() FirstOrDefault() SingleOrDefault() LastOrDefault()
each of the above methods have async() as well. FirstAsync() SingleAsync() ...

First() returns only first match, if no match then throws exception
First() Single() Last() throws exception if noresults are found
FirstOrDefault() SingleOrDefault() LastOrDefault() will return result if matches are found. They return null if no results are found 
So, its smart to use FirstOrDefault() methods for aggregations



- Disabling Tracking and DbContexts (refer Query workflow in EF)
var author = context.Authors.AsNoTracking().FirstOrDefault() // As NoTracking() returns a query, not a DbSet

-Disable Tracking at DbContext level such that all queries will not be tracked
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = PubDatabase").UseQueryTrackingBehaviour(QueryTrackingBehaviour.NoTracking);
}



- use tracking in query even if tracking is disabled at DbContext level
1. DbContext Configuration: The UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) method configures the DbContext to disable tracking by default.
2. AsTracking Method: The AsTracking method is used to enable tracking for the specific query, allowing EF Core to track changes to the entities returned by that query.

//ApplicationDbContext.cs
public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("YourConnectionStringHere");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
}

//program.cs
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

class Program
{
    static void Main()
    {
        using (var context = new ApplicationDbContext())
        {
            // This query will not track changes by default
            var noTrackingProducts = context.Products.ToList();
            Console.WriteLine("No Tracking:");
            foreach (var product in noTrackingProducts)
            {
                Console.WriteLine($"{product.ProductId}, {product.Name}, {product.Price}");
            }

            // This query will track changes
            var trackingProducts = context.Products.AsTracking().ToList();
            Console.WriteLine("With Tracking:");
            foreach (var product in trackingProducts)
            {
                Console.WriteLine($"{product.ProductId}, {product.Name}, {product.Price}");
            }
        }
    }
}



- executing raw sql query directly in EF Core (complicated queries, temp tables in sql)
1. FromSqlRaw() or ExecuteSqlRaw() methods to execute stored procedures that create and manipulate temporary tables.
2. FromSqlRaw() - if SP returns rows. Result must be mapped to a DbSet<T> in DbContext


If SP returns data, the result can be mapped to a DTO and exposed via entity class
without parameters:
var summaries = await _context.CustomerSummaries
								.FromSqlRaw("EXEC GetCustomerSummary")
								.ToListAsync();								

with parameters:
var summaries = await _context.CustomerSummaries
						      .FromSqlRaw("EXEC GetCustomerSummaryByRegion @RegionId",new SqlParameter("@RegionId", regionId))
    						  .ToListAsync();

DTO:
public class CustomerSummaryDto
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public int TotalOrders { get; set; }
}

DbContext:
public class AppDbContext : DbContext
{
    public DbSet<CustomerSummaryDto> CustomerSummaries { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Since this DTO is not tied to a table, prevent EF from trying to create one
        modelBuilder.Entity<CustomerSummaryDto>().HasNoKey();
    }
}




var results = context.CustomEntity.FromSqlRaw("EXEC YourStoredProcedure @param1, @param2", parameters).ToList();

3. ExecuteSqlRaw() - if SP doesnot return rows
var result = context.Database.ExecuteSqlRaw("EXEC YourStoredProcedure @param1, @param2", parameters);






<<h2>> DbContext and Entity
Entity: C# class that maps to a database table
DbContext: Manages database operations and configurations. Acts as bridge between Sql and C# app
DbSet: Represents a table in the database


DbContext: Represents a session with the database not when instantiated but it represents the session when 1st interaction is performed
DbContext.ChangeTracker: Manages a collection of entity objects
EntityEntry : State info for each entity. Current Values, Original Values, State enum. Entity & more

Entity: In-memory objects with key(identity) properties that DbContext is aware of 
DbContext contains EntityEntry objects with reference pointers to in-memory objects

The lifetime of a DbContext begins when the instance is created and ends when the instance is disposed. A DbContext instance is designed to be used for a single 
 unit-of-work. This means that the lifetime of a DbContext instance is usually very short.

A typical unit-of-work when using Entity Framework Core (EF Core) involves: 
	Creation of a DbContext instance
	Tracking of entity instances by the context. Entities become tracked by
		Being returned from a query
		Being added or attached to the context
	Changes are made to the tracked entities as needed to implement the business rule
	SaveChanges or SaveChangesAsync is called. EF Core detects the changes made and writes them to the database.
	The DbContext instance is disposed
 (in short), DBContext lifetime is 1.create instance 2.track changes 3.make Changes 4.Save Changes 5.Dispose the DbContext 

- Tracking and Saving Workflow
1. DbContext starts tracking when there is a reason create tracking object. (one example is tracking results of query)
Enum has Unchanged, Added, Modified, Deleted

https://learn.microsoft.com/en-us/ef/core/change-tracking/

Entity instances become tracked when they are:
	Returned from a query executed against the database
	Explicitly attached to the DbContext by Add, Attach, Update, or similar methods
	Detected as new entities connected to existing tracked entities

Entity instances are no longer tracked when:
	The DbContext is disposed
	The change tracker is cleared
	The entities are explicitly detached
