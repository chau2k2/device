using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace device.Migrations
{
    /// <inheritdoc />
    public partial class _03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "vgas");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ram");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "monitors");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "vgas",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "ram",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "monitors",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "vgas");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ram");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "monitors");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "vgas",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ram",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "monitors",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
