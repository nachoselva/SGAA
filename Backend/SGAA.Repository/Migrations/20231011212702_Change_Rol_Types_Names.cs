using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGAA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Change_Rol_Types_Names : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHK_Rol_RolType",
                table: "Rol");

            migrationBuilder.UpdateData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "NormalizedName", "RolType" },
                values: new object[] { "Administrador", "ADMINISTRADOR", "Administrador" });

            migrationBuilder.UpdateData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "NormalizedName", "RolType" },
                values: new object[] { "Inquilino", "INQUILINO", "Inquilino" });

            migrationBuilder.UpdateData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "NormalizedName", "RolType" },
                values: new object[] { "Propietario", "PROPIETARIO", "Propietario" });

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Rol_RolType",
                table: "Rol",
                sql: "[RolType] IN ('Administrador', 'Inquilino', 'Propietario')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHK_Rol_RolType",
                table: "Rol");

            migrationBuilder.UpdateData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "NormalizedName", "RolType" },
                values: new object[] { "Administrator", "ADMINISTRATOR", "Administrator" });

            migrationBuilder.UpdateData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "NormalizedName", "RolType" },
                values: new object[] { "Resident", "RESIDENT", "Resident" });

            migrationBuilder.UpdateData(
                table: "Rol",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "NormalizedName", "RolType" },
                values: new object[] { "Homeowner", "HOMEOWNER", "Homeowner" });

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Rol_RolType",
                table: "Rol",
                sql: "[RolType] IN ('Administrator', 'Resident', 'Homeowner')");
        }
    }
}
