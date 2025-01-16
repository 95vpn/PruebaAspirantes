using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaAspirantes.Migrations
{
    /// <inheritdoc />
    public partial class IdMuchosAMuchosRolRolOpcion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RolRolOpciones",
                table: "RolRolOpciones");

            migrationBuilder.AlterColumn<int>(
                name: "IdOption",
                table: "RolRolOpciones",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "IdRol",
                table: "RolRolOpciones",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RolRolOpciones",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolRolOpciones",
                table: "RolRolOpciones",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RolRolOpciones_IdRol",
                table: "RolRolOpciones",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RolRolOpciones",
                table: "RolRolOpciones");

            migrationBuilder.DropIndex(
                name: "IX_RolRolOpciones_IdRol",
                table: "RolRolOpciones");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RolRolOpciones");

            migrationBuilder.AlterColumn<int>(
                name: "IdRol",
                table: "RolRolOpciones",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "IdOption",
                table: "RolRolOpciones",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolRolOpciones",
                table: "RolRolOpciones",
                columns: new[] { "IdRol", "IdOption" });
        }
    }
}
