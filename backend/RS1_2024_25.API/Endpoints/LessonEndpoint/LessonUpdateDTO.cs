using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_25.API.Endpoints.LessonEndpoint
{
	public class LessonUpdateDTO
	{
		public int? StudentId { get; set; }
		public int? TutorId { get; set; }
		public int? SubjectId { get; set; }
		public string? LessonDate { get; set; }
		public string? StartTime { get; set; }
		public string? EndTime { get; set; }
		public LessonStatus? Status { get; set; }
	}
}
