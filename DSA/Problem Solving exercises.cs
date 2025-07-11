<<h2>> Print elements in array 


using System;
using System.Collections.Generic;

class MainApp
{
    static void Main(string[] args)
    {
        int[] values = new int[10];
        
        values[0] = 20;
        values[1] = 30;
        values[2] = 40;
        
        for(int i=0;i<values.Length;i++)
        {
            Console.Write(values[i]+ " ");
        }
        
    }
}


Output: 

20 30 40 0 0 0 0 0 0 0 
