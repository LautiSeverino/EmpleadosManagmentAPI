using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorEmpleados.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class NroDocumento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NroDocumento",
                table: "Empleados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NroDocumento",
                table: "Empleados");
        }
    }
}
