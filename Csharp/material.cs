- using System - just like import in other programming languages
- using namespace - to bundle classes under same file

- Console.WriteLine() - prints on a new line and moves cursor to next line
- Console.Write() - prints on the same line and keeps the cursor on sameline after printing

- //single line comment
- /* */  multi line comment
- Variables
  - int, double(floating values), char, string, bool
  - read-only values: const int num1 = 10, const string prefix= “telnet”

- DataTypes
    - every variable must have a dataType
    - Primitive Data Types: bool=1bit, char=2bytes, string=2bytes per character, int, float=4bytes, double, long = 8 bytes
    - User Defined Data Types: Classes, Structs, Enums, Records, Interfaces, Delegates
    - var : var name ='Bob' compiler determines the data type of variables with var type. Scope is local to methods. Often used in   
            Anonymous types and LINQ queries
    
- Type Casting/ Type Conversion
    - Implicit Casting (automatically) - converting a smaller type to a larger type size
        - `char` -> `int` -> `long` -> `float` -> `double`
        - int num1 = 10
        - double d1 = num1
    - Explicit Casting (manually) - converting a larger type to a smaller size type
        - `double` -> `float` -> `long` -> `int` -> `char`
        - double num2=10.3434343
        - int value = (int) num2

- User Input
    - `Console.ReadLine()` to get user input.
    - `Console.ReadLine()` method returns a `string`. Therefore, you cannot get information from another data type,   
       such as `int`, so use below format
        - `int age = Convert.ToInt32(Console.ReadLine());`

- Operators
    - Arithmetic: +-*/ ++ —
    - Assignment = += -= *=
    - Comparisions/Relational < > ≤ ≥  == ≠
    - Logical &&  || ! (using logical operators can give boolean results, ex: print(10>15) )
- Math
    - Math.Max(5,10) | Math.Min(v1,v2) | Math.sqrt(v1)
- Strings
    - string txt=”my string”
    - string data = “my data”
        - txt.ToUpper()
        - txt.ToLower()
        - string new = txt + data or string new = string.Concat(txt, data)
    - Interpolation
        - string value = $”{txt} {data}”;
    - methods    
    - mystring[1]
    - String Parsing: Split(), SubString(), IndexOf(), LastIndexOf(), Regex.Match(), Join()

- Conditionals
  - if else
  - if {} elseif{} else{}
  - if{} else{}
  - shorthand notation - (condition)? TrueExpression: FalseExpression

  - Switch Case
    - int day = 4
    - switch (day)
    - case 1:
        - print something
        - break;
    - case 2:
    - case 3:
    - case 4:
        - print something
        - break;
    - case 5:
    - default:
        - print something
        - break;

- loops
    - while loop: will execute till condition is true
    - do-while : executes atleast once, only then checks condition
    - foreach loop
    - break, continue:
    - break: breaks loop execution at specific point
    - continue: breaks only that particualar condition and runs rest of them

- Arrays
    - string [] arr1 = {”Ask”,”tell”}

- C# Methods
    - name the method with an uppercase letter like MyMethod()
    - why methods? define the code once and use it many times

    - C# Method Parameters
    - Information can be passed to methods as parameter. Parameters act as variables inside the method.
    - multiple parameters can be passed to method separated by commas. when calling such methods the same number of arguments must be passed in same order

  static void MyMethod(string fname) {
  Console.WriteLine(fname + " Refsnes"); }
  static void Main(string[] args){
  MyMethod("Liam");
  MyMethod("Jenny");
  MyMethod("Anja");}

When a **parameter** is passed to the method, it is called an **argument**. So, from the example above: `fname` is a **parameter**, while `Liam`, `Jenny` and `Anja` are **arguments**.

- Default Parameter : If we call the method without an argument, it uses the default value which is assigned to it with **=** operator

  static void MyMethod(string country = "Norway"){
  Console.WriteLine(country); }
  static void Main(string[] args){
  MyMethod("Sweden");
  MyMethod("India");
  MyMethod();
  MyMethod("USA"); }
//A parameter with a default value, is often known as an "optional parameter". From the example above, country is an optional parameter and "Norway" is the default value.

Optional parameters must be declared after all the required parameters. To pass an argument to only any one of optional parameters use named arguments in key: value syntax.
  public void YourMethod(int reqA, string reqB, int optA = 10, string optB = "Default", bool optC = true)
{
// Method implementation goes here
}
// Calling the method and providing a value only for optC
YourMethod(5, "Hello", optC: false);
for named arguments, maintaining order of arguments is not required

- C# Method Overloading    
    With **method overloading**, multiple methods can have the same name with different parameters:    
    best real life example can be: when buying something, customer can pay through any mode of transaction like liquid cash, UPI, credit / debit card.

public void AcceptPayment(int liquidCash){
// Implementation for payment by liquid cash
}
public void AcceptPayment(int UPI){
// Implementation for payment by UPI
}
public void AcceptPayment(int CreditCard){
// Implementation for payment by Credit Card
}
public void AcceptPayment(int DebitCard){
// Implementation for payment by Debit Card
}

-- Collections

-Dictinoary
Dictionary<int,string> dict = new Dictionary<int,string>();
		dict.Add(3,"South East");
		dict.Add(4,"Western Union");
		Console.WriteLine(dict[4]);
		foreach(KeyValuePair<int,string> kvp in dict)
		{
		    Console.WriteLine($"ID={kvp.Key} Bank={kvp.Value}");
		    //Console.WriteLine("{0}rd is {1}",kvp.Key,kvp.Value); // this is composite syntax used in c# before introducing Interpolation
		}
  dict.Clear() //erases data in dictionary


    

--Development Logics

-Null check for given value, works for excel cells, any other data types
  var value="eg ypt ";
  if(String.IsNullOrWhiteSpace(value.ToString()))
  {
      Console.WriteLine("Data is not available");
  }
  else
  {
      Console.WriteLine("Data is available");
  }
    
--Plural Sight C# Path--
  - Understanding Value Types & Reference Types
    Value Types: Allocated on Stack. int float char double. They have fixed allocation on stack which is provided by complier
    Reference Types: Allocated on Heap. Stack contains pointer to memory adddress
    1. Objects holds the heap memory address of the class. Objects are stored on Stack itself
    2. Strings are reference Types meaning its stored on managed heap memory.
      2a. Immutable: Strings are immutable so any change to existing string will create a new string object in memory. Garbage collector (gc) cleans up unwanted     
          strings. gc collects short lived string quickly while long-lived strings impacts heap performance
      2b. Storage: strings are stored as swquential read-only collection of Char objects. Each char in string occupies 2 byts as C# uses UTF-16 encoding
      2c. Interning: .NET runtime uses Interning to ensure identical string literals are stored only once in the intern pool which reduces memory overhead.
          example:
              string greeting = "Hello, World!";
              string anotherGreeting = "Hello, World!";  
              // Both variables point to the same memory location due to string interning
              bool areSame = object.ReferenceEquals(greeting, anotherGreeting); // true
      2d. When to use StringBuilder? for frequent string concatenations memory usage and performace issues arises as each concatenation creates a new string. So use   
          stringBuilder. StringBuilder is mutable meaning content can be modified on the StringBuilder instance without creating a new instance every time. It has an            Internal buffer to resize only when necessary. uses methods of stringBuilder class. Also, after doing all operations convert the StringBuilder to String               data type to interact with other parts of code and .NET framework that expect immutable string
                          StringBuilder sb = new StringBuilder();
                          sb.Append("Hello, ");
                          sb.Append("World!");
                          sb.AppendLine(); // Adds a newline character
                          string result = sb.ToString(); // Convert the StringBuilder to a string
      2e. String Interpolation also creates some memory overhead(see example below) as it creates temporary objects and integer values are boxed to objects.
          example: string name = "John";
                  string greeting = $"Hello, {name}!";
                  // Internally, above statement is translated into .Format() or .Concat()
                  string greeting = string.Format("Hello, {0}!", name);
  
- Custom Types
  1. VS > View > Object Browser > right click view name spaces > Observe all the in-built namespaces with method details
  2 .Community also develops libraries which can be installed via NuGet packages. For examples NewtonSoft Json. After installing this 3rd party, it would be also   
     visible under Object Browser. (GlobalUsings.cs file will be at debug/net6.0)
 
- notes
int num1; //declared but not initialized
int num1=10; //declared & initialized
//similarly, assume there is a Employee class
public Employee emp; // emp of type Employee is declared
public Employee emp = new Employee(); //initialized

  
- Enumerators (Enums)
  Enums are a way to define set of named constants (read-only variables). Values of enum members must be integers
  Enums can be used in variable declarations, method parameters, return types, and switch statements just like any   
  other data type.
  //Constants.cs
  namespace eSystems
  {
    enum Weeks { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday}
    enum HttpStatus { ServerError = 500, Success = 400, ReDirection = 300 }
  }
  //Program.cs
 Console.WriteLine((int)Weeks.Thursday); //returns 3
 Console.WriteLine((int)HttpStatus.ServerError); //returns 500

//Using Enums in a switch case
  int day = (int)Weeks.Monday;
switch (day)
{
    case (int)Weeks.Monday:
        Console.WriteLine("Its Monday");
        break;
    case (int)Weeks.Tuesday:
        Console.WriteLine("Its Tuesday");
        break;
}



- Struct
1. Struct is a user-definend type, a lightweight alternative to class. Struct donot support inheritance and are stored 
  as value type on stack memory.
2. Static fields are not available for instances they are at class level only
3. department is static field, so the same static value applies for all instances of Employee class.Value can be 
    updated using className Employee.department=""
4. static methods also are at Class level, so class Name should be used to call a static method


 
- Static Data Understanding
//Employee.cs
 public class Employee
 {
     public int eid;
     public string name;
     public static string department = "Sales"; //static field
     public static void sendGreetings() { Console.WriteLine("Welcome"); } // staic method
 }
//Porgram.cs
Employee emp1;
emp1 = new Employee();
emp1.name = "Alex";
emp1.eid = 362254;
Console.WriteLine($"{emp1.name} {emp1.eid} {Employee.department}"); // Alex 362254 Sales

Employee.department = "Clients";
Console.WriteLine($"{emp1.name} {emp1.eid} {Employee.department}"); // Alex 362254 Clients

Employee.sendGreetings(); // calling static method using className instead of instance name



-- Nullable Types
1. Primitive data types cannot hold a null value as they are value types which uses Stack.
2. Its possible to make the primitive data type as null using Nullable operator ? 
3. default value can be assigned using ?? if value of a variable is NULL. (lets say database returns NULL 
   for discountPrice) This can be handled by giving default value to variable using null-coalescing operator ??

  int? number; // int is nullable
  number = null; //null is assigned
  int num1 = number ?? 23; // ?? checks if leftHand side value is null,if null right hand side value will be assigned



-- Records
new reference type from c# 9
prefer records to contain just data, if there is a need to use methods go for classes
features: immutability, concise, value-based equality



-- Arrays
1. To store set of data, variables are not useful. So Arrays or Collection can store set of data
2. Array is a data structure which stores same type of data. Values in an array can be accessed useing 0 based indexing
3. Arrays are reference types, new allocates memory in heap, size of array must be defined during array creation
4. Array Methods: .Reverse() .Copy() .Sort()

example of integer array
int[] array = new int[10];
    for(int i=0;i<=9;i++) {
        array[i] = i;     }
    Console.WriteLine($"{array[0]} , {array[3]}");  

example of string array
String[] duplicates = {"B123","C234","A345","C15","B177","G3003","C235","B179"};
   foreach(string i in duplicates)  {
        if(i.StartsWith('B')) {
        Console.Write(i+"|"); }     }



-- Collections
1. Collections are flexible as size can expand or shrink during runtime. Size need not be defined during creation time

Syntax: List<int> employees = new List<int>();



-- Object Oriented Programming OOPS
Structure: Classes, Objects, Methods, Properties, Interfaces
Four Pillars: Encapsulation, Abstraction, Inheritance, Polymorphism

- Encapsulation
1. Hides internal implemetation & data. Exposes only required information

// simple implementation using get & set
public class Employee
{
    public int ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string salary { get; set; }
}
//program.cs
Employee employee = new Employee();
employee.ID = 10;
employee.FirstName = "ALex";
employee.LastName = "A";
employee.salary = "2500";

Console.WriteLine($"{employee.ID} {employee.FirstName} {employee.LastName} {employee.salary}");


//using get & set methods
1. Private fields can be accessed using get, set methods
2. Object of the class cannot access private variables directly, must navigate through get,set methods
public class Student
{
    private int age;
    private string address;    
    public int Age 
    {
        get { return age; }            
        set  {
            if (value < 0) { age = 12; }
            age = value;  }
    }
    public string Address
    {
        get { return address; }
        set { address = value; }           
    }
}

Program.cs
Student s1;
s1 = new Student();

s1.Age = 21;
s1.Address = "Hogwarts";
Console.WriteLine($"age: {s1.Age} and address: { s1.Address}");

//using get & private set
1. In Private set, value cannot be directly assigned to variable. class object can assign the value using constructor or any method.
2. This approach will greatly help to validate data before assignment
private string address;
public string Address {
    get { return address; }
    private set { address = value; } }

public Student(string initialAddress) { address = initialAddress;    } //constructor

public void updateAddress(string updatedAddress) { //method
    address = "changed to " + updatedAddress; }

- Abstraction



- Inheritance
1. Classes can reuse functionalities from other class. Lower development time because of code reusability
2. Parent/Base class Child/Derived class
  [Recap] Access Modifiers: public, private, protected
3. private type cannot be accessed by any child classes.

  Syntax
  public class ParentClass {}  
  public class ChildClass : ParentClass {}

public class Vehicle
{
    public string Brand { get; set; }
    public void Honk()
    {
        Console.WriteLine("tuut.. tuuy...");
    }        
}
public class Car : Vehicle
{
    public string CarName { get; set; }
}

public class Bike  : Vehicle
{
    public String BikeName { get; set; }
}

//Program.cs
Vehicle v1 = new Vehicle();
v1.Brand = "ford";
Console.WriteLine($"{v1.Brand}");
v1.Honk();



- Understanding Reference Type & Object Type
1. Reference Type: The reference variable myDog is of type Animal. This means it can hold a reference to any object that is an instance of Animal or any of its subclasses (like Dog).
2. Object Type: The actual object created is of type Dog. This means the object in memory is a Dog instance, which includes all the properties and methods of the Dog class, as well as those   
   inherited from the Animal class.
3. Method Call: When myDog.MakeSound() is called, the MakeSound method of the Dog class is executed, even though myDog is of type Animal. This is because the actual object is a Dog, and the Dog      class overrides the MakeSound method from the Animal class. This demonstrates runtime polymorphism. Output: Bark
4. Accessing Subclass Methods: When you create an object of a subclass and assign it to a reference variable of the superclass type, you can only call methods that are defined in the superclass. This is because the reference type determines what methods are accessible, not the object type.
class Animal
{
    public virtual void MakeSound()
    {
        Console.WriteLine("Some generic animal sound");
    }
}

class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Bark");
    }

    public void Fetch()
    {
        Console.WriteLine("Dog is fetching the ball");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Animal myDog = new Dog(); // Reference type is Animal, object type is Dog
        myDog.MakeSound(); // Outputs: Bark

        // myDog.Fetch(); // This line would cause a compile-time error
    }
}



- Polymorphism
Poly&morph
Overriding & OverLoading
--Overriding
1. Use the Reference of Super class and create object of subclass.
2. Revisit Understanding Reference Type & Object Type
 public class Vehicle
 {
     public virtual void Speed()
     {
         Console.WriteLine("General Speed");
     }
 }
 public class Car : Vehicle
 {
     public override void Speed()
     {
         Console.WriteLine("Car speed is 120 km/hr");
     }
 }
 public class Bike : Vehicle
 {
     public override void Speed()
     {
         Console.WriteLine("Bike speed is 80 km/hr");
     }

 } 

//program.cs
Vehicle v = new Bike();
v.Speed();

-Overloading
1. payCabBill() is overloaded with diferent parameters
public class Employee
{
    public void payCabBill()
    {
        Console.WriteLine("Bill Payed");
    }
    public void payCabBill(string mode)
    {
        if (mode == "UPI")
        {
            Console.WriteLine("Bill Paid using UPI");
        }
        if (mode == "Swipe")
        {
            Console.WriteLine("Bill Paid using Swipe Card");
        }
    }
}

//Program.cs
Employee Alex = new Employee();
Alex.payCabBill();
Alex.payCabBill("UPI");
Alex.payCabBill("TouchPay");



-- Interface
  Syntax: public Interface IEmployee {}
          public class Employee : IEmployee
1.Its best practice to use I for an interface name  
2.An Interface specifies what a class should do. A class which implements Interface must provide definitions for all the methods 
3.All methods are public and abstract
4.Cannot contain private access type & cannot contain variables
public interface IEmployee
{
    void PerformWork();
    void ReceiveBonus();
    bool ApplyLeave();
}
public class Employee : IEmployee
{
    public void PerformWork()
    {
        Console.WriteLine("Work is completed");
    }
    public void ReceiveBonus()
    {
        Console.WriteLine("Bonus is added");
    }
    public bool ApplyLeave()
    {
        return true;
    }
}
//program.cs
Employee Bob = new Employee();
Bob.PerformWork();
Bob.ReceiveBonus();
bool status = Bob.ApplyLeave();

string leaveStatus = status ? "Approved" : "Rejected";



-- Events, Event Handler, Delegate, Lambda
1. Events are like notifications. Event Raiser triggers an event and Delegate ensure that it reaches to its destination
2. Events & Delegates go handInhand. Without Delegates Events are of no use
3. Delegate is like class. public delegate just like public class
4. Every Delegate ends with the keyword Handler


- Delegates
1. Syntax is to use delegate keyword with a return type
2. Method must use the same return type, signature of delegate. Delegate & method name can be different
3. A delegate can be reused by many methods
4. Delegates are used to pass methods as parameter to other methods

public class useDelegates
{
    public delegate void MyDelegate(string message);
    public static void PrintMessage(string message)
    {
        Console.WriteLine("Message: " + message);
    }
    public static void Greet(string message)
    {
        Console.WriteLine("Greetings: " + message);
    }

    public static void Main()
    {
        MyDelegate del1 = PrintMessage;
        MyDelegate del2 = Greet;

        del1("Hello.. !!");
        del2("Welcome to eSystems");
    }
}


//Video Encode example
public class VideoEncoder
{
    public delegate void VideoEncodedEventHandler(object source,EventArgs args);
    public event VideoEncodedEventHandler VideoEncoded;
    public void Encode()
    {
        Console.WriteLine("Encoding Video....");
        Thread.Sleep(5);

        OnVideoEncoded();
    }
    protected virtual void OnVideoEncoded()
    {
        if (VideoEncoded != null)
        {
            VideoEncoded(this,EventArgs.Empty);
        }
    }         
}
public class EmailService
{
    public void SendEmail(object source, EventArgs args)
    {
        Console.WriteLine("Email Service: Sending an email....");
    }
}
public class TextService
{
    public void SendText(object source, EventArgs args)
    {
        Console.WriteLine("Text Service: Sending an text....");
    }
}
//program.cs
 VideoEncoder videoencoder = new VideoEncoder();           
 var mailservice = new EmailService();
 var textservice = new TextService();

 videoencoder.VideoEncoded += mailservice.SendEmail; // subscribing mail service
 videoencoder.VideoEncoded += textservice.SendText; // subscribing Text service


 videoencoder.Encode();


-- Lambda Expressions
1. Lambda expressions in C# are a concise way to represent anonymous methods using a special syntax
2. Lambdas are widely used in LINQ Expressions, Func<T, TResult>, Action<T>, Custom Delegates, Event Handlers
3. lambda expression uses the => operator, which is known as the lambda operator
4. Syntax: (parameters) => expression   'for multiple parameters' (parameters) => { statements }

(A) Taking example of Func<T,TResult> with lambda
  Func<int, int> square = x => x * x; //a lambda expression that squares a number.
  int result = square(5); //returns 25

split the above example into 2 parts, left and right side of =
--left side-- Func<int, int> square
left side is a C# Delegate Func<T1,T2,..T16,TResult> that take upto 16 parameters and returns a value
Func<int, int> .Here 1st int represents input parameter while the 2nd is return type
Func<int, int> square .Here squaure is an instance of delegate Func

--right side--  x => x * x
This is the lambda expression assigned to the square delegate.
x is the parameter of the lambda expression.
=> is the lambda operator, which separates the parameter from the body of the expression.
x * x is the body of the lambda expression, which specifies what the lambda does. In this case, it multiplies x by itself.

(B) Taking Example of Action<T> with lambda



-- IEnumerable<T> || Interface
It is used to iterate through collections like list,array using foreach loops
1. IEnumerable has a method GetEnumerator() which returns the Interface IEnumerator
2. IEnumerator has 3 methods: Current()-pointing current element. MoveNext()-for advancing to next element and Reset()-for moving the control 
   back to initial point
3. Generic IEnumerable<T> inherits Non-Generic IEnumerable.So, methods in Non-Generic IEnumerable also needs to be implemented if we are 
   using IEnumerable<T>
4. It is used for in-memory operations like arrays, collections

public class MyCollection<T> : IEnumerable<T>
{
    private List<T> _items = new List<T>();
    public void Add(T item)
    {
        _items.Add(item);
    }
    public IEnumerator<T> GetEnumerator()
    {
        return _items.GetEnumerator();
    }
    
    //Implementation of Non-Generic IEnumerable. 
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator(); //calls GetEnumerator(). So, this enumerator can be used for generic&non-generic
    }
}

//program.cs
var collection = new List<int>();
collection.Add(1);
collection.Add(10);
collection.Add(100);
collection.Add(1000);

foreach(var i in collection)
{
    Console.WriteLine(i);
}



-- IQueryable<T> || Interface
1. It is used for external data source querying with LINQ and uses deferred execution
2. Queries are not executed when they are defined. Instead, they are executed when the query is enumerated (e.g., using foreach or ToList()).
3. IQueryable<T> uses expression trees to represent the query. This allows the query to be translated into a format that the data source 
    understands (e.g., SQL for databases).
4. IQueryable<T> is typically implemented by query providers (e.g., Entity Framework) that translate LINQ queries into the appropriate data source queries.


-- Extension Methods



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




-- Asynchronous Programming
1) When there is a wait time, instead of blocking the main thread, use it for the next task and come back after      
   wait time
2) async and await keywords are used to tell the compiler to work asynchronously
3) async - return type of method which marks the method as asynchronous
4) await - Thread is free to execute another and can come back after wait operation
5) use Task.Delay() is asynchronous as it allows current thread to be freed up to perform other tasks while waiting. Thread.Sleep() is synchronous and blocks the thread for total delay time, so using this method in async approach defeats the purpose.
6) Asynchronous Execution: When you use await, the method doesn't block the thread. Instead, it allows other tasks to run while waiting for the awaited task to complete. This is useful for keeping the application responsive, especially in UI applications or when performing I/O-bound operations.
7) Task Continuation: When an awaited task completes, the method resumes execution from the point where it was awaiting. This ensures that the subsequent code runs only after the awaited task is done.

- Task Implementation without await
1. This implementation is asynchronous. However, after starting SomeMethod() it needs to wait for completion of Wait(). But, without waiting it ended the some method.
2. The idea of async approach is to not block the main thread but not the methods
3. So, to ensure wait is properly handled before executing next line we need to use await , Task
4. SomeMethod() is calling Wait(). Warning will be shown on this line because Wait() is returning a Task which means its a promise. (If Wait() returns void instead of Task, warning will be gone. Still wait is not handled)
5. If we don’t use the await operator while calling the Wait method, the Wait method is not going to wait for this operation to finish, which means that once we call the Wait method, the next line of code inside the SomeMethod is going to be 	 
    executed immediately


public static void Main(String[] args)
 {            
     Console.WriteLine("Main Method Started");
     
     SomeMethod();

     Console.WriteLine("Main Method Ended");
     Console.ReadKey();
 }
 public async static void SomeMethod()
 {
     Console.WriteLine("Some Method Started");
     Wait();

     Console.WriteLine("Some Method Ended");
 }

 public static async Task Wait()
 {
     Console.WriteLine("Wait Time Started");

     await Task.Delay(TimeSpan.FromSeconds(10));

     Console.WriteLine("Wait Time Ended");
 }

Output:
Main Method Started
Some Method Started
Wait Time Started
Some Method Ended
Main Method Ended

Wait Time Ended


- Task Implementation with await
public static void Main(String[] args)
{            
    Console.WriteLine("Main Method Started");
    
    SomeMethod();

    Console.WriteLine("Main Method Ended");
    Console.ReadKey();
}
public async static void SomeMethod()
{
    Console.WriteLine("Some Method Started");
    await Wait();
    Console.WriteLine("Some Method Ended");
}

public static async Task Wait()
{
    Console.WriteLine("Wait Time Started");

    await Task.Delay(TimeSpan.FromSeconds(10));

    Console.WriteLine("\nWait Time Ended");
}

Output:
Main Method Started
Some Method Started
Wait Time Started
Main Method Ended

Wait Time Ended
Some Method Ended
	

- Implementation without waiting for async operation
This is same as 'Task Implementation without await'. Only change is to get rid of warning message change return type of Wait() to void from Task


-- Entity Framework Core for Data Access
 Scaffold - reverese engineering the database
 Never store connection strings into source control

-- Certification freecodecamp & Microsoft--
  Console.Write();
 Console.WriteLine();
--data types
string for words, phrases, or any alphanumeric data for presentation, not calculation
char for a single alphanumeric character
int for a whole number
decimal for a number with a fractional component
bool for a true/false value
var keyword must be initialized not just declaring

--Variables
Variables are containers to store data
declare variable with its data type and use it
naming conventions - use camelCase (underscores are not best in variable names as some programming languages uses it for different reasons)
int accountNumber
string queryResultFromSql
reserved keywords cannot be used as variable names 

--String Formatting
\ escape sequence
\t tab space
\n new line
@ directive

Console.Write("Hello \"World\"!"); - using \ escape sequence to print ""
Console.Write(" "+Convert.ToChar(35)+"World"+Convert.ToChar(35)+"!"); - using ASCII value to print ""

file paths also contain \ but compiler understands it as escape sequence instead of path, to solve this problem use \\ so compiler treats it as \
Console.WriteLine("c:\\source\\repos"); - compiler treats as c:\source\repos
if this is given to compiler, Console.Write(@"C:\source"); then error will be as Unrecognized escape sequence because compiler thinks \s as escape sequence

A better way to handle both escape sequence and filePath is to use directive@ (called as verbatim string literal)
Console.Write(@"C:\source");
just add @ (so that everything gets printed as it is including \n \t)

-String Concatenation
combining two strings or combining string with string literal
// string firstName = "Bob";
// string message = "Hello " + firstName;
// string text = "Hello" + "!";

String Interpolation can be used for concatenation
$
after declaring variables use $ and refer variable names inside {}
// string one = "One";
// string two = "Two";
// String text = $"{one}{two}";
// Console.WriteLine($"Hello {text} boss{text}!");
using string interpolation & directive - first use interpolation then directive (higher precedence)
// char str = '"';
// Console.Write($@"{str}he\llo{str} ");

When string concatenation involves math operation, then include math variables in () to readability
// string firstName = "Bob";
// int widgetsSold = 7;
// Console.WriteLine(firstName + " sold " + (widgetsSold + 7) + " widgets.");

--download & install software
download visual studio code https://code.visualstudio.com/
download dotnet SDK, verify using cmd > dotnet --version

create a folder & open in VScode
 create new dotnet project using the cmd: dotnet new console -o ./CsharpProjects/TestProject
 The driver: dotnet in this example.
 The command: new console in this example.
 The command arguments: -o ./CsharpProjects/TestProject in this example.
program.cs is main file, write sample code here
VsCode > right click on project > click on Integrated terminal to open the path in termiinal

save > dotnet build > dotnet run

save: saves the code to file
dotnet build: generates new build with saved code. also creates new .exe , .dll in \TestProject\bin\Debug\net7.0\
dotnet run: runs the program

class libraries
many class libraries are available for different purposes. In below example, WriteLine is a method in Console class. All, data types are also part of class
  Console.WriteLine("Hello");

Calling methods 
Start by typing the class name. In this case, the class name is Console.
Add the member access operator, the . symbol.
Add the method's name. In this case, the method's name is WriteLine.
Add the method invocation operator, which is a set of parentheses ().
Finally, specify the arguments that are passed to the method, if there are any, between the parentheses of the method invocation operator. In this case, you specify the text that you want the Console.WriteLine() method to write to the console (for example, "Hello World!").

depending on the method, we may need to pass input parameters and accept return value

stateful(Instance) method: 
    depends on values stored in memory, also can create or update data in memory
    stateful methods requires object creation
stateless(static) method:
    doesn't depend on values stored in memory or change anything in memory. 
    Stateless methods can be accessed directly with out using objects [ClassName.Method()]. Eg: Console.WriteLine()

 fun fact: Random.Next() is stateful because the design depends on date,time. It does some fraction and seeds to algorithm to get random value

 --Creating Instance of class 
   //Random dice = new Random();
   //Random dice = new(); //simplified way of creating object called as target-typed new expression.
   instance of a class is called an object.The new operator does several important things:
    It first requests an address in the computer's memory large enough to store a new object based on the Random class.
    It creates the new object, and stores it at the memory address.
    It returns the memory address so that it can be saved in the dice variable.

-- Methods
void methods : executes code but doesn't return any value
Methods can be designed to return any data type or another class
- Often times, the terms 'parameter' and 'argument' are used interchangeably. However, 'parameter' refers to the variable that's being used inside the method. An 'argument' is the value that's passed when the method is called.
-An overloaded method is defined with multiple method signatures. Overloaded methods provide different ways to call the method or provide different types of data.
- Console.Clear()
--Intellisense can help to code more quickly.
--code block {} codewritten inside{} is called code block (terminology)
  Random dice = new Random();
int roll1 = dice.Next(1,10);
int roll2 = dice.Next(1,10);
int roll3 = dice.Next(1,10);

Console.WriteLine($"Rolls : { roll1} {roll2} {roll3 }");

-- String Array initialization 2 types , accessing elements using for loop, using Interpolation to print array elements
using System;
{
    String[] myarray = new string[6];
    
    for(int i=0;i<=4;i++) {
        myarray[i] = i.ToString();}
    myarray[5]="Last index in array";

    Console.WriteLine($"{myarray[0]}, {myarray[1]}, {myarray[2]}, {myarray[3]}, {myarray[4]}, {myarray[5]}");

    //initialize values in array while creating 
    string[] data = { "A123", "B456", "C789" };
    Console.WriteLine(data[0]);
  
  onsole.WriteLine("Printing using for each \n ");
  foreach(string i in myarray) {
        Console.Write(i+ "|"); }
}
-- exercise with string array
using System;
{
   String[] duplicates = {"B123","C234","A345","C15","B177","G3003","C235","B179"};

   foreach(string i in duplicates){
        if(i.StartsWith('B')) {
        Console.Write(i+"|"); }
    }
}
-- int Array initialization
using System;
{
    int[] array = new int[10];
    for(int i=0;i<=9;i++){
        array[i] = I; }
    Console.WriteLine($"{array[0]} , {array[3]}");    

  //for each is simple to use & better when array size is dynamic
    Console.WriteLine("Printing using for each \n ");
    foreach(int i in array)    {
        Console.Write(i + " ");    }
}

Writing readable code is as important as writing efficient code. In Agile environment, after writing code it may be passed to other group for enhancing it, so its best to follow naming conventions & best practices while developing

-- Variable Naming Conventions: use camel case ex: thisIsCamelCase. Avoid Under Scores (_) as they serve different purpose, avoid short form names- use full readable name, avoid numbers/special characters/keywords. Best Examples that even fits into Complex projects/ frameworks: char userOption; int gameScore; float particlesPerMillion; bool processedCustomer;

-- Using code comments: use comments to write meaningful notes, explaining the complexity of code block, trying alternative solutions (remember to update comments if code is updated else it wouldbe meaning less) using comments to explain c# concepts is bad practice

-- use whitespaces for readability, clearness

--Expression: if (myName == "Luiz")
--Evaluating inside print statemet  
Console.WriteLine("a" == "a"); //True
Console.WriteLine("a" == "A"); //False
Console.WriteLine(1 == 2); //False

string myValue = "a";
Console.WriteLine(myValue == "a"); //True

--Using inequality operator
Console.WriteLine("a" != "a"); //False
Console.WriteLine("a" != "A"); //True
Console.WriteLine(1 != 2); //True

string myValue = "a";
Console.WriteLine(myValue != "a"); //False

--Using comparisons
Console.WriteLine(1 > 2); //False
Console.WriteLine(1 < 2); //True
Console.WriteLine(1 >= 1); //True
Console.WriteLine(1 <= 1); //True

-- logical negation /unary operator / not operator
  string pangram = "The quick brown fox jumps over the lazy dog.";
Console.WriteLine(!pangram.Contains("fox")); //False
Console.WriteLine(!pangram.Contains("cow")); //True

--Conditional Operator
COnditional operator can be used instead on nested if conditions fits in
  Syntax: <evaluate this condition> ? <if condition is true, return this value> : <if condition is false, return this value>
int saleAmount = 1001;
int discount = saleAmount > 1000 ? 100 : 50;
Console.WriteLine($"Discount: {discount}");

--Code blocks and variable scope
bool flag = true;
if (flag) {
    int value = 10;
    Console.WriteLine($"Inside the code block: {value}"); }
Console.WriteLine($"Outside the code block: {value}");

Output: Program.cs(7,46): error CS0103: The name 'value' does not exist in the current context

Code:
bool flag = true;
int value = 0;
if (flag){
    Console.WriteLine($"Inside the code block: {value}");}
value = 10;
Console.WriteLine($"Outside the code block: {value}");
Output:
Inside the code block: 0
Outside the code block: 10

-- Switch Case
switch (fruit)
{
    case "apple":
        Console.WriteLine($"App will display information for apple.");
        break;

    case "banana":
        Console.WriteLine($"App will display information for banana.");
        break;

    case "cherry":
        Console.WriteLine($"App will display information for cherry.");
        break;
}
-Code Example
using System;
{
string text="10-MN-L";
string[] model = text.Split('-');
int id = int.Parse(model[0]);

switch(id) {
    case 13:
        Console.WriteLine("its 13");
        break;
    case 10:
        Console.WriteLine("its 10");
        break;
    default:
        Console.WriteLine("None");
        break;
    }
}
-- while loop
int i=10;
while(i<=10) {
    Console.Write(i+"|");
    i++; }
-- do while loop : guarantees that loop executes atleast once (ctrl+esc to exit)
using System;
{
    int i=10;
    do{
        Console.Write("Hi");
    }while(i!=10);
}
The for statement: executes its body while a specified Boolean expression (the 'condition') evaluates to true.
The foreach statement: enumerates the elements of a collection and executes its body for each element of the collection.
The do-while statement: conditionally executes its body one or more times.
The while statement: conditionally executes its body zero or more times.

-- Working with data
Choosing right data type is important to save Heap memory space and to avoid loss of data
Signed & Unsigneddata types
Output
Signed integral types:
sbyte  : -128 to 127
short  : -32768 to 32767
int    : -2147483648 to 2147483647
long   : -9223372036854775808 to 9223372036854775807

Unsigned integral types:
byte   : 0 to 255
ushort : 0 to 65535
uint   : 0 to 4294967295
ulong  : 0 to 18446744073709551615
```
Floating point types:
float  : -3.402823E+38 to 3.402823E+38 (with ~6-9 digits of precision)
double : -1.79769313486232E+308 to 1.79769313486232E+308 (with ~15-17 digits of precision)
decimal: -79228162514264337593543950335 to 79228162514264337593543950335 (with 28-29 digits of precision)

While creating a instance of a class, new keyword creates memory inside heap

The term widening conversion means that you're attempting to convert a value from a data type that could hold less information to a data type that can hold more information. In this case, a value stored in a variable of type int converted to a variable of type decimal, doesn't lose information. Since any int value can easily fit inside of a decimal, the compiler performs the conversion.


  int myInt = 3;
Console.WriteLine($"int: {myInt}");

decimal myDecimal = myInt;
Console.WriteLine($"decimal: {myDecimal}");

If decimal value is assigned to int, it leads to loss of data like below example
decimal myDecimal = 3.14m;
Console.WriteLine($"decimal: {myDecimal}");

int myInt = (int)myDecimal;
Console.WriteLine($"int: {myInt}");

-- Widening Conversion VS Narrowing Conversion
The term narrowing conversion means that you're attempting to convert a value from a data type that can hold more information to a data type that can hold less information. Widening COnversion is exact opposite of it.

  
