using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RS1_2024_2025.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSubjectCategorySeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 2, 1 });

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
                keyValues: new object[] { 3, 22 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 9, 23 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 5, 25 });

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 22);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 1,
                column: "Name",
                value: "Algebra");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 2,
                column: "Name",
                value: "Geometrija");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 3,
                column: "Name",
                value: "Gramatika");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 4,
                column: "Name",
                value: "Pravopis");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 5,
                column: "Name",
                value: "Optika");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 6,
                column: "Name",
                value: "Kinematika");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 7,
                column: "Name",
                value: "Termodinamika");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 8,
                column: "Name",
                value: "Programiranje");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 9,
                column: "Name",
                value: "Baze podataka");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 10,
                column: "Name",
                value: "Web razvoj i dizajn");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 11, "OOP" },
                    { 12, "Neuroanatomija" },
                    { 13, "Kosti" },
                    { 14, "Kotiranje" },
                    { 15, "Tehničko pismo" },
                    { 16, "Nacrtna geometrija" },
                    { 17, "Trigonometrija" },
                    { 18, "Genetika" },
                    { 19, "Deklinacije i komparacije pridjeva" },
                    { 20, "Latinske poslovice" },
                    { 21, "Ohmov zakon i Kirhofova pravila" },
                    { 22, "Strujni krugovi" },
                    { 23, "Hemijske jednačine" },
                    { 24, "Hemijske veze" },
                    { 25, "Organska hemija" },
                    { 26, "Brojni sistemi" },
                    { 27, "Konverzacija" }
                });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Osnove njemačkog jezika: govor, čitanje i pisanje", "Njemački jezik" });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Osnove turskog jezika: govor, čitanje i pisanje", "Turski jezik" });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 14,
                columns: new[] { "Description", "DifficultyLevel", "Name" },
                values: new object[] { "Napredna fizika, strujni krugovi, sheme", "srednja skola", "Osnove elektrotehnike" });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 15,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Osnove ljudske građe tijela, kosti, mišići, anatomija nervnog sistema", "Anatomija" });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 17,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Tehničko pismo, kotiranje, tlocrt, nacrt, bokocrt", "Tehničko crtanje" });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 18,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Osnovni pojmovi u mikrobiologiji, imenovanje preparata, prepoznavanje mikrobioloskih organizama", "Mikrobiologija" });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 23,
                column: "Description",
                value: "Osnovni koncepti bioloških nauka, ekologija, anatomija i evolucija");

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 27,
                column: "Description",
                value: "Napredne tehnike i metode u matematici (trigonometrija, analitička geometrija)");

            migrationBuilder.InsertData(
                table: "SubjectsCategories",
                columns: new[] { "CategoryID", "SubjectID" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 4, 1 },
                    { 3, 2 },
                    { 5, 3 },
                    { 7, 3 },
                    { 2, 7 },
                    { 4, 12 },
                    { 4, 13 },
                    { 3, 19 },
                    { 3, 20 },
                    { 3, 21 },
                    { 5, 24 },
                    { 7, 24 },
                    { 8, 26 },
                    { 9, 26 },
                    { 2, 27 },
                    { 27, 2 },
                    { 22, 3 },
                    { 23, 5 },
                    { 24, 5 },
                    { 25, 5 },
                    { 27, 12 },
                    { 27, 13 },
                    { 21, 14 },
                    { 22, 14 },
                    { 12, 15 },
                    { 13, 15 },
                    { 14, 17 },
                    { 15, 17 },
                    { 16, 17 },
                    { 19, 19 },
                    { 20, 19 },
                    { 27, 20 },
                    { 27, 21 },
                    { 21, 24 },
                    { 22, 24 },
                    { 23, 25 },
                    { 24, 25 },
                    { 25, 25 },
                    { 11, 26 },
                    { 26, 26 },
                    { 17, 27 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 27, 2 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 22, 3 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 23, 5 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 24, 5 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 25, 5 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 4, 12 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 27, 12 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 4, 13 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 27, 13 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 21, 14 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 22, 14 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 12, 15 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 13, 15 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 14, 17 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 15, 17 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 16, 17 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 3, 19 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 19, 19 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 20, 19 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 3, 20 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 27, 20 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 3, 21 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 27, 21 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 5, 24 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 7, 24 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 21, 24 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 22, 24 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 23, 25 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 24, 25 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 25, 25 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 8, 26 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 9, 26 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 11, 26 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 26, 26 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 2, 27 });

            migrationBuilder.DeleteData(
                table: "SubjectsCategories",
                keyColumns: new[] { "CategoryID", "SubjectID" },
                keyValues: new object[] { 17, 27 });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 27);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 1,
                column: "Name",
                value: "Matematika");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 2,
                column: "Name",
                value: "Bosanski jezik");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 3,
                column: "Name",
                value: "Priroda i društvo");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 4,
                column: "Name",
                value: "Engleski jezik");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 5,
                column: "Name",
                value: "Hemija");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 6,
                column: "Name",
                value: "Fizika");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 7,
                column: "Name",
                value: "Historija");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 8,
                column: "Name",
                value: "Geografija");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 9,
                column: "Name",
                value: "Biologija");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 10,
                column: "Name",
                value: "Informatika");

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Fizička aktivnost i usvajanje zdravih životnih navika", "Tjelesni i zdravstveni odgoj" });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Upoznavanje sa osnovnim religijskim pojmovima i vrijednostima", "Vjeronauka" });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 14,
                columns: new[] { "Description", "DifficultyLevel", "Name" },
                values: new object[] { "Osnove njemačkog jezika: govor, čitanje i pisanje", "osnovna skola", "Njemački jezik" });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 15,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Proučavanje društva, normi, vrijednosti i socijalnih grupa", "Sociologija" });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 17,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Uvod u logičko razmišljanje, etiku i epistemologiju", "Filozofija" });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 18,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Formalno razmišljanje, argumentacija i zaključivanje", "Logika" });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 23,
                column: "Description",
                value: "Osnovni koncepti bioloških nauka, ekologija i evolucija");

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "ID",
                keyValue: 27,
                column: "Description",
                value: "Napredne tehnike i metode u matematici");

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "ID", "Description", "DifficultyLevel", "Name" },
                values: new object[,]
                {
                    { 16, "Osnove ljudskog ponašanja i mentalnih procesa", "srednja skola", "Psihologija" },
                    { 22, "Unapređenje fizičke spremnosti i zdravih stilova života", "srednja skola", "Tjelesni i zdravstveni odgoj" }
                });

            migrationBuilder.InsertData(
                table: "SubjectsCategories",
                columns: new[] { "CategoryID", "SubjectID" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 8, 4 },
                    { 5, 5 },
                    { 7, 6 },
                    { 3, 8 },
                    { 3, 9 },
                    { 3, 10 },
                    { 3, 11 },
                    { 4, 14 },
                    { 3, 15 },
                    { 3, 17 },
                    { 3, 18 },
                    { 2, 19 },
                    { 9, 23 },
                    { 5, 25 },
                    { 3, 16 },
                    { 3, 22 }
                });
        }
    }
}
