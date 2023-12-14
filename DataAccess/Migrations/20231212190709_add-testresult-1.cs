using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addtestresult1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "test_result",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    test_date_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    duration = table.Column<int>(type: "int", nullable: false),
                    has_hearing_loss = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_result", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "test_result");
        }
    }
}
