using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedEntityTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tone_audiometry_question_test_TestId",
                table: "tone_audiometry_question");

            migrationBuilder.RenameColumn(
                name: "TestId",
                table: "tone_audiometry_question",
                newName: "test_id");

            migrationBuilder.RenameIndex(
                name: "IX_tone_audiometry_question_TestId",
                table: "tone_audiometry_question",
                newName: "IX_tone_audiometry_question_test_id");

            migrationBuilder.AlterColumn<string>(
                name: "option",
                table: "text_question_option",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "question",
                table: "text_question",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "test",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "label",
                table: "target_audience",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                table: "employee",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "infix",
                table: "employee",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                table: "employee",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "employee_number",
                table: "employee",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tone_audiometry_question_test_test_id",
                table: "tone_audiometry_question",
                column: "test_id",
                principalTable: "test",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tone_audiometry_question_test_test_id",
                table: "tone_audiometry_question");

            migrationBuilder.RenameColumn(
                name: "test_id",
                table: "tone_audiometry_question",
                newName: "TestId");

            migrationBuilder.RenameIndex(
                name: "IX_tone_audiometry_question_test_id",
                table: "tone_audiometry_question",
                newName: "IX_tone_audiometry_question_TestId");

            migrationBuilder.AlterColumn<string>(
                name: "option",
                table: "text_question_option",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "question",
                table: "text_question",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "test",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "label",
                table: "target_audience",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                table: "employee",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            migrationBuilder.AlterColumn<string>(
                name: "infix",
                table: "employee",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                table: "employee",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "employee_number",
                table: "employee",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tone_audiometry_question_test_TestId",
                table: "tone_audiometry_question",
                column: "TestId",
                principalTable: "test",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
