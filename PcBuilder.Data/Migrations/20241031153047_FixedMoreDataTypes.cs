using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PcBuilder.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedMoreDataTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("745ea995-2bea-41a0-ade6-9444a40428c1"));

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductCategory",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ImageUrl", "ManufacturerId", "OrderId", "ProductDescription", "ProductName", "ProductPrice", "StockQuantity" },
                values: new object[] { new Guid("095486a0-278b-497c-bdc8-3175d4ba7124"), new DateTime(2024, 10, 31, 0, 0, 0, 0, DateTimeKind.Local), null, 1, "Balls", 1, null, "16 Threats 4.5Ghz", "Intel Core I9", 300m, 5 });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("095486a0-278b-497c-bdc8-3175d4ba7124"));

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Manufacturer",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductCategory",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ImageUrl", "Manufacturer", "ManufacturerId", "OrderId", "ProductCategory", "ProductCategoryId", "ProductDescription", "ProductName", "ProductPrice", "StockQuantity" },
                values: new object[] { new Guid("745ea995-2bea-41a0-ade6-9444a40428c1"), new DateTime(2024, 10, 31, 0, 0, 0, 0, DateTimeKind.Local), null, null, "Balls", 0, 1, null, 0, 1, "16 Threats 4.5Ghz", "Intel Core I9", 300m, 5 });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
