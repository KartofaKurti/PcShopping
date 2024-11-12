using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PcBuilder.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsCategories");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("095486a0-278b-497c-bdc8-3175d4ba7124"));

            migrationBuilder.CreateTable(
                name: "ApplicationUsersProducts",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsersProducts", x => new { x.ApplicationUserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ApplicationUsersProducts_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUsersProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ImageUrl", "ManufacturerId", "OrderId", "ProductDescription", "ProductName", "ProductPrice", "StockQuantity" },
                values: new object[] { new Guid("6bfb34e2-7283-4e4a-8d80-86d0a16b9b84"), new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Local), null, 1, "Balls", 1, null, "16 Threats 4.5Ghz", "Intel Core I9", 300m, 5 });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsersProducts_ProductId",
                table: "ApplicationUsersProducts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUsersProducts");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6bfb34e2-7283-4e4a-8d80-86d0a16b9b84"));

            migrationBuilder.CreateTable(
                name: "ProductsCategories",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsCategories", x => new { x.ClientId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductsCategories_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ImageUrl", "ManufacturerId", "OrderId", "ProductDescription", "ProductName", "ProductPrice", "StockQuantity" },
                values: new object[] { new Guid("095486a0-278b-497c-bdc8-3175d4ba7124"), new DateTime(2024, 10, 31, 0, 0, 0, 0, DateTimeKind.Local), null, 1, "Balls", 1, null, "16 Threats 4.5Ghz", "Intel Core I9", 300m, 5 });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsCategories_ProductId",
                table: "ProductsCategories",
                column: "ProductId");
        }
    }
}
