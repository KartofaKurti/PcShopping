using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PcBuilder.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedImageUrlValidation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6bfb34e2-7283-4e4a-8d80-86d0a16b9b84"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ImageUrl", "ManufacturerId", "OrderId", "ProductDescription", "ProductName", "ProductPrice", "StockQuantity" },
                values: new object[] { new Guid("2bf68fd5-077a-4677-9499-008bcf3a8f79"), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Local), null, 1, "Balls", 1, null, "16 Threats 4.5Ghz", "Intel Core I9", 300m, 5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2bf68fd5-077a-4677-9499-008bcf3a8f79"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ImageUrl", "ManufacturerId", "OrderId", "ProductDescription", "ProductName", "ProductPrice", "StockQuantity" },
                values: new object[] { new Guid("6bfb34e2-7283-4e4a-8d80-86d0a16b9b84"), new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Local), null, 1, "Balls", 1, null, "16 Threats 4.5Ghz", "Intel Core I9", 300m, 5 });
        }
    }
}
