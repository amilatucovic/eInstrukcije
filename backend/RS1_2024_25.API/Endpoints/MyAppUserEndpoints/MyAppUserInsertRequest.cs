using RS1_2024_2025.Domain.Entities;
using RS1_2024_2025.Domain.Entities.Models.Auth;

namespace RS1_2024_2025.API.Endpoints.MyAppUserEndpoints
{
    public class MyAppUserInsertRequest
    {
        
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? CityId { get; set; }
        public UserType UserType { get; set; }
        public string Gender { get; set; }
        public byte[] ProfileImage { get; set; }
    }
}
