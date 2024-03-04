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
                name: "AutoUpdatePrice",
                table: "InvoicesDetail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AutoUpdatePrice",
                table: "InvoicesDetail",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
