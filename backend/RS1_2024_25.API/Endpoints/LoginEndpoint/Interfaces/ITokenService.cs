using System.Security.Claims;

namespace RS1_2024_25.API.Endpoints.LoginEndpoint.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(int userid, string username,string role);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
