using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_2025.Database.Seed
{
	public static class TutorSubjectData
	{
		public static void SeedData(this EntityTypeBuilder<TutorSubject> entity)
		{
			entity.HasData(new TutorSubject { TutorID = 1, SubjectID = 1 }, 
			new TutorSubject { TutorID = 1, SubjectID = 10 }, 
			new TutorSubject { TutorID = 1, SubjectID = 11 },
			new TutorSubject { TutorID = 2, SubjectID = 2 }, 
			new TutorSubject { TutorID = 2, SubjectID = 4 },  
			new TutorSubject { TutorID = 2, SubjectID = 21 },
			new TutorSubject { TutorID = 3, SubjectID = 7 },  
			new TutorSubject { TutorID = 3, SubjectID = 17 }, 
			new TutorSubject { TutorID = 3, SubjectID = 18 },  
			new TutorSubject { TutorID = 4, SubjectID = 9 },  
			new TutorSubject { TutorID = 4, SubjectID = 12 },
			new TutorSubject { TutorID = 4, SubjectID = 13 },
			new TutorSubject { TutorID = 5, SubjectID = 3 },  
			new TutorSubject { TutorID = 5, SubjectID = 6 },  
			new TutorSubject { TutorID = 5, SubjectID = 5 });
		}
	}
}
