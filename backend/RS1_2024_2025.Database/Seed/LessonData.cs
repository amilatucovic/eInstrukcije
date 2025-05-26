using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_2025.Database.Seed
{
	public static class LessonData
	{
		public static void SeedData(this EntityTypeBuilder<Lesson> entity)
		{
			entity.HasData(	new Lesson { ID = 1, StudentID = 1, TutorID = 1, SubjectID = 10, LessonDate = new DateTime(2025, 6, 10), StartTime = new DateTime(2025, 6, 10, 10, 0, 0), EndTime = new DateTime(2025, 6, 10, 11, 0, 0), Status = LessonStatus.Scheduled, Mode = LessonMode.Online },
			new Lesson { ID = 2, StudentID = 2, TutorID = 1, SubjectID = 1, LessonDate = new DateTime(2025, 6, 10), StartTime = new DateTime(2025, 6, 10, 11, 15, 0), EndTime = new DateTime(2025, 6, 10, 12, 15, 0), Status = LessonStatus.Scheduled, Mode = LessonMode.InPerson },
			new Lesson { ID = 3, StudentID = 3, TutorID = 2, SubjectID = 2, LessonDate = new DateTime(2025, 7, 5), StartTime = new DateTime(2025, 7, 5, 9, 0, 0), EndTime = new DateTime(2025, 7, 5, 10, 0, 0), Status = LessonStatus.Scheduled, Mode = LessonMode.Online },
			new Lesson { ID = 4, StudentID = 4, TutorID = 2, SubjectID = 21, LessonDate = new DateTime(2025, 7, 5), StartTime = new DateTime(2025, 7, 5, 10, 30, 0), EndTime = new DateTime(2025, 7, 5, 11, 30, 0), Status = LessonStatus.Scheduled, Mode = LessonMode.InPerson },
			new Lesson { ID = 5, StudentID = 5, TutorID = 3, SubjectID = 7, LessonDate = new DateTime(2025, 8, 12), StartTime = new DateTime(2025, 8, 12, 14, 0, 0), EndTime = new DateTime(2025, 8, 12, 15, 0, 0), Status = LessonStatus.Scheduled, Mode = LessonMode.Online },
			new Lesson { ID = 6, StudentID = 6, TutorID = 3, SubjectID = 17, LessonDate = new DateTime(2025, 8, 12), StartTime = new DateTime(2025, 8, 12, 15, 15, 0), EndTime = new DateTime(2025, 8, 12, 16, 15, 0), Status = LessonStatus.Scheduled, Mode = LessonMode.InPerson },
			new Lesson { ID = 7, StudentID = 7, TutorID = 4, SubjectID = 12, LessonDate = new DateTime(2025, 9, 1), StartTime = new DateTime(2025, 9, 1, 13, 0, 0), EndTime = new DateTime(2025, 9, 1, 14, 0, 0), Status = LessonStatus.Scheduled, Mode = LessonMode.Online },
			new Lesson { ID = 8, StudentID = 1, TutorID = 4, SubjectID = 9, LessonDate = new DateTime(2025, 9, 1), StartTime = new DateTime(2025, 9, 1, 14, 15, 0), EndTime = new DateTime(2025, 9, 1, 15, 15, 0), Status = LessonStatus.Scheduled, Mode = LessonMode.InPerson },
			new Lesson { ID = 9, StudentID = 2, TutorID = 5, SubjectID = 3, LessonDate = new DateTime(2025, 6, 25), StartTime = new DateTime(2025, 6, 25, 9, 0, 0), EndTime = new DateTime(2025, 6, 25, 10, 0, 0), Status = LessonStatus.Scheduled, Mode = LessonMode.Online },
			new Lesson { ID = 10, StudentID = 3, TutorID = 5, SubjectID = 6, LessonDate = new DateTime(2025, 6, 25), StartTime = new DateTime(2025, 6, 25, 10, 15, 0), EndTime = new DateTime(2025, 6, 25, 11, 15, 0), Status = LessonStatus.Scheduled, Mode = LessonMode.InPerson });
		}
	}
}
