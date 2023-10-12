using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGAA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Varbinary_Max : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Archivo",
                table: "UnidadImagen",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary");

            migrationBuilder.AlterColumn<byte[]>(
                name: "TituloPropiedadArchivo",
                table: "Unidad",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary");

            migrationBuilder.AlterColumn<byte[]>(
                name: "FrenteIdentificacionArchivo",
                table: "Titular",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary");

            migrationBuilder.AlterColumn<byte[]>(
                name: "DorsoIdentificacionArchivo",
                table: "Titular",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ReciboDeSueldoArchivo",
                table: "Postulante",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary");

            migrationBuilder.AlterColumn<byte[]>(
                name: "FrenteIdentificacionArchivo",
                table: "Postulante",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary");

            migrationBuilder.AlterColumn<byte[]>(
                name: "DorsoIdentificacionArchivo",
                table: "Postulante",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Archivo",
                table: "Garantia",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Archivo",
                table: "UnidadImagen",
                type: "varbinary",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "TituloPropiedadArchivo",
                table: "Unidad",
                type: "varbinary",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "FrenteIdentificacionArchivo",
                table: "Titular",
                type: "varbinary",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "DorsoIdentificacionArchivo",
                table: "Titular",
                type: "varbinary",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ReciboDeSueldoArchivo",
                table: "Postulante",
                type: "varbinary",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "FrenteIdentificacionArchivo",
                table: "Postulante",
                type: "varbinary",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "DorsoIdentificacionArchivo",
                table: "Postulante",
                type: "varbinary",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Archivo",
                table: "Garantia",
                type: "varbinary",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");
        }
    }
}
