using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addlogin4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_employee_login_employee_id",
                table: "employee");

            migrationBuilder.DropIndex(
                name: "IX_employee_employee_id",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "employee_id",
                table: "employee");

            migrationBuilder.AddColumn<string>(
                name: "employee_id",
                table: "employee_login",
                type: "nvarchar(128)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_employee_login_employee_id",
                table: "employee_login",
                column: "employee_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_employee_login_employee_employee_id",
                table: "employee_login",
                column: "employee_id",
                principalTable: "employee",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_login_employee_employee_id",
                table: "employee_login");

            migrationBuilder.DropIndex(
                name: "IX_employee_login_employee_id",
                table: "employee_login");

            migrationBuilder.DropColumn(
                name: "employee_id",
                table: "employee_login");

            migrationBuilder.AddColumn<string>(
                name: "employee_id",
                table: "employee",
                type: "nvarchar(128)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_employee_employee_id",
                table: "employee",
                column: "employee_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_employee_employee_login_employee_id",
                table: "employee",
                column: "employee_id",
                principalTable: "employee_login",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
