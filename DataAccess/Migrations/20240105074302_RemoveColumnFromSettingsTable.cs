using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColumnFromSettingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "login_inactive_time",
                table: "settings");

            migrationBuilder.DropColumn(
                name: "logo",
                table: "settings");

            migrationBuilder.DropColumn(
                name: "test_inactive_time",
                table: "settings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "login_inactive_time",
                table: "settings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "logo",
                table: "settings",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "test_inactive_time",
                table: "settings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
