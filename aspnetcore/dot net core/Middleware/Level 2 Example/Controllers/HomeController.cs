using Core.Features.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.Features.Controllers
{
    [Route("api/home")]
    //[Route("[controller]")]
    public class HomeController : ControllerBase,IDisposable
    {

        [HttpGet("success")]
        public IActionResult Success()
        {
            //return Ok(result);
            return Ok("Success");
        }

        [HttpGet("error")]
        public IActionResult Error()
        {

            throw new Exception("Simulated Exception");
        }
        public void Dispose()
        {
            Console.WriteLine("HomeController instance Destroyed ");
        }
    }
}

Output:

Console logs:
Exception Middleware before next()
HomeController instance Destroyed
Exception Middleware after next()
Exception Middleware before next()
HomeController instance Destroyed

GET https://localhost:7093/api/home/success
Success
Request took: 2 ms

GET https://localhost:7093/api/home/error
{
  "StatusCode": 200,
  "Message": "Simulated Exception",
  "Path": {
    "Value": "/api/home/error",
    "HasValue": true
  },
  "Method": "GET",
  "Timestamp": "2025-10-13T15:54:26.8038624Z"
}



