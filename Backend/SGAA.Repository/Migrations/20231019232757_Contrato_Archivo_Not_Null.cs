using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGAA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Contrato_Archivo_Not_Null : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DomicilioCompleto",
                table: "Firma",
                newName: "Domicilio");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Archivo",
                table: "Contrato",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Domicilio",
                table: "Firma",
                newName: "DomicilioCompleto");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Archivo",
                table: "Contrato",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");
        }
    }
}
