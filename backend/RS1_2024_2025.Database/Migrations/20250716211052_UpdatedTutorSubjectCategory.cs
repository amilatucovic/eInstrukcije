using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RS1_2024_2025.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTutorSubjectCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TutorSubjectCategories",
                table: "TutorSubjectCategories");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "TutorSubjectCategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "TutorSubjectCategories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TutorSubjectCategories",
                table: "TutorSubjectCategories",
                column: "ID");

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
            migrationBuilder.DropPrimaryKey(
                name: "PK_TutorSubjectCategories",
                table: "TutorSubjectCategories");

            migrationBuilder.DropIndex(
                name: "IX_TutorSubjectCategories_TutorID_SubjectID_CategoryID",
                table: "TutorSubjectCategories");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "TutorSubjectCategories");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "TutorSubjectCategories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TutorSubjectCategories",
                table: "TutorSubjectCategories",
                columns: new[] { "TutorID", "SubjectID", "CategoryID" });
        }
    }
}
