//Custom Exceptions

using System;
class Program
{
  static void Main() 
  {
   int age=20;
   try
   {
        if(age<18)
        {
            throw new AgeException("Age cannot be less than 18");
        }
        Console.WriteLine("Age: {0}",age);
   }
   catch(AgeException ex)
   {
     Console.WriteLine("Custom Exception: {0}",ex.Message);
   }
  }
}

public class AgeException : Exception
{
    public AgeException(string message): base(message) { }
}


//Extension Methods:

- Extension methods allow you to add new methods to an existing type (like int) without modifying its source code.
- It obeys O on SOLID principles

public static class IntExtensions
{
    public static bool IsEven(this int number)
    {
        return number % 2 == 0;
    }
}

class Program
{
    static void Main()
    {
        int x = 10;
        Console.WriteLine(x.IsEven());  // Output: True

        int y = 7;
        Console.WriteLine(y.IsEven());  // Output: False
    }
}
