using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RS1_2024_2025.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddTutorSubjectCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TutorSubjectCategories",
                columns: table => new
                {
                    TutorID = table.Column<int>(type: "int", nullable: false),
                    SubjectID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorSubjectCategories", x => new { x.TutorID, x.SubjectID, x.CategoryID });
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

            migrationBuilder.CreateIndex(
                name: "IX_TutorSubjectCategories_CategoryID",
                table: "TutorSubjectCategories",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_TutorSubjectCategories_SubjectID",
                table: "TutorSubjectCategories",
                column: "SubjectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TutorSubjectCategories");
        }
    }
}
