using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RS1_2024_2025.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS1_2024_2025.Database.DatabaseSeeder
{
    public static class DatabaseSeeder
    {
        
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            //if (!await context.MyAppUsers.AnyAsync()) 
            {
                var users = new List<MyAppUser>
                {
                    new Student
                    {
                        Username="maida",
                        FirstName="Maida",
                        LastName="Tucovic",
                        Password="maida123",
                        Age=7,
                        Email="maida@gmail.com",
                        PhoneNumber="060000100",
                        UserType=UserType.Student,Grade="Drugi",
                        PreferredMode="Uzivo",
                        EducationLevel=EducationLevel.ElementarySchool
                    },
                    new Tutor
                    {
                        Username = "emmaTutor",
                        FirstName = "Emma",
                        LastName = "Johnson",
                        Password = "emma123",
                        Age = 35,
                        Email = "emma.johnson@example.com",
                        PhoneNumber = "060000101",
                        UserType = UserType.Tutor,
                        Qualifications = "Doktor nauka iz oblasti Softverski inzinjering",
                        YearsOfExperience = 8,
                        Availability = "Radnim danima od 10:00 - 18:00",
                        Rating = 0,
                        Policy = "Otkazivanje je moguće samo u roku od 24 sata",
                        HourlyRate = "40KM/h"
                    },
                    new Tutor
                    {
                        Username = "liamTutor",
                        FirstName = "Liam",
                        LastName = "Brown",
                        Password = "liam123",
                        Age = 40,
                        Email = "liam.brown@example.com",
                        PhoneNumber = "060000102",
                        UserType = UserType.Tutor,
                        Qualifications = "Magistar Matematike",
                        YearsOfExperience = 12,
                        Availability = "Vikendom od 10:00 - 13:00",
                        Rating = 0,
                        Policy = "Otkazivanje je moguće samo u roku od 24 sata",
                        HourlyRate = "50KM/h"
                    },
                    new Tutor
                    {
                        Username = "oliviaTutor",
                        FirstName = "Olivia",
                        LastName = "Davis",
                        Password = "olivia123",
                        Age = 29,
                        Email = "olivia.davis@example.com",
                        PhoneNumber = "060000103",
                        UserType = UserType.Tutor,
                        Qualifications = "Licencirani profesor prirodnih nauka",
                        YearsOfExperience = 5,
                        Availability = "Ponedjeljkom od 10:00 - 13:00, Srijedom od 17:00 - 20:00",
                        Rating = 0,
                        Policy = "Otkazivanje je moguće samo u roku od 32 sata",
                        HourlyRate = "35KM/h"
                    },
                    new Tutor
                    {
                        Username = "noahTutor",
                        FirstName = "Noah",
                        LastName = "Wilson",
                        Password = "noah123",
                        Age = 33,
                        Email = "noah.wilson@example.com",
                        PhoneNumber = "060000104",
                        UserType = UserType.Tutor,
                        Qualifications = "Bachelor Fizike",
                        YearsOfExperience = 7,
                        Availability = "Fleksibilni termini, samo vikendom",
                        Rating = 0,
                        Policy = "Otkazivanje je moguće samo u roku od 24 sata",
                        HourlyRate = "40KM/h"
                    },
                    new Tutor
                    {
                        Username = "sophiaTutor",
                        FirstName = "Sophia",
                        LastName = "Taylor",
                        Password = "sophia123",
                        Age = 28,
                        Email = "sophia.taylor@example.com",
                        PhoneNumber = "060000105",
                        UserType = UserType.Tutor,
                        Qualifications = "Magistar Engleskog jezika",
                        YearsOfExperience = 4,
                        Availability = "Vikendom od 14:00 - 18:00",
                        Rating = 0,
                        Policy = "Otkazivanje u roku od 48 sati",
                        HourlyRate = "30KM/h"
                    },
                    new Tutor
                    {
                        Username = "masonTutor",
                        FirstName = "Mason",
                        LastName = "Martinez",
                        Password = "mason123",
                        Age = 37,
                        Email = "mason.martinez@example.com",
                        PhoneNumber = "060000106",
                        UserType = UserType.Tutor,
                        Qualifications = "Profesor Historije",
                        YearsOfExperience = 9,
                        Availability = "Utorkom od 17:00 - 20:00, Petkom od 18:00 - 21:00",
                        Rating = 0,
                        Policy = "Nema otkazivanja na dan instrukcija",
                        HourlyRate = "55KM/h"
                    },
                    new Tutor
                    {
                        Username = "avaTutor",
                        FirstName = "Ava",
                        LastName = "Moore",
                        Password = "ava123",
                        Age = 32,
                        Email = "ava.moore@example.com",
                        PhoneNumber = "060000107",
                        UserType = UserType.Tutor,
                        Qualifications = "Magistar Hemije",
                        YearsOfExperience = 6,
                        Availability = "Radnim danima od 19:00 - 22:00",
                        Rating = 4.4,
                        Policy = "Otkazivanje je moguće samo u roku od 42 sata",
                        HourlyRate = "35KM/h"
                    },
                    new Tutor
                    {
                        Username = "loganTutor",
                        FirstName = "Logan",
                        LastName = "Thomas",
                        Password = "logan123",
                        Age = 30,
                        Email = "logan.thomas@example.com",
                        PhoneNumber = "060000108",
                        UserType = UserType.Tutor,
                        Qualifications = "Magistar Likovnih umjetnosti",
                        YearsOfExperience = 5,
                        Availability = "Vikendom od 11:00 - 17:00",
                        Rating = 4.2,
                        Policy = "Otkazivanje je moguće samo u roku od 24 sata",
                        HourlyRate = "40KM/h"
                    },
                    new Student
                    {
                        Username = "aliceStudent",
                        FirstName = "Alice",
                        LastName = "Green",
                        Password = "alice123",
                        Age = 20,
                        Email = "alice.green@example.com",
                        PhoneNumber = "060000109",
                        UserType = UserType.Student,
                        Grade="Treci",
                        PreferredMode="Online",
                        EducationLevel=EducationLevel.ElementarySchool
                    },
                    new Student
                    {
                        Username = "bobStudent",
                        FirstName = "Bob",
                        LastName = "White",
                        Password = "bob123",
                        Age = 22,
                        Email = "bob.white@example.com",
                        PhoneNumber = "060000110",
                        UserType = UserType.Student,
                        Grade="Peti",
                        PreferredMode="Online",
                        EducationLevel=EducationLevel.ElementarySchool
                    }
                };
                var subjects = new List<Subject>
                {
                    new Subject { Name = "Matematika", Description = "Osnovni koncepti brojeva, operacija i geometrije", DifficultyLevel = "osnovna skola" },
                    new Subject { Name = "Bosanski jezik", Description = "Gramatika, pisanje i analiza književnih djela", DifficultyLevel = "osnovna skola" },
                    new Subject { Name = "Priroda i društvo", Description = "Osnovna saznanja o prirodi, društvu i svijetu", DifficultyLevel = "osnovna skola" },
                    new Subject { Name = "Engleski jezik", Description = "Osnovne vještine u govoru, čitanju i pisanju na engleskom jeziku", DifficultyLevel = "osnovna skola" },
                    new Subject { Name = "Hemija", Description = "Osnovni koncepti hemijskih reakcija i svojstava materijala", DifficultyLevel = "osnovna skola" },
                    new Subject { Name = "Fizika", Description = "Razumijevanje osnovnih principa fizike u svakodnevnom životu", DifficultyLevel = "osnovna skola" },
                    new Subject { Name = "Historija", Description = "Učenje o važnim događajima i ličnostima kroz istoriju", DifficultyLevel = "osnovna skola" },
                    new Subject { Name = "Geografija", Description = "Proučavanje geografskih pojava i zemljopisnih karakteristika svijeta", DifficultyLevel = "osnovna skola" },
                    new Subject { Name = "Biologija", Description = "Osnovni koncepti bioloških nauka, ekologija i evolucija", DifficultyLevel = "srednja skola" },
                    new Subject { Name = "Matematika", Description = "Napredne tehnike i metode u matematici", DifficultyLevel = "srednja skola" },
                    new Subject { Name = "Fizika", Description = "Istraživanje naprednih principa u fizici i njihovih primjena", DifficultyLevel = "srednja skola" },
                    new Subject { Name = "Hemija", Description = "Napredni koncepti hemije i njihova primjena", DifficultyLevel = "srednja skola" },
                    new Subject { Name = "Informatika", Description = "Razumijevanje računarskih sistema, programiranja i tehnologije", DifficultyLevel = "srednja skola" }
                };

                var cities = new List<City>
                {
                    new City { Name = "Sarajevo", PostalCode = "71000" },
                    new City { Name = "Banja Luka", PostalCode = "78000" },
                    new City { Name = "Tuzla", PostalCode = "75000" },
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

                var categories = new List<Category>
                {
                    new Category { Name = "Algebra" },
                    new Category { Name = "Geometrija" },
                    new Category { Name = "Trigonometrija" },
                    new Category { Name = "Analitička geometrija" },
                    new Category { Name = "Kalkulus" },
                    new Category { Name = "Mehanika" },
                    new Category { Name = "Gramatika" },
                    new Category { Name = "Eseji" },
                    new Category { Name = "Pravopis" },
                    new Category { Name = "Literatura" },
                    new Category { Name = "Genetika" },
                    new Category { Name = "Ekologija" },
                    new Category { Name = "Analitička hemija" }
                };


                await context.MyAppUsers.AddRangeAsync(users);
                await context.Subjects.AddRangeAsync(subjects);
                await context.Cities.AddRangeAsync(cities);
                await context.Categories.AddRangeAsync(categories);

                await context.SaveChangesAsync();
            }
        }
    }
}
