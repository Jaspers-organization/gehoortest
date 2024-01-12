using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class maketargetaudiencenullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tone_audiometry_question_test_test_id",
                table: "tone_audiometry_question");

            migrationBuilder.AddForeignKey(
                name: "FK_tone_audiometry_question_test_test_id",
                table: "tone_audiometry_question",
                column: "test_id",
                principalTable: "test",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tone_audiometry_question_test_test_id",
                table: "tone_audiometry_question");

            migrationBuilder.AddForeignKey(
                name: "FK_tone_audiometry_question_test_test_id",
                table: "tone_audiometry_question",
                column: "test_id",
                principalTable: "test",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
