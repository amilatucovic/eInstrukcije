using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_25.API.Endpoints.LessonEndpoint
{
	public class LessonCreateDTO
	{
			public int StudentID { get; set; }
			public int TutorID { get; set; }
			public int SubjectID { get; set; }
		public string Date { get; set; } // npr. "2025-07-06"
		public string Start { get; set; } // npr. "14:00"
		public string End { get; set; }   // npr. "15:00"
		public LessonMode LessonMode { get; set; }

	}
}
