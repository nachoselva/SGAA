using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGAA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class rename_auth_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aplicacion_Users_InquilinoUsuarioId",
                table: "Aplicacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Firma_Users_ContratoId",
                table: "Firma");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaims_Roles_RoleId",
                table: "RoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_Unidad_Users_PropiedadId",
                table: "Unidad");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_Users_UserId",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_Users_UserId",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_Users_UserId",
                table: "UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTokens",
                table: "UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLogins",
                table: "UserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClaims",
                table: "UserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Roles_RolType",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleClaims",
                table: "RoleClaims");

            migrationBuilder.RenameTable(
                name: "UserTokens",
                newName: "UsuarioToken");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "UsuarioRol");

            migrationBuilder.RenameTable(
                name: "UserLogins",
                newName: "UsuarioLogin");

            migrationBuilder.RenameTable(
                name: "UserClaims",
                newName: "UsuarioPermiso");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Rol");

            migrationBuilder.RenameTable(
                name: "RoleClaims",
                newName: "RolPermiso");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                table: "UsuarioRol",
                newName: "IX_UsuarioRol_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserLogins_UserId",
                table: "UsuarioLogin",
                newName: "IX_UsuarioLogin_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserClaims_UserId",
                table: "UsuarioPermiso",
                newName: "IX_UsuarioPermiso_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_RolType",
                table: "Rol",
                newName: "IX_Rol_RolType");

            migrationBuilder.RenameIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RolPermiso",
                newName: "IX_RolPermiso_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioToken",
                table: "UsuarioToken",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioRol",
                table: "UsuarioRol",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioLogin",
                table: "UsuarioLogin",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioPermiso",
                table: "UsuarioPermiso",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rol",
                table: "Rol",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolPermiso",
                table: "RolPermiso",
                column: "Id");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Rol_RolType",
                table: "Rol",
                sql: "[RolType] IN ('Administrator', 'Resident', 'Homeowner')");

            migrationBuilder.AddForeignKey(
                name: "FK_Aplicacion_Usuario_InquilinoUsuarioId",
                table: "Aplicacion",
                column: "InquilinoUsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Firma_Usuario_ContratoId",
                table: "Firma",
                column: "ContratoId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolPermiso_Rol_RoleId",
                table: "RolPermiso",
                column: "RoleId",
                principalTable: "Rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Unidad_Usuario_PropiedadId",
                table: "Unidad",
                column: "PropiedadId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioLogin_Usuario_UserId",
                table: "UsuarioLogin",
                column: "UserId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioPermiso_Usuario_UserId",
                table: "UsuarioPermiso",
                column: "UserId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Rol_RoleId",
                table: "UsuarioRol",
                column: "RoleId",
                principalTable: "Rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Usuario_UserId",
                table: "UsuarioRol",
                column: "UserId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioToken_Usuario_UserId",
                table: "UsuarioToken",
                column: "UserId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aplicacion_Usuario_InquilinoUsuarioId",
                table: "Aplicacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Firma_Usuario_ContratoId",
                table: "Firma");

            migrationBuilder.DropForeignKey(
                name: "FK_RolPermiso_Rol_RoleId",
                table: "RolPermiso");

            migrationBuilder.DropForeignKey(
                name: "FK_Unidad_Usuario_PropiedadId",
                table: "Unidad");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioLogin_Usuario_UserId",
                table: "UsuarioLogin");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioPermiso_Usuario_UserId",
                table: "UsuarioPermiso");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Rol_RoleId",
                table: "UsuarioRol");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Usuario_UserId",
                table: "UsuarioRol");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioToken_Usuario_UserId",
                table: "UsuarioToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioToken",
                table: "UsuarioToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioRol",
                table: "UsuarioRol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioPermiso",
                table: "UsuarioPermiso");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioLogin",
                table: "UsuarioLogin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolPermiso",
                table: "RolPermiso");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rol",
                table: "Rol");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Rol_RolType",
                table: "Rol");

            migrationBuilder.RenameTable(
                name: "UsuarioToken",
                newName: "UserTokens");

            migrationBuilder.RenameTable(
                name: "UsuarioRol",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "UsuarioPermiso",
                newName: "UserClaims");

            migrationBuilder.RenameTable(
                name: "UsuarioLogin",
                newName: "UserLogins");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "RolPermiso",
                newName: "RoleClaims");

            migrationBuilder.RenameTable(
                name: "Rol",
                newName: "Roles");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioRol_RoleId",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioPermiso_UserId",
                table: "UserClaims",
                newName: "IX_UserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioLogin_UserId",
                table: "UserLogins",
                newName: "IX_UserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RolPermiso_RoleId",
                table: "RoleClaims",
                newName: "IX_RoleClaims_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Rol_RolType",
                table: "Roles",
                newName: "IX_Roles_RolType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTokens",
                table: "UserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserClaims",
                table: "UserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLogins",
                table: "UserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleClaims",
                table: "RoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Roles_RolType",
                table: "Roles",
                sql: "[RolType] IN ('Administrator', 'Resident', 'Homeowner')");

            migrationBuilder.AddForeignKey(
                name: "FK_Aplicacion_Users_InquilinoUsuarioId",
                table: "Aplicacion",
                column: "InquilinoUsuarioId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Firma_Users_ContratoId",
                table: "Firma",
                column: "ContratoId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaims_Roles_RoleId",
                table: "RoleClaims",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Unidad_Users_PropiedadId",
                table: "Unidad",
                column: "PropiedadId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_Users_UserId",
                table: "UserClaims",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_Users_UserId",
                table: "UserLogins",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_Users_UserId",
                table: "UserTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
