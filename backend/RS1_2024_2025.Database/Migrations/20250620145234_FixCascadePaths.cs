using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RS1_2024_2025.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadePaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_MyAppUsers_MyAppUserID",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Lessons_LessonID",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Students_StudentID",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Students_StudentID",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Subjects_SubjectID",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Tutors_TutorID",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MyAppUsers_ReceiverID",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MyAppUsers_SenderID",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_MyAuthenticationTokens_MyAppUsers_MyAppUserId",
                table: "MyAuthenticationTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Lessons_LessonID",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Students_StudentID",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Tutors_TutorID",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationsPayments_Reservations_ReservationID",
                table: "ReservationsPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_MyAppUsers_MyAppUserID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsSubjects_Students_StudentID",
                table: "StudentsSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsSubjects_Subjects_SubjectID",
                table: "StudentsSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectsCategories_Categories_CategoryID",
                table: "SubjectsCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectsCategories_Subjects_SubjectID",
                table: "SubjectsCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Tutors_MyAppUsers_MyAppUserID",
                table: "Tutors");

            migrationBuilder.DropForeignKey(
                name: "FK_TutorsSubjects_Subjects_SubjectID",
                table: "TutorsSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TutorsSubjects_Tutors_TutorID",
                table: "TutorsSubjects");

            migrationBuilder.DropIndex(
                name: "IX_Tutors_MyAppUserID",
                table: "Tutors");

            migrationBuilder.CreateIndex(
                name: "IX_Tutors_MyAppUserID",
                table: "Tutors",
                column: "MyAppUserID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_MyAppUsers_MyAppUserID",
                table: "Admins",
                column: "MyAppUserID",
                principalTable: "MyAppUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Lessons_LessonID",
                table: "Attendances",
                column: "LessonID",
                principalTable: "Lessons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Students_StudentID",
                table: "Attendances",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Students_StudentID",
                table: "Lessons",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Subjects_SubjectID",
                table: "Lessons",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Tutors_TutorID",
                table: "Lessons",
                column: "TutorID",
                principalTable: "Tutors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MyAppUsers_ReceiverID",
                table: "Messages",
                column: "ReceiverID",
                principalTable: "MyAppUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MyAppUsers_SenderID",
                table: "Messages",
                column: "SenderID",
                principalTable: "MyAppUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MyAuthenticationTokens_MyAppUsers_MyAppUserId",
                table: "MyAuthenticationTokens",
                column: "MyAppUserId",
                principalTable: "MyAppUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Lessons_LessonID",
                table: "Reservations",
                column: "LessonID",
                principalTable: "Lessons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Students_StudentID",
                table: "Reservations",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Tutors_TutorID",
                table: "Reservations",
                column: "TutorID",
                principalTable: "Tutors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationsPayments_Reservations_ReservationID",
                table: "ReservationsPayments",
                column: "ReservationID",
                principalTable: "Reservations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_MyAppUsers_MyAppUserID",
                table: "Students",
                column: "MyAppUserID",
                principalTable: "MyAppUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsSubjects_Students_StudentID",
                table: "StudentsSubjects",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsSubjects_Subjects_SubjectID",
                table: "StudentsSubjects",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectsCategories_Categories_CategoryID",
                table: "SubjectsCategories",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectsCategories_Subjects_SubjectID",
                table: "SubjectsCategories",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tutors_MyAppUsers_MyAppUserID",
                table: "Tutors",
                column: "MyAppUserID",
                principalTable: "MyAppUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TutorsSubjects_Subjects_SubjectID",
                table: "TutorsSubjects",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TutorsSubjects_Tutors_TutorID",
                table: "TutorsSubjects",
                column: "TutorID",
                principalTable: "Tutors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_MyAppUsers_MyAppUserID",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Lessons_LessonID",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Students_StudentID",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Students_StudentID",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Subjects_SubjectID",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Tutors_TutorID",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MyAppUsers_ReceiverID",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MyAppUsers_SenderID",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_MyAuthenticationTokens_MyAppUsers_MyAppUserId",
                table: "MyAuthenticationTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Lessons_LessonID",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Students_StudentID",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Tutors_TutorID",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationsPayments_Reservations_ReservationID",
                table: "ReservationsPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_MyAppUsers_MyAppUserID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsSubjects_Students_StudentID",
                table: "StudentsSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsSubjects_Subjects_SubjectID",
                table: "StudentsSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectsCategories_Categories_CategoryID",
                table: "SubjectsCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectsCategories_Subjects_SubjectID",
                table: "SubjectsCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Tutors_MyAppUsers_MyAppUserID",
                table: "Tutors");

            migrationBuilder.DropForeignKey(
                name: "FK_TutorsSubjects_Subjects_SubjectID",
                table: "TutorsSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TutorsSubjects_Tutors_TutorID",
                table: "TutorsSubjects");

            migrationBuilder.DropIndex(
                name: "IX_Tutors_MyAppUserID",
                table: "Tutors");

            migrationBuilder.CreateIndex(
                name: "IX_Tutors_MyAppUserID",
                table: "Tutors",
                column: "MyAppUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_MyAppUsers_MyAppUserID",
                table: "Admins",
                column: "MyAppUserID",
                principalTable: "MyAppUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Lessons_LessonID",
                table: "Attendances",
                column: "LessonID",
                principalTable: "Lessons",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Students_StudentID",
                table: "Attendances",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Students_StudentID",
                table: "Lessons",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Subjects_SubjectID",
                table: "Lessons",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Tutors_TutorID",
                table: "Lessons",
                column: "TutorID",
                principalTable: "Tutors",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MyAppUsers_ReceiverID",
                table: "Messages",
                column: "ReceiverID",
                principalTable: "MyAppUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MyAppUsers_SenderID",
                table: "Messages",
                column: "SenderID",
                principalTable: "MyAppUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MyAuthenticationTokens_MyAppUsers_MyAppUserId",
                table: "MyAuthenticationTokens",
                column: "MyAppUserId",
                principalTable: "MyAppUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Lessons_LessonID",
                table: "Reservations",
                column: "LessonID",
                principalTable: "Lessons",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Students_StudentID",
                table: "Reservations",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Tutors_TutorID",
                table: "Reservations",
                column: "TutorID",
                principalTable: "Tutors",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationsPayments_Reservations_ReservationID",
                table: "ReservationsPayments",
                column: "ReservationID",
                principalTable: "Reservations",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_MyAppUsers_MyAppUserID",
                table: "Students",
                column: "MyAppUserID",
                principalTable: "MyAppUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsSubjects_Students_StudentID",
                table: "StudentsSubjects",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsSubjects_Subjects_SubjectID",
                table: "StudentsSubjects",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectsCategories_Categories_CategoryID",
                table: "SubjectsCategories",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectsCategories_Subjects_SubjectID",
                table: "SubjectsCategories",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tutors_MyAppUsers_MyAppUserID",
                table: "Tutors",
                column: "MyAppUserID",
                principalTable: "MyAppUsers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TutorsSubjects_Subjects_SubjectID",
                table: "TutorsSubjects",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TutorsSubjects_Tutors_TutorID",
                table: "TutorsSubjects",
                column: "TutorID",
                principalTable: "Tutors",
                principalColumn: "ID");
        }
    }
}
