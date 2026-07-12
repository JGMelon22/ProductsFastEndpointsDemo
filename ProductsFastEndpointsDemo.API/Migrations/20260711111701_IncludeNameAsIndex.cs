using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsFastEndpointsDemo.Migrations
{
    /// <inheritdoc />
    public partial class IncludeNameAsIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "idx_product_name",
                table: "products",
                column: "name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_product_name",
                table: "products");
        }
    }
}
