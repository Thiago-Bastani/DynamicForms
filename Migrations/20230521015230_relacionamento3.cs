using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formularios.Migrations
{
    /// <inheritdoc />
    public partial class relacionamento3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campos_Formularios_FormularioId",
                table: "Campos");

            migrationBuilder.DropIndex(
                name: "IX_Campos_FormularioId",
                table: "Campos");

            migrationBuilder.DropColumn(
                name: "FormularioId",
                table: "Campos");

            migrationBuilder.CreateTable(
                name: "CampoFormulario",
                columns: table => new
                {
                    CamposId = table.Column<int>(type: "int", nullable: false),
                    FormulariosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampoFormulario", x => new { x.CamposId, x.FormulariosId });
                    table.ForeignKey(
                        name: "FK_CampoFormulario_Campos_CamposId",
                        column: x => x.CamposId,
                        principalTable: "Campos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampoFormulario_Formularios_FormulariosId",
                        column: x => x.FormulariosId,
                        principalTable: "Formularios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampoFormulario_FormulariosId",
                table: "CampoFormulario",
                column: "FormulariosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampoFormulario");

            migrationBuilder.AddColumn<int>(
                name: "FormularioId",
                table: "Campos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Campos_FormularioId",
                table: "Campos",
                column: "FormularioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campos_Formularios_FormularioId",
                table: "Campos",
                column: "FormularioId",
                principalTable: "Formularios",
                principalColumn: "Id");
        }
    }
}
