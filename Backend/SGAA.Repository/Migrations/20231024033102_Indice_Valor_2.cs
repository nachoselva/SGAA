using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGAA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Indice_Valor_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndiceValor_Indices_IndiceId",
                table: "IndiceValor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Indices",
                table: "Indices");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Indices_Nombre",
                table: "Indices");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_Indices",
                table: "Indices",
                column: "Id");

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
    }
}
