using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace colab.Migrations
{
    /// <inheritdoc />
    public partial class AjusteRelacaoBolsaProjeto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bolsas_Projetos_ProjetoId",
                table: "Bolsas");

            migrationBuilder.AlterColumn<int>(
                name: "ProjetoId",
                table: "Bolsas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bolsas_Projetos_ProjetoId",
                table: "Bolsas",
                column: "ProjetoId",
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

            migrationBuilder.AlterColumn<int>(
                name: "ProjetoId",
                table: "Bolsas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Bolsas_Projetos_ProjetoId",
                table: "Bolsas",
                column: "ProjetoId",
                principalTable: "Projetos",
                principalColumn: "Id");
        }
    }
}
