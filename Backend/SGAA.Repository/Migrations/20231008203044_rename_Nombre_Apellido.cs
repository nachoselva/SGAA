#nullable disable

namespace SGAA.Repository.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    /// <inheritdoc />
    public partial class rename_Nombre_Apellido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "Apellido");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Apellido",
                table: "Users",
                newName: "FirstName");
        }
    }
}
