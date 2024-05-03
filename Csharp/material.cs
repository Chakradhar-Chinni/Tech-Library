- using System - just like import in other programming languages
- using namespace - to bundle classes under same file

- Console.WriteLine() - enters on new line
- Console.Write() - enters on same line

- //single line comment
- /* */  multi line comment
- Variables
  - int, double(floating values), char, string, bool
  - read-only values: const int num1 = 10, const string prefix= “telnet”

- DataTypes
    - every variable must have a dataType
    - bool=1bit, char=2bytes, string=2bytes per character, int, float=4bytes, double, long = 8 bytes
    
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
    - `Console.ReadLine()` method returns a `string`. Therefore, you cannot get information from another data type, such as `int`, so use below format
        - `int age = Convert.ToInt32(Console.ReadLine());`

- Operators
    - Arithmetic: +-*/ ++ —
    - Assignment = += -= *=
    - Comparisions < > ≤ ≥  == ≠
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
    - IndexOf(”E”)
    - mystring[1]

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
