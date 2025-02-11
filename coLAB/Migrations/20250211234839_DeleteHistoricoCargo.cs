using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace colab.Migrations
{
    /// <inheritdoc />
    public partial class DeleteHistoricoCargo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricosCargo");

            migrationBuilder.DropIndex(
                name: "IX_Bolsas_PessoaId",
                table: "Bolsas");

            migrationBuilder.AddColumn<int>(
                name: "CargoId",
                table: "Bolsas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bolsas_CargoId",
                table: "Bolsas",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Bolsas_PessoaId",
                table: "Bolsas",
                column: "PessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bolsas_Cargos_CargoId",
                table: "Bolsas",
                column: "CargoId",
                principalTable: "Cargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bolsas_Cargos_CargoId",
                table: "Bolsas");

            migrationBuilder.DropIndex(
                name: "IX_Bolsas_CargoId",
                table: "Bolsas");

            migrationBuilder.DropIndex(
                name: "IX_Bolsas_PessoaId",
                table: "Bolsas");

            migrationBuilder.DropColumn(
                name: "CargoId",
                table: "Bolsas");

            migrationBuilder.CreateTable(
                name: "HistoricosCargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CargoId = table.Column<int>(type: "integer", nullable: false),
                    PessoaId = table.Column<int>(type: "integer", nullable: false),
                    Data_fim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Data_inicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosCargo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricosCargo_Cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoricosCargo_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bolsas_PessoaId",
                table: "Bolsas",
                column: "PessoaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosCargo_CargoId",
                table: "HistoricosCargo",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosCargo_PessoaId",
                table: "HistoricosCargo",
                column: "PessoaId");
        }
    }
}
