using System;
using System.Data;  // Required for DataTable

class Program
{
    static void Main()
    {
        // Create a new DataTable
        DataTable table = new DataTable("Students");

        // Define columns
        table.Columns.Add("ID", typeof(int));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("Age", typeof(int));

        // Add rows
        table.Rows.Add(1, "Alice", 22);
        table.Rows.Add(2, "Bob", 24);
        table.Rows.Add(3, "Charlie", 21);

        // Display the DataTable
        foreach (DataRow row in table.Rows)
        {
            Console.WriteLine($"{row["ID"]}, {row["Name"]}, {row["Age"]}");
        }
    }
}
--------
  Columns.Add() expects a Type object rather than a primitive data type. So, type(int) returns object which says data table to store integer values
