using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace colabAPI.Migrations
{
    /// <inheritdoc />
    public partial class Relacionamento_OrientadorProjeto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrientadorId",
                table: "Projetos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjetoId",
                table: "Bolsistas",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_OrientadorId",
                table: "Projetos",
                column: "OrientadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Bolsistas_ProjetoId",
                table: "Bolsistas",
                column: "ProjetoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bolsistas_Projetos_ProjetoId",
                table: "Bolsistas",
                column: "ProjetoId",
                principalTable: "Projetos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projetos_Orientadores_OrientadorId",
                table: "Projetos",
                column: "OrientadorId",
                principalTable: "Orientadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bolsistas_Projetos_ProjetoId",
                table: "Bolsistas");

            migrationBuilder.DropForeignKey(
                name: "FK_Projetos_Orientadores_OrientadorId",
                table: "Projetos");

            migrationBuilder.DropIndex(
                name: "IX_Projetos_OrientadorId",
                table: "Projetos");

            migrationBuilder.DropIndex(
                name: "IX_Bolsistas_ProjetoId",
                table: "Bolsistas");

            migrationBuilder.DropColumn(
                name: "OrientadorId",
                table: "Projetos");

            migrationBuilder.DropColumn(
                name: "ProjetoId",
                table: "Bolsistas");
        }
    }
}
