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
