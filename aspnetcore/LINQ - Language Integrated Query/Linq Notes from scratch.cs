Linq 

Linq types: 
1. Query Syntax - simple, SQL style
2. Method Syntax - easy to read, non-sql style, can support complex queries & non-supported queries in query syntax too
3. Query + Method

--------------------------------
--------------------------------
query syntax:
Initialization - from object in datasource
Condition      - Condition
Selection	   - Selection

query syntax example: 
List<int> integerList = new List<int>();
{
 1,2,3,4,5,6,7,8,9,10
}

Initialization - from obj in integerList
Condition      - where obj > 5
Selection	   - select obj;

var result = from obj in integerList where obj>5 select obj;
--------------------------------
--------------------------------

method syntax:
Initialization - data source
Condition      - 
Selection	   - 

method syntax example:
Initialization - integerList 
Condition      - .Where(obj => obj>5)
Selection	   - .ToList();



method syntax example:
List<int> integerList = new List<int>();
{
 1,2,3,4,5,6,7,8,9,10
}
var result = integerList.Where(obj => obj>5).ToList();
foreach(var item in result)
{
  Console.WriteLine(item);
}
--------------------------------
--------------------------------

IEnumerable vs IQueryable
Both are Interfaces for storing data & performing data manipulations like filtering, ordering

IEnumerable
- system.collections namespace
- in-memory filtering
- small datasets
-Example 
  IEnumerable<Product> products = dbContext.Products;
  var result = products.Where(p => p.Price > 100); // Filtered in C#  
  (sql equivalent) SELECT * FROM Products;

IQueryable
- system.linq namespace
- filtered at external source
- large datasets
- builds an expression tree which the LINQ provider (EF) converts into SQL and executes on the database.
- Example
  IQueryable<Product> products = dbContext.Products;
  var result = products.Where(p => p.Price > 100).ToList(); // filterd in sql
  (sql equivalent) SELECT * FROM Products WHERE Price > 100;

note: 


var list = await q.ToListAsync();            // single SQL query executed on the DB
--------------------------------
--------------------------------
qn: select query with WHERE clause, algorithm used behind it

Filtering Operators - Where, OfType
 
-Where is used to filter based on a condition
 code example:
	 var numbers = new List<int> { 10, 15, 20, 25 };
	 var result = numbers.Where(x => x > 15);

-OfType is used to filter based on data type
 code example:
	var list = new ArrayList{1,"Hi",34,"magic"};
	var ints = list.OfType<int>();
	
Set Operators - Distinct, Except, Intersect, Union, Concat

-Distinct
	- Removes duplicate elements from a sequence
		var nums = new[] { 1, 2, 2, 3, 3, 4 };
		var result = nums.Distinct();
		//output: 1,2,3,4

-Except
	- returns all elements by removing except elements
		var a = new[] {1,2,3,4};
		var b = new[] {3,4,5};
		var result = a.Except(b);

-Intersect	
	- shows common elements
	  var result = a.Intersect(b);

-Union
	shows unique elements by removing duplicates
	var result = a.Union(b);
	
-Concat
	shows all elements from both collections without removing duplicates
	var result = a.Concat(b);


Ordering Operators - OrderBy, OrderByDescending, ThenBy, ThenByDescending, Reverse

-OrderBy
	used to order the list in ascending order
	var a = new[] {2,3,1,4};
	var result = a.OrderBy(x=>x);
	var result = employees.OrderBy(e=>e.Name);

-OrderByDescending
	used to order the list in descending order
	var result = a.OrderByDescending(x=>x);
	var result = employees.OrderByDescending(x=> x);

-ThenBy
	add another level of sorting in ascending order, must be used after OrderBy
	var result = employees.OrderBy(e=>e.Department);
						  .ThenBy(e=> e.Name);

-ThenByDescending
		add another level of sorting in descending order, must be used after OrderByDescending
		var result = employees.OrderBy(e=>e.Department);
						  .ThenByDescending(e=> e.Name);
		
-Reverse
	reverses the entire list
	var result = employees.Reverse();

Aggregate Operators - Sum, Max, Min, Average, Count, Aggregate

-Sum
	to sum elements
	var a = new[]  {1,2,3,4};
	var result = a.Sum(x=>x);
	
-Max
  get maximum element 
  var result = a.Max(x=>x);

-Min
	get minimum element
	var result = a.Min(x=>x);

-Average
    get average of list
	var result = a.Average(x=>x);

-Count 
	get number of elements
	var result = a.Count(x=>x);
	var result = a.Count(x=> x%2==0);
	
	

cont. from Linq Quantifiers
