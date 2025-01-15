using RS1_2024_2025.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RS1_2024_2025.Domain.Requests
{
	public class StudentInsertRequest
	{
		[Required]
		public string Username { get; set; }

		[Required]
		[MinLength(8)]
		public string Password { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		[RegularExpression("^[0-9]+$")]
		[Range(6, 20)]	
		public int Age { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[RegularExpression("^[0-9]+$")]
		[MaxLength(13)]
		[MinLength(9)]
		public string PhoneNumber { get; set; }

		[Required]
		public int CityId { get; set; }
		public UserType UserType { get; set; }

		[Required]
		public string Gender { get; set; }

		[Required]
		[RegularExpression("^[a-zA-Z0-9]*$")]
		public string Grade { get; set; }

		[Required]
		public string PreferredMode { get; set; }

		[Required]
		public EducationLevel EducationLevel { get; set; }
	}
}
