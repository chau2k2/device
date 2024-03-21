using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace device.Migrations
{
    /// <inheritdoc />
    public partial class _11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_storages_laptops_LaptopId",
                table: "storages");

            migrationBuilder.DropIndex(
                name: "IX_storages_LaptopId",
                table: "storages");

            migrationBuilder.DropColumn(
                name: "inventory",
                table: "laptops");

            migrationBuilder.RenameColumn(
                name: "LaptopId",
                table: "storages",
                newName: "inventory");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "storages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EProductType",
                table: "storages",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "storages");

            migrationBuilder.DropColumn(
                name: "EProductType",
                table: "storages");

            migrationBuilder.RenameColumn(
                name: "inventory",
                table: "storages",
                newName: "LaptopId");

            migrationBuilder.AddColumn<int>(
                name: "inventory",
                table: "laptops",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_storages_LaptopId",
                table: "storages",
                column: "LaptopId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_storages_laptops_LaptopId",
                table: "storages",
                column: "LaptopId",
                principalTable: "laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
