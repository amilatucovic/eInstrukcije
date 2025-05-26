using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_2025.Database.Seed
{
	public static class CategoryData
	{
		public static void SeedData(this EntityTypeBuilder<Category> entity)
		{
			entity.HasData(new Category { ID=1, Name = "Matematika" },
			new Category { ID= 2, Name = "Bosanski jezik" },
			new Category { ID = 3, Name = "Priroda i društvo" },
			new Category { ID = 4, Name = "Engleski jezik" },
			new Category { ID = 5, Name = "Hemija" },
			new Category { ID = 6, Name = "Fizika" },
			new Category { ID = 7, Name = "Historija" },
			new Category { ID = 8, Name = "Geografija" },
			new Category { ID = 9, Name = "Biologija" },
			new Category { ID = 10, Name = "Informatika" });
		}
	}
}
