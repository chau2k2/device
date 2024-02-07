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
            migrationBuilder.Sql("ALTER TABLE \"laptopsDetail\" ALTER COLUMN \"BatteryCatttery\" TYPE double precision USING \"BatteryCatttery\"::double precision;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BatteryCatttery",
                table: "laptopsDetail",
                type: "text",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
