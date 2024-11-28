using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PcBuilder.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d16c5f7c-9a95-4afe-8c5c-daa7eb876f10"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("0d7e387e-ec0d-400a-b909-d28a9535cd91"), 0, "a0fcfc1e-2f85-42a2-ab9e-b439d3a4e0c1", "jane.smith@example.com", false, "Jane", "Smith", false, null, "JANE.SMITH@EXAMPLE.COM", "JANE.SMITH@EXAMPLE.COM", "AQAAAAIAAYagAAAAEL3Wh3dNoVvf2b3O0o2hXCSJ5u18dN7ENT/NwmmDeiGhVCfysV7hCQCIDA643WbUjA==", null, false, "fbb2d54e-c535-45d9-9997-f5dda9faf7e2", false, "jane.smith@example.com" },
                    { new Guid("247cefe3-c6c3-4c42-b121-e67ca56c804e"), 0, "112c332b-e268-45b9-86dc-95790d9cb6c6", "admin@example.com", false, "Admin", "User", false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAENiMADEnMPlTWpKTqSBdwsl6kc/9UJP1FGS2xmdBWkvGQUvilmtndNhC4DjlZqNJsQ==", null, false, "dfcd70e5-8e23-40ee-9d46-514e224a16ef", false, "admin@example.com" },
                    { new Guid("d89d55d0-643e-4c2c-ba10-cf4af53bdd5a"), 0, "8e9a37ab-a8b0-45d5-a1d5-5da25410fe18", "john.doe@example.com", false, "John", "Doe", false, null, "JOHN.DOE@EXAMPLE.COM", "JOHN.DOE@EXAMPLE.COM", "AQAAAAIAAYagAAAAENgZWHdDAyBKL+GOCN4LrKYYO80xeSNGQbQj6VpyvrztlbMnzuOqtPsXDViPeA1kxw==", null, false, "d9f20ede-5b62-496e-9bdf-7684bd5d2fba", false, "john.doe@example.com" }
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CategoryName",
                value: "GraphicCard");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 5, "Motherboard" },
                    { 6, "PowerSupply" },
                    { 7, "CoolingSystem" },
                    { 8, "Case" },
                    { 9, "Monitor" },
                    { 10, "Keyboard" },
                    { 11, "Mouse" },
                    { 12, "Headset" },
                    { 13, "Speaker" },
                    { 14, "Networking" },
                    { 15, "Software" },
                    { 16, "Accessories" },
                    { 17, "ExternalStorage" },
                    { 18, "Printer" },
                    { 19, "Scanner" },
                    { 20, "UPS" }
                });

            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 3,
                column: "ManufacturerName",
                value: "Nvidia");

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "Id", "ManufacturerName" },
                values: new object[,]
                {
                    { 4, "Asus" },
                    { 5, "MSI" },
                    { 6, "Gigabyte" },
                    { 7, "Corsair" },
                    { 8, "Samsung" },
                    { 9, "Kingston" },
                    { 10, "Seagate" },
                    { 11, "WesternDigital" },
                    { 12, "EVGA" },
                    { 13, "Zotac" },
                    { 14, "CoolerMaster" },
                    { 15, "Razer" },
                    { 16, "Logitech" },
                    { 17, "Dell" },
                    { 18, "HP" },
                    { 19, "Lenovo" },
                    { 20, "Acer" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ImageUrl", "IsDeleted", "ManufacturerId", "ProductDescription", "ProductName", "ProductPrice", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("38b6673d-1960-46c2-9118-4fbacf618be2"), new DateTime(2024, 9, 29, 18, 41, 57, 328, DateTimeKind.Utc).AddTicks(8863), null, 1, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSsjmv9eF3JH-x84rITfEsNzyqROaH77VFSbg&s", false, 1, "Mid-range processor for gaming and productivity.", "Intel Core i5-12600K", 289.99m, 30 },
                    { new Guid("5b98ef71-ba75-4308-8493-332b29dd7a3a"), new DateTime(2024, 11, 3, 18, 41, 57, 328, DateTimeKind.Utc).AddTicks(8870), null, 2, "https://static.gigabyte.com/StaticFile/Image/Global/0a0848c3f652206dc7f2c5c30c6cdf51/Product/28078/Png", false, 2, "High-end GPU for gamers and creators.", "AMD Radeon RX 6700 XT", 479.99m, 8 },
                    { new Guid("6fffccc1-cb82-4ac6-834d-752a74d736b5"), new DateTime(2024, 10, 14, 18, 41, 57, 328, DateTimeKind.Utc).AddTicks(8861), null, 1, "https://ardes.bg/uploads/original/amd-ryzen-9-5900x-3-70ghz-294246.jpg", false, 2, "12-core, 24-thread unlocked desktop processor.", "AMD Ryzen 9 5900X", 499.99m, 25 },
                    { new Guid("bcc0c8b5-8c1f-4fd0-9c33-964e175f3666"), new DateTime(2024, 11, 13, 18, 41, 57, 328, DateTimeKind.Utc).AddTicks(8868), null, 2, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSKl2rQBycoQrqhqNZ2fZxvg9b6VwYvlpVU0A&s", false, 3, "High-performance gaming graphics card.", "NVIDIA GeForce RTX 3080", 799.99m, 10 },
                    { new Guid("be327c0b-4042-4852-809d-3e07cadef3dc"), new DateTime(2024, 11, 18, 18, 41, 57, 328, DateTimeKind.Utc).AddTicks(8873), null, 2, "https://desktop.bg/system/images/330952/original/asus_dual_geforce_rtx_3060_v2_oc.png", false, 3, "Affordable GPU for gaming and rendering.", "NVIDIA GeForce RTX 3060", 329.99m, 15 },
                    { new Guid("fca25a7d-5374-4d7d-b0fe-dd86663a8503"), new DateTime(2024, 10, 29, 18, 41, 57, 328, DateTimeKind.Utc).AddTicks(8847), null, 1, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQXX3ZSYTZvtMtFoaUA_oki-zPXvMGkVkEl5w&s", false, 1, "12th Gen Intel Core Processor with 16 cores and 24 threads.", "Intel Core i9-12900K", 599.99m, 20 },
                    { new Guid("00460a6d-6988-44dc-b931-d53e7ea17dfe"), new DateTime(2024, 10, 31, 18, 41, 57, 328, DateTimeKind.Utc).AddTicks(8928), null, 7, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTrYLf6w4qlkOW7Fiq2rHDHMCfAUGsTJ285bw&s", false, 14, "Popular air cooler for CPUs.", "Cooler Master Hyper 212 Black Edition", 39.99m, 20 },
                    { new Guid("39eab59d-d36d-44a1-99a7-cdabc1f4d77b"), new DateTime(2024, 11, 6, 18, 41, 57, 328, DateTimeKind.Utc).AddTicks(8921), null, 6, "https://cdn.ozone.bg/media/catalog/product/z/a/zahranvane_corsair_rm750x_750w_1649168291_0.jpg", false, 7, "750W fully modular power supply.", "Corsair RM750x", 129.99m, 15 },
                    { new Guid("50c54595-b45d-4d7f-b6d8-97aa8eb9f9a6"), new DateTime(2024, 11, 23, 18, 41, 57, 328, DateTimeKind.Utc).AddTicks(8885), null, 4, "https://ardes.bg/uploads/original/seagate-hdd-desktop-barracuda-guardian-3-5-2tb-sat-202842.jpg", false, 10, "Reliable 2TB hard drive for storage solutions.", "Seagate Barracuda 2TB HDD", 49.99m, 35 },
                    { new Guid("8d1ad771-e0c2-42f7-b676-36cd5d826f7e"), new DateTime(2024, 11, 18, 18, 41, 57, 328, DateTimeKind.Utc).AddTicks(8793), null, 8, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR9XMpErvJJ23ttmNH27I0ryXkBctPnIdY6Cw&s", false, 18, "High-performance gaming desktop with Intel Core i9-12900K and NVIDIA GeForce RTX 3080.", "HP Omen 45L Gaming Desktop", 2199.99m, 5 },
                    { new Guid("9557f8a4-82a5-4df4-8589-2acf04dd9137"), new DateTime(2024, 11, 16, 18, 41, 57, 328, DateTimeKind.Utc).AddTicks(8829), null, 8, "https://m.media-amazon.com/images/I/61Qsj+79gpL.jpg", false, 17, "Top-tier gaming desktop featuring Intel Core i9-12900KF and NVIDIA GeForce RTX 3090.", "Alienware Aurora R13", 3299.99m, 3 },
                    { new Guid("a8698aa1-3850-4ad6-a7c4-4425412af140"), new DateTime(2024, 10, 9, 18, 41, 57, 328, DateTimeKind.Utc).AddTicks(8932), null, 7, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQU4PVoTx-579S2oM2ogZfGlGgh4ou0pRtiQQ&s", false, 16, "Premium AIO liquid cooler with customizable display.", "NZXT Kraken Z73", 249.99m, 8 },
                    { new Guid("ad1dd074-4076-4fe9-9e7c-72b36638f784"), new DateTime(2024, 11, 14, 18, 41, 57, 328, DateTimeKind.Utc).AddTicks(8880), null, 3, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQHgmi8hPa4cN1-i4J1Yoj3pJosnb4Y9qkjuA&s", false, 9, "Stylish DDR4 RAM with RGB lighting.", "G.Skill Trident Z RGB 16GB", 89.99m, 40 },
                    { new Guid("b2a45586-25af-446b-b80f-9c7fcc084d50"), new DateTime(2024, 10, 29, 18, 41, 57, 328, DateTimeKind.Utc).AddTicks(8926), null, 6, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTrYGL_1tHmDzXpC5ougJrrGMSAzbmF4m96Nw&s", false, 12, "850W 80+ Gold certified power supply.", "EVGA SuperNOVA 850 G5", 139.99m, 10 },
                    { new Guid("b2c04416-5fca-4924-8d4d-9606ea3520d7"), new DateTime(2024, 11, 8, 18, 41, 57, 328, DateTimeKind.Utc).AddTicks(8832), null, 8, "placeholder", false, 20, "Mid-range gaming PC with AMD Ryzen 7 5800X and NVIDIA GeForce RTX 3060 Ti.", "CyberPowerPC Gamer Supreme", 1499.99m, 10 },
                    { new Guid("ce98137e-68b1-48fa-8f9e-7b08a23d0a3f"), new DateTime(2024, 11, 16, 18, 41, 57, 328, DateTimeKind.Utc).AddTicks(8876), null, 3, "https://desktop.bg/system/images/335374/original/16gb_2x8gb_ddr4_3200mhz_corsair_vengeance_lpx_black_duplicate.png", false, 7, "High-performance DDR4 RAM for gaming PCs.", "Corsair Vengeance LPX 16GB", 79.99m, 50 },
                    { new Guid("d417f1b8-0667-42f7-9c65-a5d050c6d019"), new DateTime(2024, 11, 10, 18, 41, 57, 328, DateTimeKind.Utc).AddTicks(8934), null, 9, "https://gfx3.senetic.com/akeneo-catalog/0/b/d/1/0bd1c975a95be30f9e40a9a2a149c65aabdcf696_1651906_DELL_U2723QE_image1.jpg", false, 17, "27-inch 4K UHD monitor with excellent color accuracy.", "Dell UltraSharp U2723QE", 649.99m, 12 },
                    { new Guid("e1aa8a4e-2c68-4c82-b189-785edfb9d973"), new DateTime(2024, 10, 24, 18, 41, 57, 328, DateTimeKind.Utc).AddTicks(8938), null, 9, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRsH4TNyBkDn0CnGXN6sXfv4nE1eF1B7rgUWA&s", false, 19, "27-inch 4K 144Hz gaming monitor with HDR support.", "LG UltraGear 27GP950-B", 799.99m, 5 },
                    { new Guid("eb6ebcd4-9497-4d89-98ae-81e89e8453bc"), new DateTime(2024, 11, 8, 18, 41, 57, 328, DateTimeKind.Utc).AddTicks(8882), null, 4, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRbyzFKbMYhmuBtvMIYYqKdu6mKjajg-dMchg&s", false, 8, "Fast PCIe 4.0 NVMe SSD for high-performance systems.", "Samsung 980 PRO 1TB SSD", 149.99m, 20 },
                    { new Guid("f8865874-b830-4a31-bd84-a54a941064c1"), new DateTime(2024, 11, 20, 18, 41, 57, 328, DateTimeKind.Utc).AddTicks(8845), null, 8, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQMAbfTV4NnUxIDwvzYDthWyZ78yTVZ8uASjw&s", false, 5, "Gaming desktop with Intel Core i7-13700KF and NVIDIA GeForce RTX 4070 Ti.", "MSI Infinite RS 13th", 2599.99m, 7 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("0d7e387e-ec0d-400a-b909-d28a9535cd91"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("247cefe3-c6c3-4c42-b121-e67ca56c804e"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d89d55d0-643e-4c2c-ba10-cf4af53bdd5a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00460a6d-6988-44dc-b931-d53e7ea17dfe"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("38b6673d-1960-46c2-9118-4fbacf618be2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("39eab59d-d36d-44a1-99a7-cdabc1f4d77b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("50c54595-b45d-4d7f-b6d8-97aa8eb9f9a6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5b98ef71-ba75-4308-8493-332b29dd7a3a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6fffccc1-cb82-4ac6-834d-752a74d736b5"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8d1ad771-e0c2-42f7-b676-36cd5d826f7e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9557f8a4-82a5-4df4-8589-2acf04dd9137"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a8698aa1-3850-4ad6-a7c4-4425412af140"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ad1dd074-4076-4fe9-9e7c-72b36638f784"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b2a45586-25af-446b-b80f-9c7fcc084d50"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b2c04416-5fca-4924-8d4d-9606ea3520d7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bcc0c8b5-8c1f-4fd0-9c33-964e175f3666"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("be327c0b-4042-4852-809d-3e07cadef3dc"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ce98137e-68b1-48fa-8f9e-7b08a23d0a3f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d417f1b8-0667-42f7-9c65-a5d050c6d019"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e1aa8a4e-2c68-4c82-b189-785edfb9d973"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("eb6ebcd4-9497-4d89-98ae-81e89e8453bc"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f8865874-b830-4a31-bd84-a54a941064c1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fca25a7d-5374-4d7d-b0fe-dd86663a8503"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CategoryName",
                value: "Graphics Card");

            migrationBuilder.UpdateData(
                table: "Manufacturers",
                keyColumn: "Id",
                keyValue: 3,
                column: "ManufacturerName",
                value: "NVIDIA");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AddedOn", "ApplicationUserId", "CategoryId", "ImageUrl", "IsDeleted", "ManufacturerId", "ProductDescription", "ProductName", "ProductPrice", "StockQuantity" },
                values: new object[] { new Guid("d16c5f7c-9a95-4afe-8c5c-daa7eb876f10"), new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Local), null, 1, "Balls", false, 1, "16 Threats 4.5Ghz", "Intel Core I9", 300m, 5 });
        }
    }
}
