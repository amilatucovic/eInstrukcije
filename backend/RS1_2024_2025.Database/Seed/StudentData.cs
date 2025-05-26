using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_2025.Database.Seed
{
	public static class StudentData
	{
		public static void SeedData(this EntityTypeBuilder<Student> entity)
		{
			entity.HasData(new Student { ID=1, MyAppUserID = 7, Grade = "Drugi", PreferredMode = "Online", EducationLevel = EducationLevel.ElementarySchool },
			new Student { ID=2, MyAppUserID = 8, Grade = "Peti", PreferredMode = "InClass", EducationLevel = EducationLevel.ElementarySchool },
			new Student { ID=3, MyAppUserID = 9, Grade = "Četvrti", PreferredMode = "InClass", EducationLevel = EducationLevel.HighSchool },
			new Student { ID=4, MyAppUserID = 10, Grade = "Treći", PreferredMode = "Online", EducationLevel = EducationLevel.HighSchool },
			new Student { ID=5, MyAppUserID = 11, Grade = "Prvi", PreferredMode = "Online", EducationLevel = EducationLevel.HighSchool },
			new Student { ID=6, MyAppUserID = 12, Grade = "Sedmi", PreferredMode = "Online", EducationLevel = EducationLevel.ElementarySchool },
			new Student { ID=7, MyAppUserID = 13, Grade = "Drugi", PreferredMode = "InClass", EducationLevel = EducationLevel.ElementarySchool });
		}
	}
}
