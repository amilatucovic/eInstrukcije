using RS1_2024_25.API.Data.Models.Auth;

namespace RS1_2024_25.API.Endpoints.LoginEndpoint.Interfaces
{
    public interface IAuthenticationService
    {
        Task<MyAppUser> AuthenticateAsync(string username, string password);
        //string GenerateJwtToken(MyAppUser user);
    }
}
