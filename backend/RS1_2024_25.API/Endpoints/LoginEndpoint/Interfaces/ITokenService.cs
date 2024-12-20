namespace RS1_2024_2025.API.Endpoints.LoginEndpoint.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string username, IList<string> roles);
    }
}
