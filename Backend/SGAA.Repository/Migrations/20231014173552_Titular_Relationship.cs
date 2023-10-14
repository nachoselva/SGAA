using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGAA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Titular_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Titular_Unidad_UnidadId",
                table: "Titular");

            migrationBuilder.AddForeignKey(
                name: "FK_Titular_Unidad_UnidadId",
                table: "Titular",
                column: "UnidadId",
                principalTable: "Unidad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Titular_Unidad_UnidadId",
                table: "Titular");

            migrationBuilder.AddForeignKey(
                name: "FK_Titular_Unidad_UnidadId",
                table: "Titular",
                column: "UnidadId",
                principalTable: "Unidad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
