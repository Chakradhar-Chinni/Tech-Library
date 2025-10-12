using Core.Features.Services;
using Microsoft.AspNetCore.Mvc;

namespace Core.Features.Controllers
{
    [Route("api/home")]
    public class HomeController : ControllerBase,IDisposable
    {
        private readonly ISingletonService _singletonService;
        private readonly IScopedService _scopedService;
        private readonly ITransientService _transientService;
        public HomeController(ISingletonService singleton, IScopedService scoped, 
            ITransientService transient)
        {
            _singletonService = singleton;
            _scopedService = scoped;
            _transientService = transient;
            Console.WriteLine("HomeController instance created");
        }
        [HttpGet]
        public IActionResult Index()
        {
            //return Ok(result);
            return Ok(
                new
                {
                    singleton = _singletonService.GetID(),
                    scoped = _scopedService.GetID(),
                    transient = _transientService.GetID(),

                });
        }
        public void Dispose()
        {
            Console.WriteLine("HomeController instance Destroyed ");
        }
    }
}
