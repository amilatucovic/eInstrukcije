using Microsoft.EntityFrameworkCore;
using RS1_2024_2025.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_2024_2025.Database.DatabaseSeeder
{
	public static class DatabaseSeeder
	{
		public static async Task Initialize(ApplicationDbContext context)
		{
			Console.WriteLine("Seed poceo");

			// CITIES
			var cities = new List<City>
			{
				new City { Name = "Sarajevo", PostalCode = "71000" },
				new City { Name = "Banja Luka", PostalCode = "78000" },
				new City { Name= "Tuzla", PostalCode = "75000" },
				new City { Name = "Zenica", PostalCode = "72000" },
				new City { Name = "Mostar", PostalCode = "88000" },
				new City { Name = "Bijeljina", PostalCode = "76300" },
				new City { Name = "Brčko", PostalCode = "76100" },
				new City { Name = "Livno", PostalCode = "80200" },
				new City { Name = "Doboj", PostalCode = "74000" },
				new City { Name = "Travnik", PostalCode = "72290" },
				new City { Name = "Bihać", PostalCode = "77000" },
				new City { Name = "Goražde", PostalCode = "73000" },
				new City { Name = "Prijedor", PostalCode = "79101" }
			};

			foreach (var city in cities)
			{
				if (!await context.Cities.AnyAsync(c => c.Name == city.Name))
				{
					await context.Cities.AddAsync(city);
				}
			}

			var users = new List<MyAppUser>
			{
				new MyAppUser { Username="maida", FirstName="Maida", LastName="Tucovic", Password="maida123", Age=7, Email="maida@gmail.com", PhoneNumber="060000100", UserType=UserType.Student, Gender="Žensko", CityId=2 },
				new MyAppUser { Username = "emmaStudent", FirstName = "Emma", LastName = "Johnson", Password = "emma123", Age = 35, Email = "emma.johnson@example.com", PhoneNumber = "060000101", UserType = UserType.Student, Gender="Žensko", CityId=4},
				new MyAppUser { Username = "liamTutor", FirstName = "Liam", LastName = "Brown", Password = "liam123", Age = 40, Email = "liam.brown@example.com", PhoneNumber = "060000102", UserType = UserType.Tutor, Gender="Muško", CityId=1 },
				new MyAppUser { Username = "oliviaTutor", FirstName = "Olivia", LastName = "Davis", Password = "olivia123", Age = 29, Email = "olivia.davis@example.com", PhoneNumber = "060000103", UserType = UserType.Tutor, Gender="Žensko", CityId=5 },
				new MyAppUser { Username = "AdminAdmin", FirstName = "Admin", LastName = "Adminovic", Password = "admin123", Age = 27, Email = "admin.adminovic@example.com", PhoneNumber = "060000104", UserType = UserType.Admin, Gender="Muško", CityId=11 },
				new MyAppUser { Username="selma2204", FirstName="Selma", LastName="Alagic", Password="selma123", Age=19, Email="selma@gmail.com", PhoneNumber="060254100", UserType=UserType.Student, Gender="Žensko", CityId=11 }
			};

			foreach (var user in users)
			{
				if (!await context.MyAppUsers.AnyAsync(u => u.Username == user.Username))
				{
					await context.MyAppUsers.AddAsync(user);
				}
			}

			await context.SaveChangesAsync();

			var dbUsers = await context.MyAppUsers.ToListAsync();

			var tutors = new List<Tutor>
			{
				new Tutor
				{
					MyAppUserID = dbUsers.FirstOrDefault(u => u.Username == "liamTutor")?.ID ?? 0,
					Qualifications = "Bachelor Fizike",
					YearsOfExperience = 7,
					Availability = "Fleksibilni termini, samo vikendom",
					Rating = 0,
					Policy = "Otkazivanje je moguće samo u roku od 24 sata",
					HourlyRate = "40KM/h"
				},
				new Tutor
				{
					MyAppUserID = dbUsers.FirstOrDefault(u => u.Username == "oliviaTutor")?.ID ?? 0,
					Qualifications = "Magistar Engleskog jezika",
					YearsOfExperience = 4,
					Availability = "Vikendom od 14:00 - 18:00",
					Rating = 0,
					Policy = "Otkazivanje u roku od 48 sati",
					HourlyRate = "30KM/h"
				}
			};

			foreach (var tutor in tutors)
			{
				if (tutor.MyAppUserID != 0 && !await context.Tutors.AnyAsync(t => t.MyAppUserID == tutor.MyAppUserID))
				{
					await context.Tutors.AddAsync(tutor);
				}
			}

			var students = new List<Student>
			{
				new Student
				{
					MyAppUserID = dbUsers.FirstOrDefault(u => u.Username == "maida")?.ID ?? 0,
					Grade="Peti",
					PreferredMode="Online",
					EducationLevel=EducationLevel.ElementarySchool
				},
				new Student
				{
					MyAppUserID = dbUsers.FirstOrDefault(u => u.Username == "emmaStudent")?.ID ?? 0,
					Grade="Drugi",
					PreferredMode="Online",
					EducationLevel=EducationLevel.HighSchool
				}
			};

			foreach (var student in students)
			{
				if (student.MyAppUserID != 0 && !await context.Students.AnyAsync(s => s.MyAppUserID == student.MyAppUserID))
				{
					await context.Students.AddAsync(student);
				}
			}

			var admins = new List<Admin>
			{
				new Admin
				{
					MyAppUserID = dbUsers.FirstOrDefault(u => u.Username == "AdminAdmin")?.ID ?? 0,
					Role="Admin",
					CanApproveRequests=true,
					CanViewLogs=true
				}
			};

			foreach (var admin in admins)
			{
				if (admin.MyAppUserID != 0 && !await context.Admins.AnyAsync(a => a.MyAppUserID == admin.MyAppUserID))
				{
					await context.Admins.AddAsync(admin);
				}
			}

			// SUBJECTS
			var subjects = new List<Subject>
			{
				new Subject { Name = "Matematika", Description = "Osnovni koncepti brojeva, operacija i geometrije", DifficultyLevel = "osnovna skola" },
				new Subject { Name = "Bosanski jezik", Description = "Gramatika, pisanje i analiza književnih djela", DifficultyLevel = "osnovna skola" },
				new Subject { Name = "Priroda i društvo", Description = "Osnovna saznanja o prirodi, društvu i svijetu", DifficultyLevel = "osnovna skola" },
				new Subject { Name = "Engleski jezik", Description = "Osnovne vještine u govoru, čitanju i pisanju na engleskom jeziku", DifficultyLevel = "osnovna skola" },
				new Subject { Name = "Hemija", Description = "Osnovni koncepti hemijskih reakcija i svojstava materijala", DifficultyLevel = "osnovna skola" },
				new Subject { Name = "Fizika", Description = "Razumijevanje osnovnih principa fizike u svakodnevnom životu", DifficultyLevel = "osnovna skola" },
				new Subject { Name = "Historija", Description = "Učenje o važnim događajima i ličnostima kroz historiju", DifficultyLevel = "osnovna skola" },
				new Subject { Name = "Geografija", Description = "Proučavanje geografskih pojava i zemljopisnih karakteristika svijeta", DifficultyLevel = "osnovna skola" },
				new Subject { Name = "Biologija", Description = "Osnovni koncepti bioloških nauka, ekologija i evolucija", DifficultyLevel = "srednja skola" },
				new Subject { Name = "Matematika", Description = "Napredne tehnike i metode u matematici", DifficultyLevel = "srednja skola" },
				new Subject { Name = "Fizika", Description = "Istraživanje naprednih principa u fizici i njihovih primjena", DifficultyLevel = "srednja skola" },
				new Subject { Name = "Hemija", Description = "Napredni koncepti hemije i njihova primjena", DifficultyLevel = "srednja skola" },
				new Subject { Name = "Informatika", Description = "Razumijevanje računarskih sistema, programiranja i tehnologije", DifficultyLevel = "srednja skola" }
			};

			foreach (var subject in subjects)
			{
				if (!await context.Subjects.AnyAsync(s => s.Name == subject.Name && s.DifficultyLevel == subject.DifficultyLevel))
				{
					await context.Subjects.AddAsync(subject);
				}
			}


			// CATEGORIES
			var categories = new List<Category>
			{
				new Category { Name = "Matematika" },
				new Category { Name = "Bosanski jezik" },
				new Category { Name = "Priroda i društvo" },
				new Category { Name = "Engleski jezik" },
				new Category { Name = "Hemija" },
				new Category { Name = "Fizika" },
				new Category { Name = "Historija" },
				new Category { Name = "Geografija" },
				new Category { Name = "Biologija" },
				new Category { Name = "Informatika" }
			};

			foreach (var category in categories)
			{
				if (!await context.Categories.AnyAsync(c => c.Name == category.Name))
				{
					await context.Categories.AddAsync(category);
				}
			}

			await context.SaveChangesAsync();

		}
	}
}
