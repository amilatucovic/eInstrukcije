using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RS1_2024_2025.Domain;
using RS1_2024_2025.Domain.Entities.Models.Auth;
using RS1_2024_2025.API.Endpoints.LoginEndpoint.Interfaces;
using RS1_2024_2025.Database;
using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_2025.API.Endpoints.LoginEndpoint.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApplicationDbContext _context;

        public AuthenticationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MyAppUser> AuthenticateAsync(string username, string password)
        {
            var user = await _context.MyAppUsers
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || user.Password != password) 
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

        public async Task<string> GetUserRoleAsync(MyAppUser user)
        {
            return user.UserType.ToString();
        }
    }
}
