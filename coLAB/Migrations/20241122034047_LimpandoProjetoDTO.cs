using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace colabAPI.Migrations
{
    /// <inheritdoc />
    public partial class LimpandoProjetoDTO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projetos_Orientadores_OrientadorId",
                table: "Projetos");

            migrationBuilder.AlterColumn<int>(
                name: "OrientadorId",
                table: "Projetos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Projetos_Orientadores_OrientadorId",
                table: "Projetos",
                column: "OrientadorId",
                principalTable: "Orientadores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projetos_Orientadores_OrientadorId",
                table: "Projetos");

            migrationBuilder.AlterColumn<int>(
                name: "OrientadorId",
                table: "Projetos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projetos_Orientadores_OrientadorId",
                table: "Projetos",
                column: "OrientadorId",
                principalTable: "Orientadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
