using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RS1_2024_2025.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedAnotherUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyAppUsers_MyAppUsers_MyAppUserID",
                table: "MyAppUsers");

            migrationBuilder.DropIndex(
                name: "IX_MyAppUsers_MyAppUserID",
                table: "MyAppUsers");

            migrationBuilder.DropColumn(
                name: "CanApproveRequests",
                table: "MyAppUsers");

            migrationBuilder.DropColumn(
                name: "CanViewLogs",
                table: "MyAppUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "MyAppUsers");

            migrationBuilder.DropColumn(
                name: "MyAppUserID",
                table: "MyAppUsers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "MyAppUsers");

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
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_MyAppUserID",
                table: "Admins",
                column: "MyAppUserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.AddColumn<bool>(
                name: "CanApproveRequests",
                table: "MyAppUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CanViewLogs",
                table: "MyAppUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "MyAppUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MyAppUserID",
                table: "MyAppUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "MyAppUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyAppUsers_MyAppUserID",
                table: "MyAppUsers",
                column: "MyAppUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_MyAppUsers_MyAppUsers_MyAppUserID",
                table: "MyAppUsers",
                column: "MyAppUserID",
                principalTable: "MyAppUsers",
                principalColumn: "ID");
        }
    }
}
