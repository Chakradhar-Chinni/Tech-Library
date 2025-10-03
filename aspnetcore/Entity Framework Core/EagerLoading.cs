 EF Core: Eager Loading with Filtering and Pagination

### ✅ **1. Key Concepts**

- **Eager Loading**: Use `.Include(...)` to load related entities (e.g., `Department`, `Designation`, `EmployeeType`) along with the main entity (`Employee`) in a single query.
- **LINQ Query Composition**: LINQ queries are **immutable and composable**. Each method like `.Where(...)` or `.Include(...)` returns a new query object that builds on the previous one. So:
  ```csharp
  query = query.Where(...);
  ```
  **does not overwrite** the previous query — it **adds** to it.

---

### ✅ **2. Full Example with Explanation**

~~~Model Entity

public class Employee
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public int DepartmentId { get; set; }
    public int DesignationId { get; set; }
    public int EmployeeTypeId { get; set; }

    public Department Department { get; set; }
    public Designation Designation { get; set; }
    public EmployeeType EmployeeType { get; set; }
}

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Designation
{
    public int Id { get; set; }
    public string Title { get; set; }
}

public class EmployeeType
{
    public int Id { get; set; }
    public string TypeName { get; set; }
}

```csharp
public async Task<(List<Employee> Employees, int TotalCount)> GetEmployees(
    string? searchTerm,
    int? departmentId,
    int? employeeTypeId,
    int pageNumber,
    int pageSize)
{
    // Step 1: Start with base query and eager loading
    var query = _context.Employees
        .Include(e => e.Department)       // Load Department
        .Include(e => e.Designation)      // Load Designation
        .Include(e => e.EmployeeType)     // Load EmployeeType
        .AsQueryable();                   // Make it composable

    // Step 2: Apply filters if provided
    if (!string.IsNullOrWhiteSpace(searchTerm))
        query = query.Where(e => e.FullName.Contains(searchTerm));

    if (departmentId.HasValue && departmentId.Value > 0)
        query = query.Where(e => e.DepartmentId == departmentId.Value);

    if (employeeTypeId.HasValue && employeeTypeId.Value > 0)
        query = query.Where(e => e.EmployeeTypeId == employeeTypeId.Value);

    // Step 3: Get total count before pagination
    var totalCount = await query.CountAsync();

    // Step 4: Apply pagination and execute query
    var employees = await query
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .AsNoTracking()     // Improves performance
        .ToListAsync();     // Executes the query

    // Step 5: Return results
    return (employees, totalCount);
}
```

---

### 🧪 **Testing the Query**

```csharp
var result = await GetEmployees("John", 2, 1, 1, 10);
foreach (var emp in result.Employees)
{
    Console.WriteLine($"{emp.FullName} - {emp.Department.Name} - {emp.Designation.Title} - {emp.EmployeeType.TypeName}");
}
Console.WriteLine($"Total Count: {result.TotalCount}");
```

---

### 🛠️ **Optional: View SQL Query**

To inspect the generated SQL:

```csharp
string sql = query.ToQueryString();
Console.WriteLine(sql);
```

Or enable logging in `DbContext`:

```csharp
options.UseSqlServer("YourConnectionString")
       .LogTo(Console.WriteLine, LogLevel.Information)
       .EnableSensitiveDataLogging();
```
