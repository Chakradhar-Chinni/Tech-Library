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

1.When the code execution leaves the using block, the Dispose method of SqlConnection,SqlDataReader is called automatically, which closes the connection.
2.scon object is used to connect to SQL database. scon is created from SqlConnection class
3. command object from SqlCommand class is created with query,scon

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
