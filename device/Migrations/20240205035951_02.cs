using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace device.Migrations
{
    /// <inheritdoc />
    public partial class _02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaptopProducer_laptops_LaptopsId",
                table: "LaptopProducer");

            migrationBuilder.DropForeignKey(
                name: "FK_LaptopProducer_producers_ProducersId",
                table: "LaptopProducer");

            migrationBuilder.RenameColumn(
                name: "ProducersId",
                table: "LaptopProducer",
                newName: "ProducerId");

            migrationBuilder.RenameColumn(
                name: "LaptopsId",
                table: "LaptopProducer",
                newName: "LaptopId");

            migrationBuilder.RenameIndex(
                name: "IX_LaptopProducer_ProducersId",
                table: "LaptopProducer",
                newName: "IX_LaptopProducer_ProducerId");

            migrationBuilder.AddForeignKey(
                name: "FK_LaptopProducer_laptops_LaptopId",
                table: "LaptopProducer",
                column: "LaptopId",
                principalTable: "laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LaptopProducer_producers_ProducerId",
                table: "LaptopProducer",
                column: "ProducerId",
                principalTable: "producers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaptopProducer_laptops_LaptopId",
                table: "LaptopProducer");

            migrationBuilder.DropForeignKey(
                name: "FK_LaptopProducer_producers_ProducerId",
                table: "LaptopProducer");

            migrationBuilder.RenameColumn(
                name: "ProducerId",
                table: "LaptopProducer",
                newName: "ProducersId");

            migrationBuilder.RenameColumn(
                name: "LaptopId",
                table: "LaptopProducer",
                newName: "LaptopsId");

            migrationBuilder.RenameIndex(
                name: "IX_LaptopProducer_ProducerId",
                table: "LaptopProducer",
                newName: "IX_LaptopProducer_ProducersId");

            migrationBuilder.AddForeignKey(
                name: "FK_LaptopProducer_laptops_LaptopsId",
                table: "LaptopProducer",
                column: "LaptopsId",
                principalTable: "laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LaptopProducer_producers_ProducersId",
                table: "LaptopProducer",
                column: "ProducersId",
                principalTable: "producers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
