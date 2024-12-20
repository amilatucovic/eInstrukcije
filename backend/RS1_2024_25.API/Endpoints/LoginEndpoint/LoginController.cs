using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RS1_2024_2025.API.Endpoints.LoginEndpoint.DTOs;
using RS1_2024_2025.API.Endpoints.LoginEndpoint.Interfaces;

namespace RS1_2024_2025.API.Endpoints.LoginEndpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _authenticationService.AuthenticateAsync(loginRequestDto.Username, loginRequestDto.Password);

            if (user == null)
                return Unauthorized("Invalid credentials.");

            //var token = _authenticationService.GenerateJwtToken(user);

            //var loginResponseDto = new LoginResponseDto
            //{
            //    Token = token,
            //    Role = user.UserType.ToString()
            //};

            //return Ok(loginResponseDto);
            return Ok(new { Message = "Login successful", Username = user.Username });
        }
    }
}
