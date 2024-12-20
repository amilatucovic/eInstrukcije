using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RS1_2024_2025.API.Endpoints.LoginEndpoint.DTOs;
using RS1_2024_2025.API.Endpoints.LoginEndpoint.Interfaces;
using RS1_2024_2025.Database;
using RS1_2024_25.API.Endpoints.LoginEndpoint.Interfaces;
namespace RS1_2024_2025.API.Endpoints.LoginEndpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenService _tokenService;
        private readonly ApplicationDbContext _context;
        public LoginController(IAuthenticationService authenticationService, ITokenService tokenService, ApplicationDbContext context)
        {
            _authenticationService = authenticationService;
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _authenticationService.AuthenticateAsync(loginRequestDto.Username, loginRequestDto.Password);

            if (user == null)
                return Unauthorized("Invalid credentials.");

            var role = user.UserType.ToString();
            var token = _tokenService.GenerateToken(user.Username, role);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();

            var loginResponseDto = new LoginResponseDto()
            {
                Token = token,
                RefreshToken = refreshToken,
                Role = role
            };
            return Ok(loginResponseDto);
        }
    }
}
