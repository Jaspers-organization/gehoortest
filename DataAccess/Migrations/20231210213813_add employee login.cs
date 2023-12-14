using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addemployeelogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeLoginId",
                table: "employee",
                type: "nvarchar(128)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "employee_login",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_login", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employee_EmployeeLoginId",
                table: "employee",
                column: "EmployeeLoginId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_employee_employee_login_EmployeeLoginId",
                table: "employee",
                column: "EmployeeLoginId",
                principalTable: "employee_login",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_employee_login_EmployeeLoginId",
                table: "employee");

            migrationBuilder.DropTable(
                name: "employee_login");

            migrationBuilder.DropIndex(
                name: "IX_employee_EmployeeLoginId",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "EmployeeLoginId",
                table: "employee");
        }
    }
}
