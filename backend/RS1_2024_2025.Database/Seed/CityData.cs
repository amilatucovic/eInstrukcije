using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_2025.Database.Seed
{
	public static class CityData
	{
		public static void SeedData(this EntityTypeBuilder<City> entity)
		{
			entity.HasData(new City { ID = 1, Name = "Sarajevo", PostalCode = "71000" });
			entity.HasData(new City { ID = 2, Name = "Tuzla", PostalCode = "75000" });
			entity.HasData(new City { ID = 3, Name = "Zenica", PostalCode = "72000" });
			entity.HasData(new City { ID = 4, Name = "Mostar", PostalCode = "88000" });
			entity.HasData(new City { ID = 5, Name = "Bijeljina", PostalCode = "76300" });
			entity.HasData(new City { ID = 6, Name = "Brčko", PostalCode = "76100" });
			entity.HasData(new City { ID = 7, Name = "Livno", PostalCode = "80200" });
			entity.HasData(new City { ID = 8, Name = "Doboj", PostalCode = "74000" });
			entity.HasData(new City { ID = 9, Name = "Travnik", PostalCode = "72290" });
			entity.HasData(new City { ID = 10, Name = "Bihać", PostalCode = "77000" });
			entity.HasData(new City { ID = 11, Name = "Goražde", PostalCode = "73000" });
			entity.HasData(new City { ID = 12, Name = "Prijedor", PostalCode = "79101" });
			entity.HasData(new City { ID = 13, Name = "Banja Luka", PostalCode = "78000" });
		} 
	}
}
