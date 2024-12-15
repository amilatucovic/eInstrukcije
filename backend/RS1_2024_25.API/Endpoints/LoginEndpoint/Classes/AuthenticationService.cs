using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.Data.Models.Auth;
using RS1_2024_25.API.Endpoints.LoginEndpoint.Interfaces;

namespace RS1_2024_25.API.Endpoints.LoginEndpoint.Classes
{
    public class AuthenticationService:IAuthenticationService
    {
        private readonly ApplicationDbContext _context;

        public AuthenticationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MyAppUser> AuthenticateAsync(string username, string password)
        {
            // Retrieve the user by username
            var user = await _context.MyAppUsers
                .FirstOrDefaultAsync(u => u.Username == username);

            // Check if user exists and the password matches
            if (user == null || user.Password != password) // Hash passwords in a real app
            {
                return null;
            }

            return user;
        }
        //public string GenerateJwtToken(MyAppUser user)
        //{ 
        //    var roles = _userManager.GetRolesAsync(user).Result;
        //    return _tokenService.GenerateToken(user.Username, roles);
        //}
    }
}
