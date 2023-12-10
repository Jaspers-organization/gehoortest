using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseUpdatebeforemerge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_text_question_option_text_question_text_question_id",
                table: "text_question_option");

            migrationBuilder.AddForeignKey(
                name: "FK_text_question_option_text_question_text_question_id",
                table: "text_question_option",
                column: "text_question_id",
                principalTable: "text_question",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_text_question_option_text_question_text_question_id",
                table: "text_question_option");

            migrationBuilder.AddForeignKey(
                name: "FK_text_question_option_text_question_text_question_id",
                table: "text_question_option",
                column: "text_question_id",
                principalTable: "text_question",
                principalColumn: "id");
        }
    }
}
