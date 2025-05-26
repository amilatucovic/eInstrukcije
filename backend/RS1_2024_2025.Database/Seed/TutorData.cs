using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_2025.Database.Seed
{
	public static class TutorData
	{
		public static void SeedData(this EntityTypeBuilder<Tutor> entity)
		{
			entity.HasData(new Tutor { ID = 1, MyAppUserID = 2, Qualifications = "Profesor matematike", YearsOfExperience = 10, Availability = "Radnim danima od 17:00 do 20:00", Rating = 0, Policy = "Potrebno otkazati 24 sata ranije", HourlyRate = "25KM/h" },
			new Tutor { ID = 2, MyAppUserID = 3, Qualifications = "Magistar Engleskog jezika", YearsOfExperience = 4, Availability = "Vikendom od 14:00 - 18:00", Rating = 0, Policy = "Otkazivanje u roku od 48 sati", HourlyRate = "30KM/h" },
			new Tutor { ID = 3, MyAppUserID = 4, Qualifications = "Diplomirani fizičar", YearsOfExperience = 7, Availability = "Ponedjeljak - Srijeda od 16:00 do 19:00", Rating = 0, Policy = "Bez otkazivanja u istom danu", HourlyRate = "28KM/h" },
			new Tutor { ID = 4, MyAppUserID = 5, Qualifications = "Profesorica hemije", YearsOfExperience = 12, Availability = "Radnim danima poslije 18:00", Rating = 0, Policy = "Otkazivanje najkasnije 24h ranije", HourlyRate = "35KM/h" },
			new Tutor { ID = 5, MyAppUserID = 6, Qualifications = "Master informatike", YearsOfExperience = 6, Availability = "Subotom i nedjeljom 09:00-14:00", Rating = 0, Policy = "Mogućnost pomjeranja termina", HourlyRate = "32KM/h" });
		}
	}
}
