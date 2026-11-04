using System.Security.Claims;

namespace DemoShoppe.Application.Abstractions
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
