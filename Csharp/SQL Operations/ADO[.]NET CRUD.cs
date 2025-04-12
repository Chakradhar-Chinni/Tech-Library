/*---------------------------- 
Read data from SQL Data Table
------------------------------*/
public void ReadFromTable() {
     string SqlConnectionString = "";
     string query = "SELECT * FROM Purchasing.Vendor";
  
     using (SqlConnection scon = new SqlConnection(SqlConnectionString))
     {
         SqlCommand command = new SqlCommand(query, scon);
         
         try {
             scon.Open();
             using(SqlDataReader reader = command.ExecuteReader())
             {
                 while(reader.Read())
                 {
                     Console.WriteLine("Account Name: "+ reader[2].ToString() + " Rating: "+ reader[3].ToString()); 
                 }
                 }
             }

         catch(Exception ex){
             Console.WriteLine("Exception Occured: " + ex.Message); }
         
         finally {
             Console.WriteLine("Execution Completed"); }
         
     }
 }

1.When the code execution leaves the using block, the Dispose method of SqlConnection,SqlDataReader is called automatically, which closes the connection and manages resources effectively
2.scon object is used to connect to SQL database. scon is created from SqlConnection class
3. command object from SqlCommand class is created with query,scon
4. comman.ExecuteReader() will execute the query and store the result to reader object of SqlDataReader class
5. Read() in SqlDataReader class is used in while loop as it advances the cursor to next row. Initially, the cursor points to previous rows of 0th index or -1 index

/*---------------------------- 
printing entire row 
------------------------------*/
StringBuilder rowOutput = new StringBuilder();
while (reader.Read())
{
    for (int i = 0; i < reader.FieldCount; i++)
    {
        rowOutput.Append(reader[i].ToString() + "\n");
    }
    Console.WriteLine(rowOutput);
}

1. use reader.FieldCount when sql row length is unknown
2. store everything to string builder and print after exiting loop. Its efficient compared to printing inside loop
/*---------------------------- 
Update values in existing SQL Data Table 
------------------------------*/
public void WriteToTable()
{
    string SqlConnectionString = "";
    string query = "UPDATE Purchasing.Vendor SET Name=@Name,AccountNumber=@AccountNumber WHERE BusinessEntityID=1492";

    using(SqlConnection scon = new SqlConnection(SqlConnectionString))
    {
        SqlCommand command = new SqlCommand(query, scon);
        
        command.Parameters.AddWithValue("@Name", "Australia Bike Retailer, CO");
        command.Parameters.AddWithValue("@AccountNumber", "AUSTRALI0004");

        try 
        {   
            scon.Open();
            int RowsAffected = command.ExecuteNonQuery();
            Console.WriteLine("Rows Affected: " + RowsAffected);
        }
        catch(Exception ex)
        {
            Console.WriteLine("Exception is: "+ ex.Message);
        }
        finally{
            Console.WriteLine("---Execution is Completed---");}
    }
}

/*---------------------------- 
Update column Name in SQL Data Table 
------------------------------*/

public void ModifyColumnNames()
{
    string SqlConnectionString = "";
    
     //using Interpolation to achieve dynamicness
    string TableName = "table23";
    string OldColumnName = "Ratings";
    string NewColumnName = "Rating";
    string query = $"EXEC sp_rename '{TableName}.{OldColumnName}','{NewColumnName}','COLUMN' ";
    //string query = "EXEC sp_rename 'table23.Rating Comments','Ratings','COLUMN' ";

    using (SqlConnection scon = new SqlConnection(SqlConnectionString))
    {
        SqlCommand command = new SqlCommand (query, scon);
        try
        {
            scon.Open();
            command.ExecuteNonQuery();
            Console.WriteLine("Column Renamed sucessfully");
        }
        catch(Exception ex) {Console.WriteLine(ex.Message);}
        finally { Console.WriteLine("\n---Execution Completed---"); }
    }
}
/*-------------------------
calling Sql Stored Procedure without params using c#
--------------------------*/
public void callProcedure()
{
    string SqlConnectionString = "";
    string query = "exec uspGetJiraRestApiConfiguration";
    using (SqlConnection scon = new SqlConnection(SqlConnectionString))
    {
        SqlCommand command = new SqlCommand(query, scon);
        try
        {
            scon.Open();
            using(SqlDataReader reader = command.ExecuteReader())
            {
                //use the below approach when column details are known
                while (reader.Read())
                {
                    Console.WriteLine(reader[1].ToString() + "\t" + reader[2].ToString() + "\t" + reader[3].ToString());
                }
                //use this approach when sql table is dymanic
                //while(reader.Read())
                //{
                //    object[] values = new object[reader.FieldCount];
                //    reader.GetValues(values);

                //    for(int i=0;i<values.Length;i++)
                //    {
                //        Console.WriteLine(values[i]+"\t");
                //    }
                //}
            }
            //command.ExecuteNonQuery();
            Console.WriteLine("Procedure execution completed");
        }
        catch (Exception ex) { Console.WriteLine("Exception Occured: " + ex.Message); }
        finally { Console.WriteLine("\nProgam Execution Completed"); }
    }
}

/*-------------------------
calling Sql Stored Procedure with params using c#
--------------------------*/
public void callProcedurewithParams()
{
    string SqlConnectionString = "";
    string countryName = "ITALY";
    string query = $"exec [dbo].[uspGetCountryCode] @issuerCountry={countryName}";
    using (SqlConnection scon = new SqlConnection(SqlConnectionString))
    {
        SqlCommand command = new SqlCommand(query,scon);

        try {
            scon.Open();
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read()) {
                    Console.WriteLine(reader[0].ToString()); }    
            }
            Console.WriteLine("Procedure is called successfully");
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }
}
