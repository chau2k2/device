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
                name: "monitors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
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
                    Name = table.Column<string>(type: "text", nullable: false),
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
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
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
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vgas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "laptops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IdProducer = table.Column<int>(type: "integer", nullable: false),
                    GiaVon = table.Column<double>(type: "double precision", nullable: false),
                    Giaban = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_laptops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_laptops_producers_IdProducer",
                        column: x => x.IdProducer,
                        principalTable: "producers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "laptopsDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cpu = table.Column<string>(type: "text", nullable: false),
                    Seri = table.Column<string>(type: "text", nullable: false),
                    IdVga = table.Column<int>(type: "integer", nullable: false),
                    IdRam = table.Column<int>(type: "integer", nullable: false),
                    HardDriver = table.Column<string>(type: "text", nullable: false),
                    IdMonitor = table.Column<int>(type: "integer", nullable: false),
                    Webcam = table.Column<string>(type: "text", nullable: false),
                    Weight = table.Column<double>(type: "double precision", nullable: false),
                    Height = table.Column<double>(type: "double precision", nullable: false),
                    Width = table.Column<double>(type: "double precision", nullable: false),
                    Length = table.Column<double>(type: "double precision", nullable: false),
                    BatteryCatttery = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", nullable: true),
                    idLaptop = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_laptopsDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_laptopsDetail_laptops_idLaptop",
                        column: x => x.idLaptop,
                        principalTable: "laptops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_laptopsDetail_monitors_IdMonitor",
                        column: x => x.IdMonitor,
                        principalTable: "monitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_laptopsDetail_ram_IdRam",
                        column: x => x.IdRam,
                        principalTable: "ram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_laptopsDetail_vgas_IdVga",
                        column: x => x.IdVga,
                        principalTable: "vgas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "khoHangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idDetail = table.Column<int>(type: "integer", nullable: false),
                    SoLuongBan = table.Column<int>(type: "integer", nullable: false),
                    SoLuongNhap = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_khoHangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_khoHangs_laptopsDetail_idDetail",
                        column: x => x.idDetail,
                        principalTable: "laptopsDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_khoHangs_idDetail",
                table: "khoHangs",
                column: "idDetail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_laptops_IdProducer",
                table: "laptops",
                column: "IdProducer");

            migrationBuilder.CreateIndex(
                name: "IX_laptopsDetail_idLaptop",
                table: "laptopsDetail",
                column: "idLaptop");

            migrationBuilder.CreateIndex(
                name: "IX_laptopsDetail_IdMonitor",
                table: "laptopsDetail",
                column: "IdMonitor");

            migrationBuilder.CreateIndex(
                name: "IX_laptopsDetail_IdRam",
                table: "laptopsDetail",
                column: "IdRam");

            migrationBuilder.CreateIndex(
                name: "IX_laptopsDetail_IdVga",
                table: "laptopsDetail",
                column: "IdVga");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "khoHangs");

            migrationBuilder.DropTable(
                name: "laptopsDetail");

            migrationBuilder.DropTable(
                name: "laptops");

            migrationBuilder.DropTable(
                name: "monitors");

            migrationBuilder.DropTable(
                name: "ram");

            migrationBuilder.DropTable(
                name: "vgas");

            migrationBuilder.DropTable(
                name: "producers");
        }
    }
}
