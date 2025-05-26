using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RS1_2024_2025.Database.Migrations
{
    /// <inheritdoc />
    public partial class InsertingAllNeededData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Matematika" },
                    { 2, "Bosanski jezik" },
                    { 3, "Priroda i društvo" },
                    { 4, "Engleski jezik" },
                    { 5, "Hemija" },
                    { 6, "Fizika" },
                    { 7, "Historija" },
                    { 8, "Geografija" },
                    { 9, "Biologija" },
                    { 10, "Informatika" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "Name", "PostalCode" },
                values: new object[,]
                {
                    { 1, "Sarajevo", "71000" },
                    { 2, "Tuzla", "75000" },
                    { 3, "Zenica", "72000" },
                    { 4, "Mostar", "88000" },
                    { 5, "Bijeljina", "76300" },
                    { 6, "Brčko", "76100" },
                    { 7, "Livno", "80200" },
                    { 8, "Doboj", "74000" },
                    { 9, "Travnik", "72290" },
                    { 10, "Bihać", "77000" },
                    { 11, "Goražde", "73000" },
                    { 12, "Prijedor", "79101" },
                    { 13, "Banja Luka", "78000" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "ID", "Description", "DifficultyLevel", "Name" },
                values: new object[,]
                {
                    { 1, "Gramatika, pisanje i analiza književnih djela", "osnovna skola", "Bosanski jezik" },
                    { 2, "Osnovne vještine u govoru, čitanju i pisanju na engleskom jeziku", "osnovna skola", "Engleski jezik" },
                    { 3, "Razumijevanje osnovnih principa fizike u svakodnevnom životu", "osnovna skola", "Fizika" },
                    { 4, "Proučavanje geografskih pojava i zemljopisnih karakteristika svijeta", "osnovna skola", "Geografija" },
                    { 5, "Osnovni koncepti hemijskih reakcija i svojstava materijala", "osnovna skola", "Hemija" },
                    { 6, "Učenje o važnim događajima i ličnostima kroz historiju", "osnovna skola", "Historija" },
                    { 7, "Osnovni koncepti brojeva, operacija i geometrije", "osnovna skola", "Matematika" },
                    { 8, "Osnovna saznanja o prirodi, društvu i svijetu", "osnovna skola", "Priroda i društvo" },
                    { 9, "Razvijanje kreativnosti kroz crtanje, slikanje i izražavanje", "osnovna skola", "Likovna kultura" },
                    { 10, "Upoznavanje sa osnovama muzike, notama i muzičkim izrazom", "osnovna skola", "Muzicka kultura" },
                    { 11, "Razvijanje tehničkih vještina i praktičnih sposobnosti", "osnovna skola", "Tehnička kultura" },
                    { 12, "Fizička aktivnost i usvajanje zdravih životnih navika", "osnovna skola", "Tjelesni i zdravstveni odgoj" },
                    { 13, "Upoznavanje sa osnovnim religijskim pojmovima i vrijednostima", "osnovna skola", "Vjeronauka" },
                    { 14, "Osnove njemačkog jezika: govor, čitanje i pisanje", "osnovna skola", "Njemački jezik" },
                    { 15, "Proučavanje društva, normi, vrijednosti i socijalnih grupa", "srednja skola", "Sociologija" },
                    { 16, "Osnove ljudskog ponašanja i mentalnih procesa", "srednja skola", "Psihologija" },
                    { 17, "Uvod u logičko razmišljanje, etiku i epistemologiju", "srednja skola", "Filozofija" },
                    { 18, "Formalno razmišljanje, argumentacija i zaključivanje", "srednja skola", "Logika" },
                    { 19, "Osnove klasičnog jezika i njegov utjecaj na savremene jezike", "srednja skola", "Latinski jezik" },
                    { 20, "Napredna gramatika, vokabular i konverzacija", "srednja skola", "Njemački jezik" },
                    { 21, "Analiza tekstova, napredno pisanje i komunikacija", "srednja skola", "Francuski jezik" },
                    { 22, "Unapređenje fizičke spremnosti i zdravih stilova života", "srednja skola", "Tjelesni i zdravstveni odgoj" },
                    { 23, "Osnovni koncepti bioloških nauka, ekologija i evolucija", "srednja skola", "Biologija" },
                    { 24, "Istraživanje naprednih principa u fizici i njihovih primjena", "srednja skola", "Fizika" },
                    { 25, "Napredni koncepti hemije i njihova primjena", "srednja skola", "Hemija" },
                    { 26, "Razumijevanje računarskih sistema, programiranja i tehnologije", "srednja skola", "Informatika" },
                    { 27, "Napredne tehnike i metode u matematici", "srednja skola", "Matematika" }
                });

            migrationBuilder.InsertData(
                table: "MyAppUsers",
                columns: new[] { "ID", "Age", "CityId", "Email", "FirstName", "Gender", "LastName", "Password", "PhoneNumber", "ProfileImageUrl", "RefreshToken", "RefreshTokenExpiryTime", "UserType", "Username" },
                values: new object[,]
                {
                    { 1, 27, 10, "admin.adminovic@example.com", "Admin", "Muško", "Adminovic", "admin123", "060000104", null, null, null, "Admin", "AdminAdmin" },
                    { 2, 40, 1, "liam.brown@example.com", "Liam", "Muško", "Brown", "liam123", "060000102", null, null, null, "Tutor", "liamTutor" },
                    { 3, 29, 1, "olivia.davis@example.com", "Olivia", "Žensko", "Davis", "olivia123", "060000103", null, null, null, "Tutor", "oliviaTutor" },
                    { 4, 34, 2, "mirza.omerovic@example.com", "Mirza", "Muško", "Omerović", "mirza123", "062222201", null, null, null, "Tutor", "mirzaProf" },
                    { 5, 42, 3, "sabina.delic@example.com", "Sabina", "Žensko", "Delić", "sabina123", "062222202", null, null, null, "Tutor", "sabinaProf" },
                    { 6, 39, 9, "edin.colic@example.com", "Edin", "Muško", "Čolić", "edin123", "062222203", null, null, null, "Tutor", "edinProf" },
                    { 7, 7, 2, "maida@gmail.com", "Maida", "Žensko", "Tucovic", "maida123", "060000100", null, null, null, "Student", "maida" },
                    { 8, 10, 9, "emma.johnson@example.com", "Emma", "Žensko", "Johnson", "emma123", "060000101", null, null, null, "Student", "emmaStudent" },
                    { 9, 19, 12, "ajla.hadzic@example.com", "Ajla", "Žensko", "Hadžić", "ajla123", "061111101", null, null, null, "Student", "ajlaStudent" },
                    { 10, 17, 7, "nedim.selimovic@example.com", "Nedim", "Muško", "Selimović", "nedim123", "061111102", null, null, null, "Student", "nedimStudent" },
                    { 11, 15, 8, "lamija.mehic@example.com", "Lamija", "Žensko", "Mehić", "lamija123", "061111103", null, null, null, "Student", "lamijaStudent" },
                    { 12, 12, 13, "adin.mujic@example.com", "Adin", "Muško", "Mujić", "adin123", "061111104", null, null, null, "Student", "adinStudent" },
                    { 13, 7, 11, "nejra.alihodzic@example.com", "Nejra", "Žensko", "Alihodžić", "nejra123", "061111105", null, null, null, "Student", "nejraStudent" }
                });

            migrationBuilder.InsertData(
                table: "SubjectsCategories",
                columns: new[] { "CategoryID", "SubjectID" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 4, 2 },
                    { 6, 3 },
                    { 8, 4 },
                    { 5, 5 },
                    { 7, 6 },
                    { 1, 7 },
                    { 3, 8 },
                    { 3, 9 },
                    { 3, 10 },
                    { 3, 11 },
                    { 3, 12 },
                    { 3, 13 },
                    { 4, 14 },
                    { 3, 15 },
                    { 3, 16 },
                    { 3, 17 },
                    { 3, 18 },
                    { 2, 19 },
                    { 4, 20 },
                    { 4, 21 },
                    { 3, 22 },
                    { 9, 23 },
                    { 6, 24 },
                    { 5, 25 },
                    { 10, 26 },
                    { 1, 27 }
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "ID", "CanApproveRequests", "CanViewLogs", "MyAppUserID", "Role" },
                values: new object[] { 1, true, true, 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "ID", "EducationLevel", "Grade", "MyAppUserID", "PreferredMode" },
                values: new object[,]
                {
                    { 1, 0, "Drugi", 7, "Online" },
                    { 2, 0, "Peti", 8, "InClass" },
                    { 3, 1, "Četvrti", 9, "InClass" },
                    { 4, 1, "Treći", 10, "Online" },
                    { 5, 1, "Prvi", 11, "Online" },
                    { 6, 0, "Sedmi", 12, "Online" },
                    { 7, 0, "Drugi", 13, "InClass" }
                });

            migrationBuilder.InsertData(
                table: "Tutors",
                columns: new[] { "ID", "Availability", "HourlyRate", "IsLiveAvailable", "MyAppUserID", "Policy", "Qualifications", "Rating", "YearsOfExperience" },
                values: new object[,]
                {
                    { 1, "Radnim danima od 17:00 do 20:00", "25KM/h", null, 2, "Potrebno otkazati 24 sata ranije", "Profesor matematike", 0.0, 10 },
                    { 2, "Vikendom od 14:00 - 18:00", "30KM/h", null, 3, "Otkazivanje u roku od 48 sati", "Magistar Engleskog jezika", 0.0, 4 },
                    { 3, "Ponedjeljak - Srijeda od 16:00 do 19:00", "28KM/h", null, 4, "Bez otkazivanja u istom danu", "Diplomirani fizičar", 0.0, 7 },
                    { 4, "Radnim danima poslije 18:00", "35KM/h", null, 5, "Otkazivanje najkasnije 24h ranije", "Profesorica hemije", 0.0, 12 },
                    { 5, "Subotom i nedjeljom 09:00-14:00", "32KM/h", null, 6, "Mogućnost pomjeranja termina", "Master informatike", 0.0, 6 }
                });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "ID", "Duration", "EndTime", "LessonDate", "Mode", "StartTime", "Status", "StudentID", "SubjectID", "TutorID" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2025, 6, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 6, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), 0, 1, 10, 1 },
                    { 2, 0, new DateTime(2025, 6, 10, 12, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2025, 6, 10, 11, 15, 0, 0, DateTimeKind.Unspecified), 0, 2, 1, 1 },
                    { 3, 0, new DateTime(2025, 7, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 7, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, 3, 2, 2 },
                    { 4, 0, new DateTime(2025, 7, 5, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2025, 7, 5, 10, 30, 0, 0, DateTimeKind.Unspecified), 0, 4, 21, 2 },
                    { 5, 0, new DateTime(2025, 8, 12, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 8, 12, 14, 0, 0, 0, DateTimeKind.Unspecified), 0, 5, 7, 3 },
                    { 6, 0, new DateTime(2025, 8, 12, 16, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2025, 8, 12, 15, 15, 0, 0, DateTimeKind.Unspecified), 0, 6, 17, 3 },
                    { 7, 0, new DateTime(2025, 9, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 9, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), 0, 7, 12, 4 },
                    { 8, 0, new DateTime(2025, 9, 1, 15, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2025, 9, 1, 14, 15, 0, 0, DateTimeKind.Unspecified), 0, 1, 9, 4 },
                    { 9, 0, new DateTime(2025, 6, 25, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 6, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, 2, 3, 5 },
                    { 10, 0, new DateTime(2025, 6, 25, 11, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2025, 6, 25, 10, 15, 0, 0, DateTimeKind.Unspecified), 0, 3, 6, 5 }
                });

            migrationBuilder.InsertData(
                table: "StudentsSubjects",
                columns: new[] { "StudentID", "SubjectID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 7 },
                    { 3, 19 },
                    { 4, 24 },
                    { 5, 22 }
                });

            migrationBuilder.InsertData(
                table: "TutorsSubjects",
                columns: new[] { "SubjectID", "TutorID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 10, 1 },
                    { 11, 1 },
                    { 2, 2 },
                    { 4, 2 },
                    { 21, 2 },
                    { 7, 3 },
                    { 17, 3 },
                    { 18, 3 },
                    { 9, 4 },
                    { 12, 4 },
                    { 13, 4 },
                    { 3, 5 },
                    { 5, 5 },
                    { 6, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "StudentsSubjects",
                keyColumns: new[] { "StudentID", "SubjectID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "StudentsSubjects",
                keyColumns: new[] { "StudentID", "SubjectID" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "StudentsSubjects",
                keyColumns: new[] { "StudentID", "SubjectID" },
                keyValues: new object[] { 3, 19 });

            migrationBuilder.DeleteData(
                table: "StudentsSubjects",
                keyColumns: new[] { "StudentID", "SubjectID" },
                keyValues: new object[] { 4, 24 });

            migrationBuilder.DeleteData(
                table: "StudentsSubjects",
                keyColumns: new[] { "StudentID", "SubjectID" },
                keyValues: new object[] { 5, 22 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 8, 4 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 7, 6 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 3, 8 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 3, 9 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 3, 10 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 3, 11 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 3, 12 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 3, 13 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 4, 14 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 3, 15 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 3, 16 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 3, 17 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 3, 18 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 2, 19 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 4, 20 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 4, 21 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 3, 22 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 9, 23 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 6, 24 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 5, 25 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 10, 26 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 1, 27 });

            migrationBuilder.DeleteData(
                table: "TutorsSubjects",
                keyColumns: new[] { "SubjectID", "TutorID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "TutorsSubjects",
                keyColumns: new[] { "SubjectID", "TutorID" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                table: "TutorsSubjects",
                keyColumns: new[] { "SubjectID", "TutorID" },
                keyValues: new object[] { 11, 1 });

            migrationBuilder.DeleteData(
                table: "TutorsSubjects",
                keyColumns: new[] { "SubjectID", "TutorID" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "TutorsSubjects",
                keyColumns: new[] { "SubjectID", "TutorID" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "TutorsSubjects",
                keyColumns: new[] { "SubjectID", "TutorID" },
                keyValues: new object[] { 21, 2 });

            migrationBuilder.DeleteData(
                table: "TutorsSubjects",
                keyColumns: new[] { "SubjectID", "TutorID" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                table: "TutorsSubjects",
                keyColumns: new[] { "SubjectID", "TutorID" },
                keyValues: new object[] { 17, 3 });

            migrationBuilder.DeleteData(
                table: "TutorsSubjects",
                keyColumns: new[] { "SubjectID", "TutorID" },
                keyValues: new object[] { 18, 3 });

            migrationBuilder.DeleteData(
                table: "TutorsSubjects",
                keyColumns: new[] { "SubjectID", "TutorID" },
                keyValues: new object[] { 9, 4 });

            migrationBuilder.DeleteData(
                table: "TutorsSubjects",
                keyColumns: new[] { "SubjectID", "TutorID" },
                keyValues: new object[] { 12, 4 });

            migrationBuilder.DeleteData(
                table: "TutorsSubjects",
                keyColumns: new[] { "SubjectID", "TutorID" },
                keyValues: new object[] { 13, 4 });

            migrationBuilder.DeleteData(
                table: "TutorsSubjects",
                keyColumns: new[] { "SubjectID", "TutorID" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "TutorsSubjects",
                keyColumns: new[] { "SubjectID", "TutorID" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "TutorsSubjects",
                keyColumns: new[] { "SubjectID", "TutorID" },
                keyValues: new object[] { 6, 5 });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Tutors",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tutors",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tutors",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tutors",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tutors",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MyAppUsers",
                keyColumn: "ID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 13);
        }
    }
}
