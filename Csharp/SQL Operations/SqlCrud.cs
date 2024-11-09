 public void ReadFromTable() {
     string SqlConnectionString = "";

     using (SqlConnection scon = new SqlConnection(SqlConnectionString))
     {
         SqlCommand command = new SqlCommand("SELECT * FROM Purchasing.Vendor", scon);
         
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

//the SqlConnection object scon is created inside a using statement. When the code execution leaves the using block, the Dispose method of SqlConnection is called automatically, which closes the connection.
