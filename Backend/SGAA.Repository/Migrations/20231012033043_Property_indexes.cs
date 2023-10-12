using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGAA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Property_indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Unidad_PropiedadId",
                table: "Unidad");

            migrationBuilder.CreateIndex(
                name: "IX_Unidad_PropiedadId_Piso_Departamento",
                table: "Unidad",
                columns: new[] { "PropiedadId", "Piso", "Departamento" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Propiedades_Calle_Altura",
                table: "Propiedades",
                columns: new[] { "Calle", "Altura" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Unidad_PropiedadId_Piso_Departamento",
                table: "Unidad");

            migrationBuilder.DropIndex(
                name: "IX_Propiedades_Calle_Altura",
                table: "Propiedades");

            migrationBuilder.CreateIndex(
                name: "IX_Unidad_PropiedadId",
                table: "Unidad",
                column: "PropiedadId");
        }
    }
}
