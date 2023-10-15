using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGAA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Aplicacion_Remove_Restrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Garantia_Aplicacion_AplicacionId",
                table: "Garantia");

            migrationBuilder.DropForeignKey(
                name: "FK_Postulante_Aplicacion_AplicacionId",
                table: "Postulante");

            migrationBuilder.AddForeignKey(
                name: "FK_Garantia_Aplicacion_AplicacionId",
                table: "Garantia",
                column: "AplicacionId",
                principalTable: "Aplicacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Postulante_Aplicacion_AplicacionId",
                table: "Postulante",
                column: "AplicacionId",
                principalTable: "Aplicacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Garantia_Aplicacion_AplicacionId",
                table: "Garantia");

            migrationBuilder.DropForeignKey(
                name: "FK_Postulante_Aplicacion_AplicacionId",
                table: "Postulante");

            migrationBuilder.AddForeignKey(
                name: "FK_Garantia_Aplicacion_AplicacionId",
                table: "Garantia",
                column: "AplicacionId",
                principalTable: "Aplicacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Postulante_Aplicacion_AplicacionId",
                table: "Postulante",
                column: "AplicacionId",
                principalTable: "Aplicacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
