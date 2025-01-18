using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaAspirantes.Migrations
{
    /// <inheritdoc />
    public partial class modelRolUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RolUsuarios",
                table: "RolUsuarios");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RolUsuarios",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolUsuarios",
                table: "RolUsuarios",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RolUsuarios_IdRol",
                table: "RolUsuarios",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RolUsuarios",
                table: "RolUsuarios");

            migrationBuilder.DropIndex(
                name: "IX_RolUsuarios_IdRol",
                table: "RolUsuarios");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RolUsuarios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolUsuarios",
                table: "RolUsuarios",
                column: "IdRol");
        }
    }
}
