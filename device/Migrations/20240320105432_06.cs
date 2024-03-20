using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace device.Migrations
{
    /// <inheritdoc />
    public partial class _06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicesDetail_laptops_LaptopId",
                table: "InvoicesDetail");

            migrationBuilder.AlterColumn<int>(
                name: "LaptopId",
                table: "InvoicesDetail",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "ProductType",
                table: "InvoicesDetail",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PrivateComputer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CostPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    SoldPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    ProducerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateComputer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivateComputer_producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "producers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrivateComputer_ProducerId",
                table: "PrivateComputer",
                column: "ProducerId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicesDetail_laptops_LaptopId",
                table: "InvoicesDetail",
                column: "LaptopId",
                principalTable: "laptops",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicesDetail_laptops_LaptopId",
                table: "InvoicesDetail");

            migrationBuilder.DropTable(
                name: "PrivateComputer");

            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "InvoicesDetail");

            migrationBuilder.AlterColumn<int>(
                name: "LaptopId",
                table: "InvoicesDetail",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicesDetail_laptops_LaptopId",
                table: "InvoicesDetail",
                column: "LaptopId",
                principalTable: "laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
