using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGAA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class remove_tipo_identificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHK_Firma_TipoIdentificacion",
                table: "Firma");

            migrationBuilder.DropColumn(
                name: "TipoIdentificacion",
                table: "Titular");

            migrationBuilder.DropColumn(
                name: "TipoIdentificacion",
                table: "Postulante");

            migrationBuilder.DropColumn(
                name: "TipoIdentificacion",
                table: "Firma");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoIdentificacion",
                table: "Titular",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TipoIdentificacion",
                table: "Postulante",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TipoIdentificacion",
                table: "Firma",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Firma_TipoIdentificacion",
                table: "Firma",
                sql: "[TipoIdentificacion] IN ('Dni')");
        }
    }
}
