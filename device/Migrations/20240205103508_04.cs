using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace device.Migrations
{
    /// <inheritdoc />
    public partial class _04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_laptopsDetail_khoHangs_KhoHangId",
                table: "laptopsDetail");

            migrationBuilder.DropIndex(
                name: "IX_laptopsDetail_KhoHangId",
                table: "laptopsDetail");

            migrationBuilder.DropColumn(
                name: "IdKhoHang",
                table: "laptopsDetail");

            migrationBuilder.DropColumn(
                name: "KhoHangId",
                table: "laptopsDetail");

            migrationBuilder.AddColumn<int>(
                name: "idDetail",
                table: "khoHangs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_khoHangs_idDetail",
                table: "khoHangs",
                column: "idDetail",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_khoHangs_laptopsDetail_idDetail",
                table: "khoHangs",
                column: "idDetail",
                principalTable: "laptopsDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_khoHangs_laptopsDetail_idDetail",
                table: "khoHangs");

            migrationBuilder.DropIndex(
                name: "IX_khoHangs_idDetail",
                table: "khoHangs");

            migrationBuilder.DropColumn(
                name: "idDetail",
                table: "khoHangs");

            migrationBuilder.AddColumn<int>(
                name: "IdKhoHang",
                table: "laptopsDetail",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KhoHangId",
                table: "laptopsDetail",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_laptopsDetail_KhoHangId",
                table: "laptopsDetail",
                column: "KhoHangId");

            migrationBuilder.AddForeignKey(
                name: "FK_laptopsDetail_khoHangs_KhoHangId",
                table: "laptopsDetail",
                column: "KhoHangId",
                principalTable: "khoHangs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
