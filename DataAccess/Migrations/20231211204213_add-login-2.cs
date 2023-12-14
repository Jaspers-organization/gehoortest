using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addlogin2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_employee_login_EmployeeLoginId",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "employee_login");

            migrationBuilder.RenameColumn(
                name: "EmployeeLoginId",
                table: "employee",
                newName: "employee_id");

            migrationBuilder.RenameIndex(
                name: "IX_employee_EmployeeLoginId",
                table: "employee",
                newName: "IX_employee_employee_id");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_employee_login_employee_id",
                table: "employee",
                column: "employee_id",
                principalTable: "employee_login",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_employee_login_employee_id",
                table: "employee");

            migrationBuilder.RenameColumn(
                name: "employee_id",
                table: "employee",
                newName: "EmployeeLoginId");

            migrationBuilder.RenameIndex(
                name: "IX_employee_employee_id",
                table: "employee",
                newName: "IX_employee_EmployeeLoginId");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "employee_login",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_employee_employee_login_EmployeeLoginId",
                table: "employee",
                column: "EmployeeLoginId",
                principalTable: "employee_login",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
