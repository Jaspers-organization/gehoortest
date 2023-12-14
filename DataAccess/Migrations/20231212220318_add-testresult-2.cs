using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addtestresult2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "target_audience",
                table: "test_result",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "text_question_result",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    question = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    test_result_id = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_text_question_result", x => x.id);
                    table.ForeignKey(
                        name: "FK_text_question_result_test_result_test_result_id",
                        column: x => x.test_result_id,
                        principalTable: "test_result",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tone_audiometry_question_result",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    frequency = table.Column<int>(type: "int", nullable: false),
                    starting_decibels = table.Column<int>(type: "int", nullable: false),
                    lowest_decibels = table.Column<int>(type: "int", nullable: false),
                    ear = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    test_result_id = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tone_audiometry_question_result", x => x.id);
                    table.ForeignKey(
                        name: "FK_tone_audiometry_question_result_test_result_test_result_id",
                        column: x => x.test_result_id,
                        principalTable: "test_result",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "text_question_answer_result",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    answer = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    text_question_result_id = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_text_question_answer_result", x => x.id);
                    table.ForeignKey(
                        name: "FK_text_question_answer_result_text_question_result_text_question_result_id",
                        column: x => x.text_question_result_id,
                        principalTable: "text_question_result",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "text_question_option_result",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    option = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    text_question_result_id = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_text_question_option_result", x => x.id);
                    table.ForeignKey(
                        name: "FK_text_question_option_result_text_question_result_text_question_result_id",
                        column: x => x.text_question_result_id,
                        principalTable: "text_question_result",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_text_question_answer_result_text_question_result_id",
                table: "text_question_answer_result",
                column: "text_question_result_id");

            migrationBuilder.CreateIndex(
                name: "IX_text_question_option_result_text_question_result_id",
                table: "text_question_option_result",
                column: "text_question_result_id");

            migrationBuilder.CreateIndex(
                name: "IX_text_question_result_test_result_id",
                table: "text_question_result",
                column: "test_result_id");

            migrationBuilder.CreateIndex(
                name: "IX_tone_audiometry_question_result_test_result_id",
                table: "tone_audiometry_question_result",
                column: "test_result_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "text_question_answer_result");

            migrationBuilder.DropTable(
                name: "text_question_option_result");

            migrationBuilder.DropTable(
                name: "tone_audiometry_question_result");

            migrationBuilder.DropTable(
                name: "text_question_result");

            migrationBuilder.DropColumn(
                name: "target_audience",
                table: "test_result");
        }
    }
}
