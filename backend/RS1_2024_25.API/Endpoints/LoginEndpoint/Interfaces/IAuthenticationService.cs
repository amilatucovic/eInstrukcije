using RS1_2024_2025.Domain.Entities;
using RS1_2024_2025.Domain.Entities.Models.Auth;

namespace RS1_2024_2025.API.Endpoints.LoginEndpoint.Interfaces
{
    public interface IAuthenticationService
    {
        Task<MyAppUser> AuthenticateAsync(string username, string password);
        //string GenerateJwtToken(MyAppUser user);
    }
}
