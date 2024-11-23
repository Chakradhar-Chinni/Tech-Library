/**************************
Making a GET request using C#
**************************/
//ApiManager.cs
using System;
using System.Net.Http;
using System.Threading.Tasks;
namespace eSystems
{
    public class ApiManager
    {
        private static readonly HttpClient client = new HttpClient();
        public async Task MakeGetRequest()    
        {
            Console.WriteLine("Calling API");
            try
            {
                string url = "https://jsonplaceholder.typicode.com/posts";
                HttpResponseMessage response = await client.GetAsync(url);                
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response:\n {responseBody}");
            }
            catch(Exception e) { Console.WriteLine(e.Message); }
        }
    }
}
//Porgram.cs
using System;
using eSystems;
namespace eSystemsApp
{
    class eSystemsClass
    {
        static async Task Main(String[] args)
        {
            Console.WriteLine("--- Program Execution Started ---\n");          
            ApiManager apimanager = new ApiManager();
            await apimanager.MakeGetRequest();
            Console.WriteLine("\n--- Program Execution completed ---");
        }
    }
}

1. await apimanager.MakeGetRequest(); - This line calls the MakeGetRequest method on the apiManager instance and waits for it to   
   complete asynchronously. The await keyword ensures that the calling method does not block while waiting for 
   MakeGetRequest to finish.
2. private static readonly HttpClient client = new HttpClient(); - This line declares and initializes a HttpClient instance as a 
   static, read-only field within the class. This ensures that the same HttpClient instance is reused throughout the lifetime of the 
   application, which is a good practice to avoid socket exhaustion and improve performance.
3. public async Task MakeGetRequest() - asysn= method type; Task= method return type. This is an asynchronous method that performs some 
   operations without blocking the calling thread. It returns a Task to indicate the asynchronous nature of the method, allowing other 
   code to await its completion.
