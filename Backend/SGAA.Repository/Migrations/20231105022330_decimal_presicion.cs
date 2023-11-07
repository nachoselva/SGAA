using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGAA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class decimal_presicion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "IngresoMensual",
                table: "Postulante",
                type: "decimal(14,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "IndiceValor",
                type: "decimal(14,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Monto",
                table: "Garantia",
                type: "decimal(14,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PuntuacionTotal",
                table: "Aplicacion",
                type: "decimal(14,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "IngresoMensual",
                table: "Postulante",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "IndiceValor",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Monto",
                table: "Garantia",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PuntuacionTotal",
                table: "Aplicacion",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,4)");
        }
    }
}
