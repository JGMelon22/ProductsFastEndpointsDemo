using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsFastEndpointsDemo.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedUpdateProductNameIndexToPatternOps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_product_name",
                table: "products");

            migrationBuilder.CreateIndex(
                name: "idx_product_name",
                table: "products",
                column: "name")
                .Annotation("Npgsql:IndexOperators", new[] { "text_pattern_ops" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_product_name",
                table: "products");

            migrationBuilder.CreateIndex(
                name: "idx_product_name",
                table: "products",
                column: "name")
                .Annotation("Npgsql:IndexOperators", new[] { null, "text_pattern_ops" });
        }
    }
}
