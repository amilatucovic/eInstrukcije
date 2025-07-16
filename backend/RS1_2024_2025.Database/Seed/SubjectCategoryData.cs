using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_2025.Database.Seed
{
	public static class SubjectCategoryData
	{
		public static void SeedData(this EntityTypeBuilder<SubjectCategory> entity)
		{
			entity.HasData(new SubjectCategory { SubjectID = 1, CategoryID = 3 },
				new SubjectCategory { SubjectID = 1, CategoryID = 4 },
				new SubjectCategory { SubjectID = 2, CategoryID = 3 },
				new SubjectCategory { SubjectID = 2, CategoryID = 4 },
				new SubjectCategory { SubjectID = 2, CategoryID = 27 },
				new SubjectCategory { SubjectID = 3, CategoryID = 5 },
				new SubjectCategory { SubjectID = 3, CategoryID = 6 },
				new SubjectCategory { SubjectID = 3, CategoryID = 7 },
				new SubjectCategory { SubjectID = 3, CategoryID = 22 },
				new SubjectCategory { SubjectID = 5, CategoryID = 23 },
				new SubjectCategory { SubjectID = 5, CategoryID = 24 },
				new SubjectCategory { SubjectID = 5, CategoryID = 25},
				new SubjectCategory { SubjectID = 7, CategoryID = 1},
				new SubjectCategory { SubjectID = 7, CategoryID = 2},
				new SubjectCategory { SubjectID = 12, CategoryID = 3},
				new SubjectCategory { SubjectID = 12, CategoryID = 4},
				new SubjectCategory { SubjectID = 12, CategoryID = 27},
				new SubjectCategory { SubjectID = 13, CategoryID = 3},
				new SubjectCategory { SubjectID = 13, CategoryID = 4},
				new SubjectCategory { SubjectID = 13, CategoryID = 27},
				new SubjectCategory { SubjectID = 14, CategoryID = 21},
				new SubjectCategory { SubjectID = 14, CategoryID = 22},
				new SubjectCategory { SubjectID = 15, CategoryID = 12},
				new SubjectCategory { SubjectID = 15, CategoryID = 13},
				new SubjectCategory { SubjectID = 17, CategoryID = 14},
				new SubjectCategory { SubjectID = 17, CategoryID = 15},
				new SubjectCategory { SubjectID = 17, CategoryID = 16},
				new SubjectCategory { SubjectID = 19, CategoryID = 3},
				new SubjectCategory { SubjectID = 19, CategoryID = 19},
				new SubjectCategory { SubjectID = 19, CategoryID = 20},
				new SubjectCategory { SubjectID = 20, CategoryID = 3},
				new SubjectCategory { SubjectID = 20, CategoryID = 4},
				new SubjectCategory { SubjectID = 20, CategoryID = 27},
				new SubjectCategory { SubjectID = 21, CategoryID = 3},
				new SubjectCategory { SubjectID = 21, CategoryID = 4},
				new SubjectCategory { SubjectID = 21, CategoryID = 27},
				new SubjectCategory { SubjectID = 24, CategoryID = 5},
				new SubjectCategory { SubjectID = 24, CategoryID = 6},
				new SubjectCategory { SubjectID = 24, CategoryID = 7},
				new SubjectCategory { SubjectID = 24, CategoryID = 21},
				new SubjectCategory { SubjectID = 24, CategoryID = 22},
				new SubjectCategory { SubjectID = 25, CategoryID = 23},
				new SubjectCategory { SubjectID = 25, CategoryID = 24},
				new SubjectCategory { SubjectID = 25, CategoryID = 25},
				new SubjectCategory { SubjectID = 26, CategoryID = 8},
				new SubjectCategory { SubjectID = 26, CategoryID = 9},
				new SubjectCategory { SubjectID = 26, CategoryID = 10},
				new SubjectCategory { SubjectID = 26, CategoryID = 11},
				new SubjectCategory { SubjectID = 26, CategoryID = 26},
				new SubjectCategory { SubjectID = 27, CategoryID = 1},
				new SubjectCategory { SubjectID = 27, CategoryID = 2},
				new SubjectCategory { SubjectID = 27, CategoryID = 17}
				);
		}
	}
}
