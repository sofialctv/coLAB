using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace colabAPI.Migrations
{
    /// <inheritdoc />
    public partial class ReformulacaoModuloProjeto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projetos_Orientadores_OrientadorId",
                table: "Projetos");

            migrationBuilder.DropTable(
                name: "ProjetoBolsista");

            migrationBuilder.DropIndex(
                name: "IX_Projetos_OrientadorId",
                table: "Projetos");

            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Projetos");

            migrationBuilder.DropColumn(
                name: "OrientadorId",
                table: "Projetos");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Projetos");

            migrationBuilder.AddColumn<int>(
                name: "ProjetoId",
                table: "Bolsas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjetoId1",
                table: "Bolsas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HistoricoStatusProjetos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProjetoId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoStatusProjetos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoStatusProjetos_Projetos_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projetos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bolsas_ProjetoId",
                table: "Bolsas",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_Bolsas_ProjetoId1",
                table: "Bolsas",
                column: "ProjetoId1");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoStatusProjetos_ProjetoId",
                table: "HistoricoStatusProjetos",
                column: "ProjetoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bolsas_Projetos_ProjetoId",
                table: "Bolsas",
                column: "ProjetoId",
                principalTable: "Projetos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bolsas_Projetos_ProjetoId1",
                table: "Bolsas",
                column: "ProjetoId1",
                principalTable: "Projetos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bolsas_Projetos_ProjetoId",
                table: "Bolsas");

            migrationBuilder.DropForeignKey(
                name: "FK_Bolsas_Projetos_ProjetoId1",
                table: "Bolsas");

            migrationBuilder.DropTable(
                name: "HistoricoStatusProjetos");

            migrationBuilder.DropIndex(
                name: "IX_Bolsas_ProjetoId",
                table: "Bolsas");

            migrationBuilder.DropIndex(
                name: "IX_Bolsas_ProjetoId1",
                table: "Bolsas");

            migrationBuilder.DropColumn(
                name: "ProjetoId",
                table: "Bolsas");

            migrationBuilder.DropColumn(
                name: "ProjetoId1",
                table: "Bolsas");

            migrationBuilder.AddColumn<int>(
                name: "Categoria",
                table: "Projetos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrientadorId",
                table: "Projetos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Projetos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_Projetos_OrientadorId",
                table: "Projetos",
                column: "OrientadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoBolsista_ProjetoId",
                table: "ProjetoBolsista",
                column: "ProjetoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projetos_Orientadores_OrientadorId",
                table: "Projetos",
                column: "OrientadorId",
                principalTable: "Orientadores",
                principalColumn: "Id");
        }
    }
}
