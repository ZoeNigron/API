using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiNORDev.Migrations
{
    /// <inheritdoc />
    public partial class LeconUtilisateur3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lecons_Utilisateurs_UtilisateurId",
                table: "Lecons");

            migrationBuilder.DropIndex(
                name: "IX_Lecons_UtilisateurId",
                table: "Lecons");

            migrationBuilder.DropColumn(
                name: "UtilisateurId",
                table: "Lecons");

            migrationBuilder.AddColumn<string>(
                name: "LeconsValidees",
                table: "Utilisateurs",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeconsValidees",
                table: "Utilisateurs");

            migrationBuilder.AddColumn<int>(
                name: "UtilisateurId",
                table: "Lecons",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lecons_UtilisateurId",
                table: "Lecons",
                column: "UtilisateurId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lecons_Utilisateurs_UtilisateurId",
                table: "Lecons",
                column: "UtilisateurId",
                principalTable: "Utilisateurs",
                principalColumn: "Id");
        }
    }
}
