using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace colab.Migrations
{
    /// <inheritdoc />
    public partial class RefactorEscolaridade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Escolaridade",
                table: "Bolsas",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Escolaridade",
                table: "Bolsas");
        }
    }
}
