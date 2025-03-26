using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiNORDev.Migrations
{
    /// <inheritdoc />
    public partial class Quiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionsQuiz",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Question = table.Column<string>(type: "TEXT", nullable: false),
                    BonneReponse = table.Column<int>(type: "INTEGER", nullable: false),
                    Explication = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsQuiz", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Texte = table.Column<string>(type: "TEXT", nullable: false),
                    EstCorrecte = table.Column<bool>(type: "INTEGER", nullable: false),
                    QuestionQuizId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_QuestionsQuiz_QuestionQuizId",
                        column: x => x.QuestionQuizId,
                        principalTable: "QuestionsQuiz",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionQuizId",
                table: "Options",
                column: "QuestionQuizId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "QuestionsQuiz");
        }
    }
}
