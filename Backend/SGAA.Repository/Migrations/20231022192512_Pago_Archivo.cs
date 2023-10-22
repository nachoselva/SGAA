using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGAA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Pago_Archivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHK_Pago_Status",
                table: "Pago");

            migrationBuilder.AddColumn<byte[]>(
                name: "Archivo",
                table: "Pago",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Pago_Status",
                table: "Pago",
                sql: "[Status] IN ('Pendiente', 'Abonado', 'Aprobado')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHK_Pago_Status",
                table: "Pago");

            migrationBuilder.DropColumn(
                name: "Archivo",
                table: "Pago");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Pago_Status",
                table: "Pago",
                sql: "[Status] IN ('Pendiente', 'Abonado')");
        }
    }
}
