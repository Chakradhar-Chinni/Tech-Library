using System;
class SimpleMaths
{
    public delegate int MathOperation(int a, int b);
 
    
    public int Add(int a, int b)
    {
        return a+b;
    }
    
    public int Difference(int a, int b)
    {
        return a-b;
    }

    public static void Main() 
    {   
       SimpleMaths simpleMaths = new SimpleMaths();
       MathOperation op1 = simpleMaths.Add;
       MathOperation op2 = simpleMaths.Difference;
       Console.WriteLine(op1(2,1));
       Console.WriteLine(op2(4,1));
    }
}

Output: 
3
3
