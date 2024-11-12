using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PcBuilder.Data.Migrations
{
    /// <inheritdoc />
    public partial class MoreTests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_ApplicationUserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ApplicationUserId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("36c35b22-0d1c-4560-bde6-e7ab95c2a5c5"));

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ImageUrl", "IsDeleted", "ManufacturerId", "ProductDescription", "ProductName", "ProductPrice", "StockQuantity" },
                values: new object[] { new Guid("e50662b1-d078-43da-8c71-a74eafd590cc"), new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Local), null, 1, "Balls", false, 1, "16 Threats 4.5Ghz", "Intel Core I9", 300m, 5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e50662b1-d078-43da-8c71-a74eafd590cc"));

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ImageUrl", "IsDeleted", "ManufacturerId", "ProductDescription", "ProductName", "ProductPrice", "StockQuantity" },
                values: new object[] { new Guid("36c35b22-0d1c-4560-bde6-e7ab95c2a5c5"), new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Local), null, 1, "Balls", false, 1, "16 Threats 4.5Ghz", "Intel Core I9", 300m, 5 });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ApplicationUserId",
                table: "Orders",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_ApplicationUserId",
                table: "Orders",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
