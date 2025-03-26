using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiNORDev.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BonneReponse",
                table: "QuestionsQuiz");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BonneReponse",
                table: "QuestionsQuiz",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
