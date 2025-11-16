using System.Security.Claims;

namespace JwtAuthDemo.Services
{
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}