using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    employee_number = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    first_name = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    infix = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "target_audience",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    from = table.Column<int>(type: "int", nullable: false),
                    to = table.Column<int>(type: "int", nullable: false),
                    label = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_target_audience", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "test",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    title = table.Column<string>(type: "varchar(50)", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    target_audience_id = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    employee_id = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test", x => x.id);
                    table.ForeignKey(
                        name: "FK_test_employee_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_test_target_audience_target_audience_id",
                        column: x => x.target_audience_id,
                        principalTable: "target_audience",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "text_question",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    question = table.Column<string>(type: "varchar(100)", nullable: false),
                    is_multi_select = table.Column<bool>(type: "bit", nullable: false),
                    has_input_field = table.Column<bool>(type: "bit", nullable: false),
                    question_number = table.Column<int>(type: "int", nullable: false),
                    QuestionType = table.Column<int>(type: "int", nullable: false),
                    test_id = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_text_question", x => x.id);
                    table.ForeignKey(
                        name: "FK_text_question_test_test_id",
                        column: x => x.test_id,
                        principalTable: "test",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tone_audiometry_question",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    frequency = table.Column<int>(type: "int", nullable: false),
                    starting_decibels = table.Column<int>(type: "int", nullable: false),
                    question_number = table.Column<int>(type: "int", nullable: false),
                    QuestionType = table.Column<int>(type: "int", nullable: false),
                    test_id = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tone_audiometry_question", x => x.id);
                    table.ForeignKey(
                        name: "FK_tone_audiometry_question_test_test_id",
                        column: x => x.test_id,
                        principalTable: "test",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "text_question_option",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    option = table.Column<string>(type: "varchar(50)", nullable: false),
                    text_question_id = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_text_question_option", x => x.id);
                    table.ForeignKey(
                        name: "FK_text_question_option_text_question_text_question_id",
                        column: x => x.text_question_id,
                        principalTable: "text_question",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_test_employee_id",
                table: "test",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_test_target_audience_id",
                table: "test",
                column: "target_audience_id");

            migrationBuilder.CreateIndex(
                name: "IX_text_question_test_id",
                table: "text_question",
                column: "test_id");

            migrationBuilder.CreateIndex(
                name: "IX_text_question_option_text_question_id",
                table: "text_question_option",
                column: "text_question_id");

            migrationBuilder.CreateIndex(
                name: "IX_tone_audiometry_question_test_id",
                table: "tone_audiometry_question",
                column: "test_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "text_question_option");

            migrationBuilder.DropTable(
                name: "tone_audiometry_question");

            migrationBuilder.DropTable(
                name: "text_question");

            migrationBuilder.DropTable(
                name: "test");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "target_audience");
        }
    }
}
