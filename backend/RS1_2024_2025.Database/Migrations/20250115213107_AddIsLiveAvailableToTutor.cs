using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RS1_2024_2025.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddIsLiveAvailableToTutor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLiveAvailable",
                table: "Tutors",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLiveAvailable",
                table: "Tutors");
        }
    }
}
