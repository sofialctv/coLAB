using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace colab.Migrations
{
    /// <inheritdoc />
    public partial class Refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bolsas_TipoBolsa_TipoBolsaId",
                table: "Bolsas");

            migrationBuilder.DropTable(
                name: "TipoBolsa");

            migrationBuilder.DropIndex(
                name: "IX_HistoricosCargo_CargoId",
                table: "HistoricosCargo");

            migrationBuilder.DropIndex(
                name: "IX_Bolsas_TipoBolsaId",
                table: "Bolsas");

            migrationBuilder.DropColumn(
                name: "TipoBolsaId",
                table: "Bolsas");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Bolsas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlanoTrabalho",
                table: "Bolsas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosCargo_CargoId",
                table: "HistoricosCargo",
                column: "CargoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HistoricosCargo_CargoId",
                table: "HistoricosCargo");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Bolsas");

            migrationBuilder.DropColumn(
                name: "PlanoTrabalho",
                table: "Bolsas");

            migrationBuilder.AddColumn<int>(
                name: "TipoBolsaId",
                table: "Bolsas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TipoBolsa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    escolaridade = table.Column<string>(type: "text", nullable: false),
                    nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoBolsa", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosCargo_CargoId",
                table: "HistoricosCargo",
                column: "CargoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bolsas_TipoBolsaId",
                table: "Bolsas",
                column: "TipoBolsaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bolsas_TipoBolsa_TipoBolsaId",
                table: "Bolsas",
                column: "TipoBolsaId",
                principalTable: "TipoBolsa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
