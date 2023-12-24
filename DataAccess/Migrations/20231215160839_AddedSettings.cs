using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "settings",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    color = table.Column<string>(type: "nvarchar(50)", nullable: false, defaultValue: "#DA0063"),
                    logo = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    test_inactive_time = table.Column<int>(type: "int", nullable: false),
                    login_inactive_time = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_settings", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "settings");
        }
    }
}
