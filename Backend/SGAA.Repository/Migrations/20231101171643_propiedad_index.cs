using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGAA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class propiedad_index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Propiedad_Calle_Altura",
                table: "Propiedad");

            migrationBuilder.DropIndex(
                name: "IX_Propiedad_CiudadId",
                table: "Propiedad");

            migrationBuilder.CreateIndex(
                name: "IX_Propiedad_CiudadId_Calle_Altura",
                table: "Propiedad",
                columns: new[] { "CiudadId", "Calle", "Altura" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Propiedad_CiudadId_Calle_Altura",
                table: "Propiedad");

            migrationBuilder.CreateIndex(
                name: "IX_Propiedad_Calle_Altura",
                table: "Propiedad",
                columns: new[] { "Calle", "Altura" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Propiedad_CiudadId",
                table: "Propiedad",
                column: "CiudadId");
        }
    }
}
