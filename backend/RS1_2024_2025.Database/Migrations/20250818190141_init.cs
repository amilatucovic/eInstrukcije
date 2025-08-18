using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RS1_2024_2025.Database.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DifficultyLevel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MyAppUsers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyAppUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MyAppUsers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SubjectsCategories",
                columns: table => new
                {
                    SubjectID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectsCategories", x => new { x.SubjectID, x.CategoryID });
                    table.ForeignKey(
                        name: "FK_SubjectsCategories_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectsCategories_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanApproveRequests = table.Column<bool>(type: "bit", nullable: false),
                    CanViewLogs = table.Column<bool>(type: "bit", nullable: false),
                    MyAppUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Admins_MyAppUsers_MyAppUserID",
                        column: x => x.MyAppUserID,
                        principalTable: "MyAppUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderID = table.Column<int>(type: "int", nullable: false),
                    ReceiverID = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Messages_MyAppUsers_ReceiverID",
                        column: x => x.ReceiverID,
                        principalTable: "MyAppUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_MyAppUsers_SenderID",
                        column: x => x.SenderID,
                        principalTable: "MyAppUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MyAuthenticationTokens",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MyAppUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyAuthenticationTokens", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MyAuthenticationTokens_MyAppUsers_MyAppUserId",
                        column: x => x.MyAppUserId,
                        principalTable: "MyAppUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreferredMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EducationLevel = table.Column<int>(type: "int", nullable: false),
                    MyAppUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Students_MyAppUsers_MyAppUserID",
                        column: x => x.MyAppUserID,
                        principalTable: "MyAppUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tutors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Qualifications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: false),
                    Availability = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Policy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HourlyRate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsLiveAvailable = table.Column<bool>(type: "bit", nullable: true),
                    MyAppUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tutors_MyAppUsers_MyAppUserID",
                        column: x => x.MyAppUserID,
                        principalTable: "MyAppUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentsSubjects",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    SubjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsSubjects", x => new { x.StudentID, x.SubjectID });
                    table.ForeignKey(
                        name: "FK_StudentsSubjects_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsSubjects_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    TutorID = table.Column<int>(type: "int", nullable: false),
                    SubjectID = table.Column<int>(type: "int", nullable: false),
                    LessonDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Mode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Lessons_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lessons_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lessons_Tutors_TutorID",
                        column: x => x.TutorID,
                        principalTable: "Tutors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    TutorID = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ReviewText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reviews_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Tutors_TutorID",
                        column: x => x.TutorID,
                        principalTable: "Tutors",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TutorsSubjects",
                columns: table => new
                {
                    TutorID = table.Column<int>(type: "int", nullable: false),
                    SubjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorsSubjects", x => new { x.TutorID, x.SubjectID });
                    table.ForeignKey(
                        name: "FK_TutorsSubjects_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorsSubjects_Tutors_TutorID",
                        column: x => x.TutorID,
                        principalTable: "Tutors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TutorSubjectCategories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TutorID = table.Column<int>(type: "int", nullable: false),
                    SubjectID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorSubjectCategories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TutorSubjectCategories_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorSubjectCategories_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorSubjectCategories_Tutors_TutorID",
                        column: x => x.TutorID,
                        principalTable: "Tutors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    LessonID = table.Column<int>(type: "int", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    IsPresent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => new { x.LessonID, x.StudentID });
                    table.ForeignKey(
                        name: "FK_Attendances_Lessons_LessonID",
                        column: x => x.LessonID,
                        principalTable: "Lessons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendances_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    TutorID = table.Column<int>(type: "int", nullable: false),
                    LessonID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reservations_Lessons_LessonID",
                        column: x => x.LessonID,
                        principalTable: "Lessons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Tutors_TutorID",
                        column: x => x.TutorID,
                        principalTable: "Tutors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationsPayments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TutorExpencesAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TutorExpencesPaid = table.Column<bool>(type: "bit", nullable: false),
                    LessonID = table.Column<int>(type: "int", nullable: true),
                    StudentID = table.Column<int>(type: "int", nullable: true),
                    TutorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationsPayments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReservationsPayments_Lessons_LessonID",
                        column: x => x.LessonID,
                        principalTable: "Lessons",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ReservationsPayments_Reservations_ReservationID",
                        column: x => x.ReservationID,
                        principalTable: "Reservations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationsPayments_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ReservationsPayments_Tutors_TutorID",
                        column: x => x.TutorID,
                        principalTable: "Tutors",
                        principalColumn: "ID");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Algebra" },
                    { 2, "Geometrija" },
                    { 3, "Gramatika" },
                    { 4, "Pravopis" },
                    { 5, "Optika" },
                    { 6, "Kinematika" },
                    { 7, "Termodinamika" },
                    { 8, "Programiranje" },
                    { 9, "Baze podataka" },
                    { 10, "Web razvoj i dizajn" },
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
                    { 12, "Osnove njemačkog jezika: govor, čitanje i pisanje", "osnovna skola", "Njemački jezik" },
                    { 13, "Osnove turskog jezika: govor, čitanje i pisanje", "osnovna skola", "Turski jezik" },
                    { 14, "Napredna fizika, strujni krugovi, sheme", "srednja skola", "Osnove elektrotehnike" },
                    { 15, "Osnove ljudske građe tijela, kosti, mišići, anatomija nervnog sistema", "srednja skola", "Anatomija" },
                    { 17, "Tehničko pismo, kotiranje, tlocrt, nacrt, bokocrt", "srednja skola", "Tehničko crtanje" },
                    { 18, "Osnovni pojmovi u mikrobiologiji, imenovanje preparata, prepoznavanje mikrobioloskih organizama", "srednja skola", "Mikrobiologija" },
                    { 19, "Osnove klasičnog jezika i njegov utjecaj na savremene jezike", "srednja skola", "Latinski jezik" },
                    { 20, "Napredna gramatika, vokabular i konverzacija", "srednja skola", "Njemački jezik" },
                    { 21, "Analiza tekstova, napredno pisanje i komunikacija", "srednja skola", "Francuski jezik" },
                    { 23, "Osnovni koncepti bioloških nauka, ekologija, anatomija i evolucija", "srednja skola", "Biologija" },
                    { 24, "Istraživanje naprednih principa u fizici i njihovih primjena", "srednja skola", "Fizika" },
                    { 25, "Napredni koncepti hemije i njihova primjena", "srednja skola", "Hemija" },
                    { 26, "Razumijevanje računarskih sistema, programiranja i tehnologije", "srednja skola", "Informatika" },
                    { 27, "Napredne tehnike i metode u matematici (trigonometrija, analitička geometrija)", "srednja skola", "Matematika" }
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
                    { 3, 1 },
                    { 4, 1 },
                    { 3, 2 },
                    { 4, 2 },
                    { 27, 2 },
                    { 5, 3 },
                    { 6, 3 },
                    { 7, 3 },
                    { 22, 3 },
                    { 23, 5 },
                    { 24, 5 },
                    { 25, 5 },
                    { 1, 7 },
                    { 2, 7 },
                    { 3, 12 },
                    { 4, 12 },
                    { 27, 12 },
                    { 3, 13 },
                    { 4, 13 },
                    { 27, 13 },
                    { 21, 14 },
                    { 22, 14 },
                    { 12, 15 },
                    { 13, 15 },
                    { 14, 17 },
                    { 15, 17 },
                    { 16, 17 },
                    { 3, 19 },
                    { 19, 19 },
                    { 20, 19 },
                    { 3, 20 },
                    { 4, 20 },
                    { 27, 20 },
                    { 3, 21 },
                    { 4, 21 },
                    { 27, 21 },
                    { 5, 24 },
                    { 6, 24 },
                    { 7, 24 },
                    { 21, 24 },
                    { 22, 24 },
                    { 23, 25 },
                    { 24, 25 },
                    { 25, 25 },
                    { 8, 26 },
                    { 9, 26 },
                    { 10, 26 },
                    { 11, 26 },
                    { 26, 26 },
                    { 1, 27 },
                    { 2, 27 },
                    { 17, 27 }
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
                    { 7, 0, new DateTime(2025, 9, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 9, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), 0, 7, 12, 4 }
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

            migrationBuilder.CreateIndex(
                name: "IX_Admins_MyAppUserID",
                table: "Admins",
                column: "MyAppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_StudentID",
                table: "Attendances",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_StudentID",
                table: "Lessons",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_SubjectID",
                table: "Lessons",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TutorID",
                table: "Lessons",
                column: "TutorID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverID",
                table: "Messages",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderID",
                table: "Messages",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "IX_MyAppUsers_CityId",
                table: "MyAppUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_MyAuthenticationTokens_MyAppUserId",
                table: "MyAuthenticationTokens",
                column: "MyAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_LessonID",
                table: "Reservations",
                column: "LessonID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_StudentID",
                table: "Reservations",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TutorID",
                table: "Reservations",
                column: "TutorID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationsPayments_LessonID",
                table: "ReservationsPayments",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationsPayments_ReservationID",
                table: "ReservationsPayments",
                column: "ReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationsPayments_StudentID",
                table: "ReservationsPayments",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationsPayments_TutorID",
                table: "ReservationsPayments",
                column: "TutorID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_StudentID",
                table: "Reviews",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TutorID",
                table: "Reviews",
                column: "TutorID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_MyAppUserID",
                table: "Students",
                column: "MyAppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsSubjects_SubjectID",
                table: "StudentsSubjects",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectsCategories_CategoryID",
                table: "SubjectsCategories",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Tutors_MyAppUserID",
                table: "Tutors",
                column: "MyAppUserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TutorsSubjects_SubjectID",
                table: "TutorsSubjects",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TutorSubjectCategories_CategoryID",
                table: "TutorSubjectCategories",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_TutorSubjectCategories_SubjectID",
                table: "TutorSubjectCategories",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TutorSubjectCategories_TutorID_SubjectID_CategoryID",
                table: "TutorSubjectCategories",
                columns: new[] { "TutorID", "SubjectID", "CategoryID" },
                unique: true,
                filter: "[CategoryID] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "MyAuthenticationTokens");

            migrationBuilder.DropTable(
                name: "ReservationsPayments");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "StudentsSubjects");

            migrationBuilder.DropTable(
                name: "SubjectsCategories");

            migrationBuilder.DropTable(
                name: "TutorsSubjects");

            migrationBuilder.DropTable(
                name: "TutorSubjectCategories");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Tutors");

            migrationBuilder.DropTable(
                name: "MyAppUsers");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
