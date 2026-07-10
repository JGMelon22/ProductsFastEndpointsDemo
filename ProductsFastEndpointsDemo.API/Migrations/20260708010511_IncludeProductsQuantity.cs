using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsFastEndpointsDemo.Migrations
{
    /// <inheritdoc />
    public partial class IncludeProductsQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "products",
                type: "INT",
                nullable: false,
                defaultValue: 0
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "quantity", table: "products");
        }
    }
}
