namespace RS1_2024_25.API.Endpoints.LoginEndpoint.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string username, IList<string> roles);
    }
}
