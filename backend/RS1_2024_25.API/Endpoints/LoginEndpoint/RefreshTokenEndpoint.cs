using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_2025.Database;
using RS1_2024_25.API.Endpoints.LoginEndpoint.DTOs;
using RS1_2024_25.API.Endpoints.LoginEndpoint.Interfaces;

namespace RS1_2024_25.API.Endpoints.LoginEndpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshTokenEndpoint : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ApplicationDbContext _context;

        public RefreshTokenEndpoint(ITokenService tokenService, ApplicationDbContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request)
        {
            var user = await _context.MyAppUsers
                .FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken);

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return Unauthorized("Invalid or expired refresh token.");

            var role = user.UserType.ToString();
            var newToken = _tokenService.GenerateToken(user.Username, role);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Token = newToken,
                RefreshToken = newRefreshToken
            });
        }
    }
}



