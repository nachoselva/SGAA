using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGAA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Usuario_firma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Firma_Usuario_ContratoId",
                table: "Firma");

            migrationBuilder.CreateIndex(
                name: "IX_Firma_UsuarioId",
                table: "Firma",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Firma_Usuario_UsuarioId",
                table: "Firma",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Firma_Usuario_UsuarioId",
                table: "Firma");

            migrationBuilder.DropIndex(
                name: "IX_Firma_UsuarioId",
                table: "Firma");

            migrationBuilder.AddForeignKey(
                name: "FK_Firma_Usuario_ContratoId",
                table: "Firma",
                column: "ContratoId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
