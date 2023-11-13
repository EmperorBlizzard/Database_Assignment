using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_Assignment_API.Migrations
{
    /// <inheritdoc />
    public partial class ChangedproductPricetoStockPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "StockPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StockPrice",
                table: "Products",
                newName: "Price");
        }
    }
}
