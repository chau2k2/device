using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace device.Migrations
{
    /// <inheritdoc />
    public partial class _07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicesDetail_laptops_LaptopId",
                table: "InvoicesDetail");

            migrationBuilder.DropIndex(
                name: "IX_InvoicesDetail_LaptopId",
                table: "InvoicesDetail");

            migrationBuilder.DropColumn(
                name: "LaptopId",
                table: "InvoicesDetail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LaptopId",
                table: "InvoicesDetail",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoicesDetail_LaptopId",
                table: "InvoicesDetail",
                column: "LaptopId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicesDetail_laptops_LaptopId",
                table: "InvoicesDetail",
                column: "LaptopId",
                principalTable: "laptops",
                principalColumn: "Id");
        }
    }
}
