using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace colab.Migrations
{
    /// <inheritdoc />
    public partial class AllowPessoaWithoutBolsa3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bolsas_Pessoas_PessoaId",
                table: "Bolsas");

            migrationBuilder.AddForeignKey(
                name: "FK_Bolsas_Pessoas_PessoaId",
                table: "Bolsas",
                column: "PessoaId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bolsas_Pessoas_PessoaId",
                table: "Bolsas");

            migrationBuilder.AddForeignKey(
                name: "FK_Bolsas_Pessoas_PessoaId",
                table: "Bolsas",
                column: "PessoaId",
                principalTable: "Pessoas",
                principalColumn: "Id");
        }
    }
}
