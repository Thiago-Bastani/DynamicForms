using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formularios.Migrations
{
    /// <inheritdoc />
    public partial class campoNomeIsUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Campos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Campos_Nome",
                table: "Campos",
                column: "Nome",
                unique: true,
                filter: "[Nome] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Campos_Nome",
                table: "Campos");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Campos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
