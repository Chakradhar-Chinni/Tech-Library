using Core.Features.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.Features.Controllers
{
    [Route("api/home")]
    //[Route("[controller]")]
    public class HomeController : ControllerBase,IDisposable
    {

        [HttpGet]
        public IActionResult Index()
        {
            //return Ok(result);
            return Ok("Success");
        }
        public void Dispose()
        {
            Console.WriteLine("HomeController instance Destroyed ");
        }
    }
}
