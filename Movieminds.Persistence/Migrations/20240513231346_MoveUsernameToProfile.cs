using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movieminds.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MoveUsernameToProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Username",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Profile",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_Name",
                table: "Profile",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Profile_Name",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Profile");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "User",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);
        }
    }
}
