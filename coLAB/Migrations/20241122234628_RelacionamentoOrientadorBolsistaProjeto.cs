using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace colabAPI.Migrations
{
    /// <inheritdoc />
    public partial class RelacionamentoOrientadorBolsistaProjeto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bolsistas_Projetos_ProjetoId",
                table: "Bolsistas");

            migrationBuilder.DropIndex(
                name: "IX_Bolsistas_ProjetoId",
                table: "Bolsistas");

            migrationBuilder.DropColumn(
                name: "ProjetoId",
                table: "Bolsistas");

            migrationBuilder.CreateTable(
                name: "ProjetoBolsista",
                columns: table => new
                {
                    BolsistaId = table.Column<int>(type: "integer", nullable: false),
                    ProjetoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjetoBolsista", x => new { x.BolsistaId, x.ProjetoId });
                    table.ForeignKey(
                        name: "FK_ProjetoBolsista_Bolsistas_BolsistaId",
                        column: x => x.BolsistaId,
                        principalTable: "Bolsistas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjetoBolsista_Projetos_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projetos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoBolsista_ProjetoId",
                table: "ProjetoBolsista",
                column: "ProjetoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjetoBolsista");

            migrationBuilder.AddColumn<int>(
                name: "ProjetoId",
                table: "Bolsistas",
                type: "integer",
                nullable: true);

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
        }
    }
}
