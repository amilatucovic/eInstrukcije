using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_2025.Domain.SearchObjects
{
	public class StudentSearchObject
	{
		public string? SearchTerm { get; set; }
		public string? Grade { get; set; }
		public EducationLevel? EducationLevel { get; set; }
		public int? CityId { get; set; }
		public string? PrefferedMode { get;  set; }
		public bool? IsUserIncluded { get; set; }
		public int Page { get; set; } = 1;
		public int PageSize { get; set; } = 5;

	}
}
