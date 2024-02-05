using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace device.Migrations
{
    /// <inheritdoc />
    public partial class _05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GiaVon",
                table: "khoHangs");

            migrationBuilder.DropColumn(
                name: "Giaban",
                table: "khoHangs");

            migrationBuilder.AddColumn<double>(
                name: "GiaVon",
                table: "laptops",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Giaban",
                table: "laptops",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GiaVon",
                table: "laptops");

            migrationBuilder.DropColumn(
                name: "Giaban",
                table: "laptops");

            migrationBuilder.AddColumn<double>(
                name: "GiaVon",
                table: "khoHangs",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Giaban",
                table: "khoHangs",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
