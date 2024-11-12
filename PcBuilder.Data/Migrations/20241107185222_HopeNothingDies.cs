using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PcBuilder.Data.Migrations
{
    /// <inheritdoc />
    public partial class HopeNothingDies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f9045b9d-81f5-4b68-a8da-a516f444f24e"));

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Orders",
                newName: "Address");

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ImageUrl", "IsDeleted", "ManufacturerId", "ProductDescription", "ProductName", "ProductPrice", "StockQuantity" },
                values: new object[] { new Guid("8f271da5-4f64-4278-8414-c76811e15e4b"), new DateTime(2024, 11, 7, 0, 0, 0, 0, DateTimeKind.Local), null, 1, "Balls", false, 1, "16 Threats 4.5Ghz", "Intel Core I9", 300m, 5 });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8f271da5-4f64-4278-8414-c76811e15e4b"));

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Orders",
                newName: "Adress");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ImageUrl", "IsDeleted", "ManufacturerId", "OrderId", "ProductDescription", "ProductName", "ProductPrice", "StockQuantity" },
                values: new object[] { new Guid("f9045b9d-81f5-4b68-a8da-a516f444f24e"), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Local), null, 1, "Balls", false, 1, null, "16 Threats 4.5Ghz", "Intel Core I9", 300m, 5 });

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderId",
                table: "Products",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
