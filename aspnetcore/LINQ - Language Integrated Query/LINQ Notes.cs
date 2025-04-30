-- LINQ || Language Integrated Query
Agenda: Select, Search, Extract subsets, find common items, joins, grouping, min, aggregation, deferred execution
Prequisites: Generics, Delegates, Lambda Expressions, Extension Methods
1. Query Syntax and Method Syntax are two approaches. Query Syntax is more readable, in few aggregation scenarios 
   only method syntax is possible

public class Employee
{
    public int ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Salary { get; set; }

    public static List<Employee> GetEmployees()
    {
        List<Employee> employees = new List<Employee>()
        {
            new Employee {ID = 1001, FirstName = "Alex", LastName = "J", Salary = 80000 },
            new Employee {ID = 1002, FirstName = "Priyanka", LastName = "Dewangan", Salary = 70000 },
            new Employee {ID = 1003, FirstName = "Hina", LastName = "Sharma", Salary = 80000 },
            new Employee {ID = 1004, FirstName = "Anurag", LastName = "Mohanty", Salary = 90000 },
            new Employee {ID = 1005, FirstName = "Sambit", LastName = "Satapathy", Salary = 100000 },
            new Employee {ID = 1006, FirstName = "Sushanta", LastName = "Jena", Salary = 160000 }
        };
        //adding 1 more employee
        employees.Add(new Employee { ID = 1007, FirstName = "Reeta", LastName = "T", Salary = 60000 });
        return employees;
    }
}
-Select Operator with Query Syntax and Method Syntax

 //Query Syntax
 //Employee.GetEmployees() returns IEnumerable<T> Employee objects then .ToList() converts returned collection to List
 List<Employee> basicQuery = (from emp in Employee.GetEmployees() select emp).ToList();
 foreach (Employee emp in basicQuery)
 {
     Console.WriteLine($"{emp.ID} {emp.FirstName} {emp.LastName} {emp.Salary}");
 }

 //Method Syntax
 IEnumerable<Employee> basicMethod = Employee.GetEmployees().ToList();
 foreach (Employee emp in basicMethod)
 {
     Console.WriteLine($"{emp.ID} {emp.FirstName} {emp.LastName} {emp.Salary}");
 }

-Select Single property 
//Query Syntax
List<int> basicQuery = (from emp in Employee.GetEmployees() select emp.ID).ToList();
foreach(var id in basicQuery) 
{
    Console.WriteLine($"{id}"); 
}

//Method Syntax
IEnumerable<int> basicMethod = Employee.GetEmployees().Select(emp => emp.ID); // query generated but not executed
foreach (var id in basicMethod) 
{
    Console.WriteLine($"{id}");
}
/*
1) Iteration: The Select operator iterates over each element in the source sequence.
2) Transformation: For each element, it applies the transformation logic defined in the lambda expression.
3) Result: It produces a new sequence where each element is the result of the applied transformation on the corresponding element from the source sequence
*/
cont from: How do you Select a Few Properties to a Different class using a LINQ Select Operator?

- Select only few properties
//Query Syntax-1, storing result into IEnumerable<T>
IEnumerable<Employee> myquery =  (from emp in Employee.GetEmployees() select new Employee()
                                                                      {
                                                                        FirstName = emp.FirstName,
                                                                        LastName  = emp.LastName,
                                                                        Salary = emp.Salary
                                                                      });
foreach(var emp in myquery)
{
    Console.WriteLine($"{emp.LastName} {emp.FirstName}");
}

//Query Syntax-2, storing result into List
List<Employee> querylist = (from emp in Employee.GetEmployees()
 select new Employee()
 {
     FirstName = emp.FirstName,
     LastName = emp.LastName,
     Salary = emp.Salary
 }).ToList();

foreach(Employee emp in querylist)
{
    Console.WriteLine($"{emp.FirstName} {emp.LastName}");
}

//Method Syntax-1, uses lambda & stores the result to list
List<Employee> selectMethod = Employee.GetEmployees().
                              Select(emp => new Employee()
                              {
                                  FirstName = emp.FirstName,
                                  LastName = emp.LastName,
                                  Salary = emp.Salary
                              }).ToList();
foreach(Employee emp in selectMethod)
{
    Console.WriteLine(emp.FirstName);
}

- Project data as anonymous type
1. After new keyword nothing is mentioned which makes the data as anonymous
2. FullName can be projected because of anonymous type, for non-anonymous type its nor possible to introduce new fields
3. Salary * 12 is also possible because of anonymous type, it is not possible for non-anonymous types
//Query Syntax
var myquery =  (from emp in Employee.GetEmployees() select new
                                                          {
                                                            FirstName = emp.FirstName,
                                                            LastName  = emp.LastName,
                                                            FullName = emp.FirstName +" "+ emp.LastName, 
                                                            Salary = emp.Salary
                                                          });
foreach(var emp in myquery)
{
    Console.WriteLine($" {emp.FirstName} {emp.LastName} {emp.Salary} {emp.FullName}");
}          
Console.WriteLine(myquery.GetType());
           
//Method Syntax
var selectMethod = Employee.GetEmployees().
                              Select(emp => new 
                              {
                                  FirstName = emp.FirstName,
                                  LastName = emp.LastName,
                                  Salary = emp.Salary * 12
                              }).ToList();
foreach(var emp in selectMethod)
{
    Console.WriteLine($" {emp.FirstName} {emp.LastName} {emp.Salary}");
}

// Linq #2 SelectMany
1. SelectMany provides method syntax only. Query Syntax can be in-directly achieved

a. SelectMany basic example

List<String> names = new List<String>();
names.Add("DOT");
names.Add("NET");

//Query Syntax
IEnumerable<char> querysyntax = from str in names
                                from ch in str
                                select ch;
foreach (char ch in querysyntax)
{
    Console.WriteLine(ch);
}

//Method Syntax
IEnumerable<char> methodsyntax = names.SelectMany(x => x);
foreach(char ch in methodsyntax)
{
    Console.WriteLine(ch);
}

- LINQ #3 Where Method
1. Where clause use predicate to 

// Using Where clause with one condition
List<int> numbers = new List<int>() {1,2,3,4,5,6,7,8,9,10 };

//Method Syntax
IEnumerable<int> result = numbers.Where(num => num %2 != 0);

//Query Syntax
IEnumerable<int> result = from num in numbers
                          where num % 2 != 0
                          select num;

foreach (var res in result)
{
    Console.WriteLine(res);
}

// Where clause with two conditions
 List<int> numbers = new List<int>() {1,2,3,4,5,6,7,8,9,10 };

 //Method Syntax
 IEnumerable<int> result = numbers.Where(num => num > 5 && num != 8);

 //Query Syntax
 IEnumerable<int> result = from num in numbers
                           where num > 5 && num != 8
                           select num;

 foreach (var res in result)
 {
     Console.WriteLine(res);
 }

// Where clause with complex data type

//Where clause with indexes
//Where clause with second overload


- LINQ #4 OfType()
1. Linq OfType() is used to check the data type of given value. 
2. If it matches given data type then the value is added to output otherwise it will be skipped
3. when there is a mix of data in collection, use OfType() to filter out. This also avoids InvalidCase Exception scenarios
4. OfType() is used for IEnumerable only not for IEnumerable<T> as OfType objective is to identify the datatype
  
//Basic Example
List<object> datalist = new List<object>() {1,2,3,"Alex",4,5,6,7,8,9,10,"Jack" }; //Alex Jack
IEnumerable<int> result = datalist.OfType<int>(); //1 2 3 4 5 6 7 8 9 10

foreach (var res in result)
{
    Console.WriteLine(res);
} //Output:

// identifying int & strings using LINQ OfType()
List<object> datalist = new List<object>() {1,2,3,"Alex",4,5,6,7,8,9,10,"Jack" };

//Method Syntax
var result = datalist.OfType<string>();

//Query Syntax
var result = (from name in datalist
              where name is string
              select name).ToList();

foreach (var res in result)
{
    Console.Write($"{res} ");
}

//OfType() with Where method
List<object> datalist = new List<object>() {1,2,3,"Alex",4,5,6,7,8,9,10,"Jack" };

//Method Syntax
var result = datalist.OfType<string>().Where(data =>data.Contains('A'));

//Query Syntax
var result = (from name in datalist
              where name is string && ((string)name).Contains('A')
              select name).ToList();

foreach (var res in result)
{
    Console.Write($"{res} ");
}

- LINQ #5 DISTINCT method
1. Distinct is used to return unique elements by removing duplicates.
2. Distinct() is overloaded. one version to return distinct elements, second version is to return distinct elements by ignoring case-sensitivity

//select distinct numbers from List<int>
List<int> numbersdata = new List<int>() {20,30,50,20,90,250,90};
//Method Syntax
var result = numbersdata.Distinct();

//Query Syntax
var result = (from numbers in numbersdata
              select numbers).Distinct();

foreach (var res in result)
{
    Console.Write($"{res} ");
}

//select distinct strings from List<string>
string[] namesArray = { "Priyanka", "HINA", "hina", "Anurag", "Anurag", "ABC", "abc" };

//Method Syntax
var result = namesArray.Distinct(); 

output : Priyanka HINA hina Anurag ABC abc
As case-sensitivity is ignored it returned like above. To include case-sensitivity use overloaded version on distinct or use Anonymous Type for simplicity



- LINQ Except Method
1.This method returns a new sequence containing elements from the first sequence that are not in the second sequence
2. Exception will be thrown if sequence is null

string[] namesArray1 = { "Priyanka", "HINA", "Anurag", "Alex", "Riya" };
string[] namesArray2 = { "Priyanka", "HINA", "Anurag","RIYA" };
int[] numberslist1 = { 20,30,40,32,50,22,28 };
int[] numberslist2 = { 50, 92 };

//Method Syntax  
    var result = numberslist1.Except(numberslist2); //20 30 40 32 22 28

    //case sensitive, output: Alex Riya
    var result = namesArray1.Except(namesArray2);

    //case in-sensitive, output: Alex 
    var result = namesArray1.Except(namesArray2, StringComparer.OrdinalIgnoreCase);


//Query Syntax
    var result = (from numbers in numberslist1 select numbers).Except(numberslist2);

    //case sensitive output: Alex Riya
    IEnumerable<string> result = (from names in namesArray1
                                  select names).Except(namesArray2);

    //case in-sensitive output: Alex
    IEnumerable<string> result = (from names in namesArray1
                                  select names).Except(namesArray2, StringComparer.OrdinalIgnoreCase);

    foreach (var res in result)
    {
        Console.Write($"{res} ");
    }



- LINQ Intersect Method
1. Intersect is used to find common items in both the collections
2. Exception will be thrown if collection is null
3. Intersect provides overloaded version to support case-sensitivity

 string[] namesArray1 = { "Priyanka", "HINA", "Anurag", "Alex", "Riya" };
 string[] namesArray2 = { "Priyanka", "HINA", "Anurag","RIYA" };
 int[] numberslist1 = { 20,30,40,32,50,22,28 };
 int[] numberslist2 = { 50, 92 };

 //Method Syntax queries
     var result = numberslist1.Intersect(numberslist2); //50

     //case sensitive, output: Priyanka HINA Anurag
     var result = namesArray1.Intersect(namesArray2);

     //case in-sensitive, output: Priyanka HINA Anurag Riya
     var result = namesArray1.Intersect(namesArray2, StringComparer.OrdinalIgnoreCase);


 //Query Syntax queries
     var result = (from numbers in numberslist1 select numbers).Intersect(numberslist2); //50

     //case in-sensitive output: Priyanka HINA Anurag
     IEnumerable<string> result = (from names in namesArray1
                                   select names).Intersect(namesArray2);

     //case sensitive output: Priyanka HINA Anurag Riya
     IEnumerable<string> result = (from names in namesArray1
                                   select names).Intersect(namesArray2, StringComparer.OrdinalIgnoreCase);


     foreach (var res in result)
     {
         Console.Write($"{res} ");
     }

-// Complex query
public class Singer
{
    public int ID { get; set; }
    public string Name { get; set; }
}
List<Singer> singerslist1 = new List<Singer>();
List<Singer> singerslist2 = new List<Singer>();

singerslist1.Add(new Singer { ID = 201, Name = "Jack" });
singerslist1.Add(new Singer { ID = 202, Name = "Alex" });
singerslist1.Add(new Singer { ID = 203, Name = "Bob" });

singerslist2.Add(new Singer { ID = 201, Name = "Jack" });
singerslist1.Add(new Singer { ID = 208, Name = "Glen" });

//Method Syntax Output: Jack
var result = singerslist1.Select(x => x.Name).Intersect(
             singerslist2.Select(y => y.Name));

//Query Syntax Output: Jack
var result = (from singer in singerslist1 select singer.Name).Intersect(
    from singer in singerslist2 select singer.Name);

foreach (var res in result)
{
    Console.Write($"{res} ");
}

-  LINQ Union Method
1. Union is used to mix two lists and remove duplicates
2. ArgumentNullException will be thrown if any of the list is null
3. has two overloaded methods - IEqualityComparer

 string[] namesArray1 = { "Priyanka", "HINA", "Anurag", "Alex", "Riya" };
 string[] namesArray2 = { "Priyanka", "HINA", "Anurag","RIYA" };
 int[] numberslist1 = { 20,30,40,32,50,22,28 };
 int[] numberslist2 = { 50, 92 };

//Method Syntax
    var result = numberslist1.Union(numberslist2); // 20 30 40 32 50 22 28 92

    //case sensitive, output: Priyanka HINA Anurag Alex Riya RIYA
    var result = namesArray1.Union(namesArray2);

    //case in-sensitive, output: Priyanka HINA Anurag Alex Riya
    var result = namesArray1.Union(namesArray2, StringComparer.OrdinalIgnoreCase);


//Query Syntax
    var result = (from numbers in numberslist1 select numbers).Union(numberslist2); // 20 30 40 32 50 22 28 92

    //case sensitive output: Priyanka HINA Anurag Alex Riya RIYA
    IEnumerable<string> result = (from names in namesArray1
                                  select names).Union(namesArray2);

    //case in-sensitive output: Priyanka HINA Anurag Alex Riya
    IEnumerable<string> result = (from names in namesArray1
                                  select names).Union(namesArray2, StringComparer.OrdinalIgnoreCase);
