using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PcBuilder.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedLastMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ca8b2d8f-0848-4e1f-8671-a7a1a2c1e3ea"));

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "AplicationUsersOrders");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ApplicationUsersProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ImageUrl", "IsDeleted", "ManufacturerId", "ProductDescription", "ProductName", "ProductPrice", "StockQuantity" },
                values: new object[] { new Guid("d16c5f7c-9a95-4afe-8c5c-daa7eb876f10"), new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Local), null, 1, "Balls", false, 1, "16 Threats 4.5Ghz", "Intel Core I9", 300m, 5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d16c5f7c-9a95-4afe-8c5c-daa7eb876f10"));

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ApplicationUsersProducts");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "AplicationUsersOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ImageUrl", "IsDeleted", "ManufacturerId", "ProductDescription", "ProductName", "ProductPrice", "StockQuantity" },
                values: new object[] { new Guid("ca8b2d8f-0848-4e1f-8671-a7a1a2c1e3ea"), new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Local), null, 1, "Balls", false, 1, "16 Threats 4.5Ghz", "Intel Core I9", 300m, 5 });
        }
    }
}
