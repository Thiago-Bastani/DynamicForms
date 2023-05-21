using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formularios.Migrations
{
    /// <inheritdoc />
    public partial class alter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campos_Tipos_TipoId",
                table: "Campos");

            migrationBuilder.AlterColumn<int>(
                name: "TipoId",
                table: "Campos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Campos_Tipos_TipoId",
                table: "Campos",
                column: "TipoId",
                principalTable: "Tipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campos_Tipos_TipoId",
                table: "Campos");

            migrationBuilder.AlterColumn<int>(
                name: "TipoId",
                table: "Campos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Campos_Tipos_TipoId",
                table: "Campos",
                column: "TipoId",
                principalTable: "Tipos",
                principalColumn: "Id");
        }
    }
}
