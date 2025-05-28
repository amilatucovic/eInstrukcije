namespace RS1_2024_2025.API.Endpoints.LoginEndpoint.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Role { get; set; }

        public string RefreshToken { get; set; }
        public int? TutorId { get; set; }
        public string Username {  get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
        public string Email { get; set; }
	}
}
