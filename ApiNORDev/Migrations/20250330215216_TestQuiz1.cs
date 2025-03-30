using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiNORDev.Migrations
{
    /// <inheritdoc />
    public partial class TestQuiz1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsQuiz_Quizzes_QuizId",
                table: "QuestionsQuiz");

            migrationBuilder.DropIndex(
                name: "IX_QuestionsQuiz_QuizId",
                table: "QuestionsQuiz");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "QuestionsQuiz");

            migrationBuilder.AlterColumn<string>(
                name: "Titre",
                table: "Quizzes",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuestionQuizIds",
                table: "Quizzes",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionQuizIds",
                table: "Quizzes");

            migrationBuilder.AlterColumn<string>(
                name: "Titre",
                table: "Quizzes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "QuestionsQuiz",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsQuiz_QuizId",
                table: "QuestionsQuiz",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsQuiz_Quizzes_QuizId",
                table: "QuestionsQuiz",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id");
        }
    }
}
