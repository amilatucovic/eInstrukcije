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

            // Pronađi tutorId ako je korisnik tutor
            int? tutorId = null;
            if (role == "Tutor")
            {
                var tutor = _context.Tutors.FirstOrDefault(t => t.MyAppUserID == user.ID);
                if (tutor != null)
                    tutorId = tutor.ID;
            }

			int? studentId = null;
			if (role == "Student")
			{
				var student = _context.Students.FirstOrDefault(s => s.MyAppUserID == user.ID);
				if (student != null)
					studentId = student.ID;
			}

			var myappuser=_context.MyAppUsers.FirstOrDefault(u=>u.ID==user.ID);
			var loginResponseDto = new LoginResponseDto()
            {
                Token = token,
                RefreshToken = refreshToken,
                Role = role,
                TutorId = tutorId,
                Username=myappuser?.Username,
                FirstName=myappuser?.FirstName,
                LastName=myappuser.LastName,
                Email=myappuser.Email,
                StudentId=studentId
            };
            return Ok(loginResponseDto);
        }
    }
}
