using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaAspirantes.Migrations
{
    /// <inheritdoc />
    public partial class RolRolOpcion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RolRolOpciones",
                table: "RolRolOpciones");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolRolOpciones",
                table: "RolRolOpciones",
                columns: new[] { "IdRol", "IdOption" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RolRolOpciones",
                table: "RolRolOpciones");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolRolOpciones",
                table: "RolRolOpciones",
                column: "IdRol");
        }
    }
}
