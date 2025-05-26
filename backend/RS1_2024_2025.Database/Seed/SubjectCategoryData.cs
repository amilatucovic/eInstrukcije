using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_2025.Database.Seed
{
	public static class SubjectCategoryData
	{
		public static void SeedData(this EntityTypeBuilder<SubjectCategory> entity)
		{
			entity.HasData(new SubjectCategory { SubjectID = 1, CategoryID = 2 },
				new SubjectCategory { SubjectID = 2, CategoryID = 4 },
				new SubjectCategory { SubjectID = 3, CategoryID = 6 },
				new SubjectCategory { SubjectID = 4, CategoryID = 8 },
				new SubjectCategory { SubjectID = 5, CategoryID = 5 },
				new SubjectCategory { SubjectID = 6, CategoryID = 7 },
				new SubjectCategory { SubjectID = 7, CategoryID = 1 },
				new SubjectCategory { SubjectID = 8, CategoryID = 3 },
				new SubjectCategory { SubjectID = 9, CategoryID = 3 },
				new SubjectCategory { SubjectID = 10, CategoryID = 3 },
				new SubjectCategory { SubjectID = 11, CategoryID = 3 },
				new SubjectCategory { SubjectID = 12, CategoryID = 3 },
				new SubjectCategory { SubjectID = 13, CategoryID = 3 },
				new SubjectCategory { SubjectID = 14, CategoryID = 4 },
				new SubjectCategory { SubjectID = 15, CategoryID = 3 },
				new SubjectCategory { SubjectID = 16, CategoryID = 3 },
				new SubjectCategory { SubjectID = 17, CategoryID = 3 },
				new SubjectCategory { SubjectID = 18, CategoryID = 3 },
				new SubjectCategory { SubjectID = 19, CategoryID = 2 },
				new SubjectCategory { SubjectID = 20, CategoryID = 4 },
				new SubjectCategory { SubjectID = 21, CategoryID = 4 },
				new SubjectCategory { SubjectID = 22, CategoryID = 3 },
				new SubjectCategory { SubjectID = 23, CategoryID = 9 },
				new SubjectCategory { SubjectID = 24, CategoryID = 6 },
				new SubjectCategory { SubjectID = 25, CategoryID = 5 },
				new SubjectCategory { SubjectID = 26, CategoryID = 10 },
				new SubjectCategory { SubjectID = 27, CategoryID = 1 });
		}
	}
}
