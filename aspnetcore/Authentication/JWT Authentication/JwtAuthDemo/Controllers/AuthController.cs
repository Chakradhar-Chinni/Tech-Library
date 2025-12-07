using JwtAuthDemo.Models;
using JwtAuthDemo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JwtAuthDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase, IDisposable
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
/*
Uri: [HttpPost} https://localhost:7048/api/Authentication/login

Request Body:
{
    "Username": "testuser",
    "Password":"password"
}
*/
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // NOTE: Replace this with real user validation (DB, Identity, LDAP, etc.)
            if (request.Username == "testuser" && request.Password == "P@ssw0rd")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, request.Username),
                    new Claim(ClaimTypes.Role, "User")
                };

                var token = _tokenService.GenerateToken(claims);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        public void Dispose()
        {
            Console.WriteLine("AuthController - TokenService destroyed");
        }
    }

}
