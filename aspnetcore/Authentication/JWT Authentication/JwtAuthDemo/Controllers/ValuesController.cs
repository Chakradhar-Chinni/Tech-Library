using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var user = HttpContext.User.Identity?.Name ?? "anonymous";
            return Ok(new { message = $"Hello {user}, this is protected data." });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult GetAdmin()
        {
            return Ok(new { message = "Admin only data" });
        }
    }
}

