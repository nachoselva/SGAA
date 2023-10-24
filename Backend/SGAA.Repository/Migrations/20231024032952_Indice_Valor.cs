using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SGAA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Indice_Valor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndiceValor_Indice_IndiceId",
                table: "IndiceValor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Indice",
                table: "Indice");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Indice_Nombre",
                table: "Indice");

            migrationBuilder.RenameTable(
                name: "Indice",
                newName: "Indices");

            migrationBuilder.AddColumn<int>(
                name: "PuntuacionCrediticia",
                table: "Postulante",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PuntuacionPenal",
                table: "Postulante",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Indices",
                table: "Indices",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Indices",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { 1, "ICL" });

            migrationBuilder.InsertData(
                table: "IndiceValor",
                columns: new[] { "Id", "FechaDesde", "IndiceId", "Valor" },
                values: new object[,]
                {
                    { 1, new DateOnly(2023, 1, 1), 1, 3.21m },
                    { 2, new DateOnly(2023, 2, 1), 1, 3.29m },
                    { 3, new DateOnly(2023, 3, 1), 1, 3.45m },
                    { 4, new DateOnly(2023, 4, 1), 1, 3.64m },
                    { 5, new DateOnly(2023, 5, 1), 1, 3.86m },
                    { 6, new DateOnly(2023, 6, 1), 1, 4.18m },
                    { 7, new DateOnly(2023, 7, 1), 1, 4.55m },
                    { 8, new DateOnly(2023, 8, 1), 1, 4.92m },
                    { 9, new DateOnly(2023, 9, 1), 1, 5.26m },
                    { 10, new DateOnly(2023, 10, 1), 1, 5.62m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Indices_Nombre",
                table: "Indices",
                column: "Nombre",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Indices_Nombre",
                table: "Indices",
                sql: "[Nombre] IN ('ICL')");

            migrationBuilder.AddForeignKey(
                name: "FK_IndiceValor_Indices_IndiceId",
                table: "IndiceValor",
                column: "IndiceId",
                principalTable: "Indices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndiceValor_Indices_IndiceId",
                table: "IndiceValor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Indices",
                table: "Indices");

            migrationBuilder.DropIndex(
                name: "IX_Indices_Nombre",
                table: "Indices");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Indices_Nombre",
                table: "Indices");

            migrationBuilder.DeleteData(
                table: "IndiceValor",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IndiceValor",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "IndiceValor",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "IndiceValor",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "IndiceValor",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "IndiceValor",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "IndiceValor",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "IndiceValor",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "IndiceValor",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "IndiceValor",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Indices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "PuntuacionCrediticia",
                table: "Postulante");

            migrationBuilder.DropColumn(
                name: "PuntuacionPenal",
                table: "Postulante");

            migrationBuilder.RenameTable(
                name: "Indices",
                newName: "Indice");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Indice",
                table: "Indice",
                column: "Id");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Indice_Nombre",
                table: "Indice",
                sql: "[Nombre] IN ('ICL')");

            migrationBuilder.AddForeignKey(
                name: "FK_IndiceValor_Indice_IndiceId",
                table: "IndiceValor",
                column: "IndiceId",
                principalTable: "Indice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
