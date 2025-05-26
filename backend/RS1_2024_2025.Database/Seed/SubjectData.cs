using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RS1_2024_2025.Domain.Entities;

namespace RS1_2024_2025.Database.Seed
{
	public static class SubjectData
	{
		public static void SeedData(this EntityTypeBuilder<Subject> entity)
		{
			entity.HasData(new Subject { ID=1, Name = "Bosanski jezik", Description = "Gramatika, pisanje i analiza književnih djela", DifficultyLevel = "osnovna skola" },
			new Subject { ID = 2, Name = "Engleski jezik", Description = "Osnovne vještine u govoru, čitanju i pisanju na engleskom jeziku", DifficultyLevel = "osnovna skola" },
			new Subject { ID = 3, Name = "Fizika", Description = "Razumijevanje osnovnih principa fizike u svakodnevnom životu", DifficultyLevel = "osnovna skola" },
			new Subject { ID = 4, Name = "Geografija", Description = "Proučavanje geografskih pojava i zemljopisnih karakteristika svijeta", DifficultyLevel = "osnovna skola" },
			new Subject { ID = 5, Name = "Hemija", Description = "Osnovni koncepti hemijskih reakcija i svojstava materijala", DifficultyLevel = "osnovna skola" },
			new Subject { ID = 6, Name = "Historija", Description = "Učenje o važnim događajima i ličnostima kroz historiju", DifficultyLevel = "osnovna skola" },
			new Subject { ID = 7, Name = "Matematika", Description = "Osnovni koncepti brojeva, operacija i geometrije", DifficultyLevel = "osnovna skola" },
			new Subject { ID = 8, Name = "Priroda i društvo", Description = "Osnovna saznanja o prirodi, društvu i svijetu", DifficultyLevel = "osnovna skola" },
			new Subject { ID = 9, Name = "Likovna kultura", Description = "Razvijanje kreativnosti kroz crtanje, slikanje i izražavanje", DifficultyLevel = "osnovna skola" },
			new Subject { ID = 10, Name = "Muzicka kultura", Description = "Upoznavanje sa osnovama muzike, notama i muzičkim izrazom", DifficultyLevel = "osnovna skola" },
			new Subject { ID = 11, Name = "Tehnička kultura", Description = "Razvijanje tehničkih vještina i praktičnih sposobnosti", DifficultyLevel = "osnovna skola" },
			new Subject { ID = 12, Name = "Tjelesni i zdravstveni odgoj", Description = "Fizička aktivnost i usvajanje zdravih životnih navika", DifficultyLevel = "osnovna skola" },
			new Subject { ID = 13, Name = "Vjeronauka", Description = "Upoznavanje sa osnovnim religijskim pojmovima i vrijednostima", DifficultyLevel = "osnovna skola" },
			new Subject { ID = 14, Name = "Njemački jezik", Description = "Osnove njemačkog jezika: govor, čitanje i pisanje", DifficultyLevel = "osnovna skola" },

			new Subject { ID = 15, Name = "Sociologija", Description = "Proučavanje društva, normi, vrijednosti i socijalnih grupa", DifficultyLevel = "srednja skola" },
			new Subject { ID = 16, Name = "Psihologija", Description = "Osnove ljudskog ponašanja i mentalnih procesa", DifficultyLevel = "srednja skola" },
			new Subject { ID = 17, Name = "Filozofija", Description = "Uvod u logičko razmišljanje, etiku i epistemologiju", DifficultyLevel = "srednja skola" },
			new Subject { ID = 18, Name = "Logika", Description = "Formalno razmišljanje, argumentacija i zaključivanje", DifficultyLevel = "srednja skola" },
			new Subject { ID = 19, Name = "Latinski jezik", Description = "Osnove klasičnog jezika i njegov utjecaj na savremene jezike", DifficultyLevel = "srednja skola" },
			new Subject { ID = 20, Name = "Njemački jezik", Description = "Napredna gramatika, vokabular i konverzacija", DifficultyLevel = "srednja skola" },
			new Subject { ID = 21, Name = "Francuski jezik", Description = "Analiza tekstova, napredno pisanje i komunikacija", DifficultyLevel = "srednja skola" },
			new Subject { ID = 22, Name = "Tjelesni i zdravstveni odgoj", Description = "Unapređenje fizičke spremnosti i zdravih stilova života", DifficultyLevel = "srednja skola" },
			new Subject { ID = 23, Name = "Biologija", Description = "Osnovni koncepti bioloških nauka, ekologija i evolucija", DifficultyLevel = "srednja skola" },
			new Subject { ID = 24, Name = "Fizika", Description = "Istraživanje naprednih principa u fizici i njihovih primjena", DifficultyLevel = "srednja skola" },
			new Subject { ID = 25, Name = "Hemija", Description = "Napredni koncepti hemije i njihova primjena", DifficultyLevel = "srednja skola" },
			new Subject { ID = 26, Name = "Informatika", Description = "Razumijevanje računarskih sistema, programiranja i tehnologije", DifficultyLevel = "srednja skola" },
			new Subject { ID = 27, Name = "Matematika", Description = "Napredne tehnike i metode u matematici", DifficultyLevel = "srednja skola" });
		}
	}
}
