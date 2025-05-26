using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_2025.Database.Seed
{
	public static class StudentSubjectData
	{
		public static void SeedData(this EntityTypeBuilder<StudentSubject> entity)
		{
			entity.HasData(new StudentSubject { StudentID = 1, SubjectID = 1 }, 
			new StudentSubject { StudentID = 2, SubjectID = 7 },  
			new StudentSubject { StudentID = 3, SubjectID = 19 },  
			new StudentSubject { StudentID = 4, SubjectID = 24 }, 
			new StudentSubject { StudentID = 5, SubjectID = 22 });
		}
	}
}
