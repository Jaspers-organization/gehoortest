using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addedbasetargetaudience : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "target_audience",
                columns: new[] { "id", "from", "label", "to" },
                values: new object[] { "00000000-0000-0000-0000-000000000000", 0, "Ongekoppelde testen", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "target_audience",
                keyColumn: "id",
                keyValue: "00000000-0000-0000-0000-000000000000");
        }
    }
}
