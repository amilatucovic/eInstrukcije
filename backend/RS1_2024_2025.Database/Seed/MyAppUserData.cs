using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_2025.Database.Seed
{
	public static class MyAppUserData
	{
		public static void SeedData(this EntityTypeBuilder<MyAppUser> entity)
		{
			entity.HasData(new MyAppUser() { ID = 1, Username = "AdminAdmin", FirstName = "Admin", LastName = "Adminovic", Password = "admin123", Age = 27, Email = "admin.adminovic@example.com", PhoneNumber = "060000104", UserType = UserType.Admin, Gender = "Muško", CityId = 10 },
			new MyAppUser() { ID = 2, Username = "liamTutor", FirstName = "Liam", LastName = "Brown", Password = "liam123", Age = 40, Email = "liam.brown@example.com", PhoneNumber = "060000102", UserType = UserType.Tutor, Gender = "Muško", CityId = 1 },
			new MyAppUser() { ID = 3, Username = "oliviaTutor", FirstName = "Olivia", LastName = "Davis", Password = "olivia123", Age = 29, Email = "olivia.davis@example.com", PhoneNumber = "060000103", UserType = UserType.Tutor, Gender = "Žensko", CityId = 1 },
			new MyAppUser() { ID = 4, Username = "mirzaProf", FirstName = "Mirza", LastName = "Omerović", Password = "mirza123", Age = 34, Email = "mirza.omerovic@example.com", PhoneNumber = "062222201", UserType = UserType.Tutor, Gender = "Muško", CityId = 2 },
			new MyAppUser() { ID = 5, Username = "sabinaProf", FirstName = "Sabina", LastName = "Delić", Password = "sabina123", Age = 42, Email = "sabina.delic@example.com", PhoneNumber = "062222202", UserType = UserType.Tutor, Gender = "Žensko", CityId = 3 },
			new MyAppUser() { ID = 6, Username = "edinProf", FirstName = "Edin", LastName = "Čolić", Password = "edin123", Age = 39, Email = "edin.colic@example.com", PhoneNumber = "062222203", UserType = UserType.Tutor, Gender = "Muško", CityId = 9 },
			new MyAppUser() { ID = 7, Username = "maida", FirstName = "Maida", LastName = "Tucovic", Password = "maida123", Age = 7, Email = "maida@gmail.com", PhoneNumber = "060000100", UserType = UserType.Student, Gender = "Žensko", CityId = 2 },
			new MyAppUser() { ID = 8, Username = "emmaStudent", FirstName = "Emma", LastName = "Johnson", Password = "emma123", Age = 10, Email = "emma.johnson@example.com", PhoneNumber = "060000101", UserType = UserType.Student, Gender = "Žensko", CityId = 9 },
			new MyAppUser() { ID = 9, Username = "ajlaStudent", FirstName = "Ajla", LastName = "Hadžić", Password = "ajla123", Age = 19, Email = "ajla.hadzic@example.com", PhoneNumber = "061111101", UserType = UserType.Student, Gender = "Žensko", CityId = 12 },
			new MyAppUser() { ID = 10, Username = "nedimStudent", FirstName = "Nedim", LastName = "Selimović", Password = "nedim123", Age = 17, Email = "nedim.selimovic@example.com", PhoneNumber = "061111102", UserType = UserType.Student, Gender = "Muško", CityId = 7 },
			new MyAppUser() { ID = 11, Username = "lamijaStudent", FirstName = "Lamija", LastName = "Mehić", Password = "lamija123", Age = 15, Email = "lamija.mehic@example.com", PhoneNumber = "061111103", UserType = UserType.Student, Gender = "Žensko", CityId = 8 },
			new MyAppUser() { ID = 12, Username = "adinStudent", FirstName = "Adin", LastName = "Mujić", Password = "adin123", Age = 12, Email = "adin.mujic@example.com", PhoneNumber = "061111104", UserType = UserType.Student, Gender = "Muško", CityId = 13 },
			new MyAppUser() { ID = 13, Username = "nejraStudent", FirstName = "Nejra", LastName = "Alihodžić", Password = "nejra123", Age = 7, Email = "nejra.alihodzic@example.com", PhoneNumber = "061111105", UserType = UserType.Student, Gender = "Žensko", CityId = 11 });
		}
	}
}