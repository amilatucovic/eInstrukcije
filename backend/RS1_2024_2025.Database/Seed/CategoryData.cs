using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_2025.Database.Seed
{
	public static class CategoryData
	{
		public static void SeedData(this EntityTypeBuilder<Category> entity)
		{
			entity.HasData(new Category { ID=1, Name = "Algebra" },
			new Category { ID= 2, Name = "Geometrija" },
			new Category { ID = 3, Name = "Gramatika" },
			new Category { ID = 4, Name = "Pravopis" },
			new Category { ID = 5, Name = "Optika" },
			new Category { ID = 6, Name = "Kinematika" },
			new Category { ID = 7, Name = "Termodinamika" },
			new Category { ID = 8, Name = "Programiranje" },
			new Category { ID = 9, Name = "Baze podataka" },
			new Category { ID = 10, Name = "Web razvoj i dizajn" },
			new Category { ID = 11, Name = "OOP" },
			new Category { ID = 12, Name = "Neuroanatomija" },
			new Category { ID = 13, Name = "Kosti" },
			new Category { ID = 14, Name = "Kotiranje" },
			new Category { ID = 15, Name = "Tehničko pismo" },
			new Category { ID = 16, Name = "Nacrtna geometrija" },
			new Category { ID = 17, Name = "Trigonometrija" },
			new Category { ID = 18, Name = "Genetika" },
			new Category { ID = 19, Name = "Deklinacije i komparacije pridjeva" },
			new Category { ID = 20, Name = "Latinske poslovice" },
			new Category { ID = 21, Name = "Ohmov zakon i Kirhofova pravila" },
			new Category { ID = 22, Name = "Strujni krugovi" },
			new Category { ID = 23, Name = "Hemijske jednačine" },
			new Category { ID = 24, Name = "Hemijske veze" },
			new Category { ID = 25, Name = "Organska hemija" },
			new Category { ID = 26, Name = "Brojni sistemi" },
			new Category { ID = 27, Name = "Konverzacija" });
		}
	}
}
