using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGAA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Multiple_Contrato_Postulacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postulacion_Contrato_ContratoId",
                table: "Postulacion");

            migrationBuilder.DropIndex(
                name: "IX_Postulacion_ContratoId",
                table: "Postulacion");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Contrato_Status",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "ContratoId",
                table: "Postulacion");

            migrationBuilder.AddColumn<int>(
                name: "PostulacionId",
                table: "Contrato",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_PostulacionId",
                table: "Contrato",
                column: "PostulacionId");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Contrato_Status",
                table: "Contrato",
                sql: "[Status] IN ('FirmaPendiente', 'Ejecutado', 'Renovado', 'Cancelado')");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Postulacion_PostulacionId",
                table: "Contrato",
                column: "PostulacionId",
                principalTable: "Postulacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Postulacion_PostulacionId",
                table: "Contrato");

            migrationBuilder.DropIndex(
                name: "IX_Contrato_PostulacionId",
                table: "Contrato");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Contrato_Status",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "PostulacionId",
                table: "Contrato");

            migrationBuilder.AddColumn<int>(
                name: "ContratoId",
                table: "Postulacion",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Postulacion_ContratoId",
                table: "Postulacion",
                column: "ContratoId",
                unique: true,
                filter: "[ContratoId] IS NOT NULL");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Contrato_Status",
                table: "Contrato",
                sql: "[Status] IN ('FirmaPendiente', 'Ejecutado')");

            migrationBuilder.AddForeignKey(
                name: "FK_Postulacion_Contrato_ContratoId",
                table: "Postulacion",
                column: "ContratoId",
                principalTable: "Contrato",
                principalColumn: "Id");
        }
    }
}
