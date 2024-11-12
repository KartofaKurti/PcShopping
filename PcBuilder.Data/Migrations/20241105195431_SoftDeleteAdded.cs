using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PcBuilder.Data.Migrations
{
    /// <inheritdoc />
    public partial class SoftDeleteAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2bf68fd5-077a-4677-9499-008bcf3a8f79"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ImageUrl", "IsDeleted", "ManufacturerId", "OrderId", "ProductDescription", "ProductName", "ProductPrice", "StockQuantity" },
                values: new object[] { new Guid("f9045b9d-81f5-4b68-a8da-a516f444f24e"), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Local), null, 1, "Balls", false, 1, null, "16 Threats 4.5Ghz", "Intel Core I9", 300m, 5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f9045b9d-81f5-4b68-a8da-a516f444f24e"));

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ImageUrl", "ManufacturerId", "OrderId", "ProductDescription", "ProductName", "ProductPrice", "StockQuantity" },
                values: new object[] { new Guid("2bf68fd5-077a-4677-9499-008bcf3a8f79"), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Local), null, 1, "Balls", 1, null, "16 Threats 4.5Ghz", "Intel Core I9", 300m, 5 });
        }
    }
}
