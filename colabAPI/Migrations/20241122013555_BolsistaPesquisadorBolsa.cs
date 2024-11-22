using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace colabAPI.Migrations
{
    /// <inheritdoc />
    public partial class BolsistaPesquisadorBolsa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pesquisadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Telefone = table.Column<string>(type: "text", nullable: false),
                    Cpf = table.Column<string>(type: "text", nullable: false),
                    Times = table.Column<int[]>(type: "integer[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pesquisadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bolsas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Valor = table.Column<double>(type: "double precision", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataPrevistaFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    Categoria = table.Column<string>(type: "text", nullable: false),
                    PesquisadorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bolsas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bolsas_Pesquisadores_PesquisadorId",
                        column: x => x.PesquisadorId,
                        principalTable: "Pesquisadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orientador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orientador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orientador_Pesquisadores_Id",
                        column: x => x.Id,
                        principalTable: "Pesquisadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bolsistas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    OrientadorId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bolsistas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bolsistas_Orientador_OrientadorId",
                        column: x => x.OrientadorId,
                        principalTable: "Orientador",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bolsistas_Pesquisadores_Id",
                        column: x => x.Id,
                        principalTable: "Pesquisadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bolsas_PesquisadorId",
                table: "Bolsas",
                column: "PesquisadorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bolsistas_OrientadorId",
                table: "Bolsistas",
                column: "OrientadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bolsas");

            migrationBuilder.DropTable(
                name: "Bolsistas");

            migrationBuilder.DropTable(
                name: "Orientador");

            migrationBuilder.DropTable(
                name: "Pesquisadores");
        }
    }
}
