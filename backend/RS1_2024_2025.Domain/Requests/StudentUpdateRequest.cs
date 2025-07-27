using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_2025.Domain.Requests
{
	public class StudentUpdateRequest
	{
		public string? Grade { get; set; }
		public string? PreferredMode { get; set; }
		public EducationLevel? EducationLevel { get; set; }

		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Username { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Email { get; set; }
		public int? CityId { get; set; }
	}
}