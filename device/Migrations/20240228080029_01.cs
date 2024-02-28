using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace device.Migrations
{
    /// <inheritdoc />
    public partial class _01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceNumber = table.Column<string>(type: "text", nullable: true),
                    DateInvoice = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TotalQuantity = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "monitors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monitors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "producers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ram",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ram", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vgas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vgas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoicesDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdLaptop = table.Column<int>(type: "integer", nullable: false),
                    IdInvoice = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    invoicesId = table.Column<int>(type: "integer", nullable: false),
                    LaptopId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicesDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoicesDetail_invoices_invoicesId",
                        column: x => x.invoicesId,
                        principalTable: "invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "laptops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IdProducer = table.Column<int>(type: "integer", nullable: false),
                    CostPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    SoldPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    producerId = table.Column<int>(type: "integer", nullable: true),
                    LaptopDetail = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_laptops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_laptops_producers_producerId",
                        column: x => x.producerId,
                        principalTable: "producers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "laptopsDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cpu = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Seri = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IdVga = table.Column<int>(type: "integer", nullable: false),
                    IdRam = table.Column<int>(type: "integer", nullable: false),
                    HardDriver = table.Column<string>(type: "text", nullable: false),
                    IdMonitor = table.Column<int>(type: "integer", nullable: false),
                    Webcam = table.Column<string>(type: "text", nullable: false),
                    Weight = table.Column<decimal>(type: "numeric", nullable: false),
                    Height = table.Column<decimal>(type: "numeric", nullable: false),
                    Width = table.Column<decimal>(type: "numeric", nullable: false),
                    Length = table.Column<decimal>(type: "numeric", nullable: false),
                    BatteryCapacity = table.Column<decimal>(type: "numeric", nullable: false),
                    idLaptop = table.Column<int>(type: "integer", nullable: false),
                    RamsId = table.Column<int>(type: "integer", nullable: false),
                    VgaId = table.Column<int>(type: "integer", nullable: false),
                    MonitorId = table.Column<int>(type: "integer", nullable: false),
                    Laptop = table.Column<int>(type: "integer", nullable: false),
                    Storage = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_laptopsDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_laptopsDetail_laptops_Laptop",
                        column: x => x.Laptop,
                        principalTable: "laptops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_laptopsDetail_monitors_MonitorId",
                        column: x => x.MonitorId,
                        principalTable: "monitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_laptopsDetail_ram_RamsId",
                        column: x => x.RamsId,
                        principalTable: "ram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_laptopsDetail_vgas_VgaId",
                        column: x => x.VgaId,
                        principalTable: "vgas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "storages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idDetail = table.Column<int>(type: "integer", nullable: false),
                    SoldNumber = table.Column<int>(type: "integer", nullable: false),
                    ImportNumber = table.Column<int>(type: "integer", nullable: false),
                    LaptopDetail = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_storages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_storages_laptopsDetail_LaptopDetail",
                        column: x => x.LaptopDetail,
                        principalTable: "laptopsDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoicesDetail_invoicesId",
                table: "InvoicesDetail",
                column: "invoicesId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicesDetail_LaptopId",
                table: "InvoicesDetail",
                column: "LaptopId");

            migrationBuilder.CreateIndex(
                name: "IX_laptops_LaptopDetail",
                table: "laptops",
                column: "LaptopDetail");

            migrationBuilder.CreateIndex(
                name: "IX_laptops_producerId",
                table: "laptops",
                column: "producerId");

            migrationBuilder.CreateIndex(
                name: "IX_laptopsDetail_Laptop",
                table: "laptopsDetail",
                column: "Laptop");

            migrationBuilder.CreateIndex(
                name: "IX_laptopsDetail_MonitorId",
                table: "laptopsDetail",
                column: "MonitorId");

            migrationBuilder.CreateIndex(
                name: "IX_laptopsDetail_RamsId",
                table: "laptopsDetail",
                column: "RamsId");

            migrationBuilder.CreateIndex(
                name: "IX_laptopsDetail_Storage",
                table: "laptopsDetail",
                column: "Storage");

            migrationBuilder.CreateIndex(
                name: "IX_laptopsDetail_VgaId",
                table: "laptopsDetail",
                column: "VgaId");

            migrationBuilder.CreateIndex(
                name: "IX_storages_LaptopDetail",
                table: "storages",
                column: "LaptopDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicesDetail_laptops_LaptopId",
                table: "InvoicesDetail",
                column: "LaptopId",
                principalTable: "laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_laptops_laptopsDetail_LaptopDetail",
                table: "laptops",
                column: "LaptopDetail",
                principalTable: "laptopsDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_laptopsDetail_storages_Storage",
                table: "laptopsDetail",
                column: "Storage",
                principalTable: "storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_laptopsDetail_laptops_Laptop",
                table: "laptopsDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_storages_laptopsDetail_LaptopDetail",
                table: "storages");

            migrationBuilder.DropTable(
                name: "InvoicesDetail");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropTable(
                name: "laptops");

            migrationBuilder.DropTable(
                name: "producers");

            migrationBuilder.DropTable(
                name: "laptopsDetail");

            migrationBuilder.DropTable(
                name: "monitors");

            migrationBuilder.DropTable(
                name: "ram");

            migrationBuilder.DropTable(
                name: "storages");

            migrationBuilder.DropTable(
                name: "vgas");
        }
    }
}
