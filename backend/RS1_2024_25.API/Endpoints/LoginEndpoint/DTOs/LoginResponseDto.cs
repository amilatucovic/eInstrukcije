using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_2025.API.Endpoints.LoginEndpoint.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Role { get; set; }

        public string RefreshToken { get; set; }
        public int? TutorId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? StudentId { get; set; }
        public string PhoneNumber { get; set; }
        public EducationLevel? EducationLevel { get; set; }
        public string? PreferredMode {  get; set; }
        public string? Grade { get; set; }
		public int? CityId { get; set; }
        public City City { get; set; }
	}
}
