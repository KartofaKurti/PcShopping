using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Data.Models;
using System;
using System.Collections.Generic;

namespace PcBuilder.Data.Configurations
{
    public class ProductEntityConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(this.GenerateProducts());
        }

        private ICollection<Product> GenerateProducts()
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "HP Omen 45L Gaming Desktop",
                    ProductPrice = 2199.99M,
                    StockQuantity = 5,
                    AddedOn = DateTime.UtcNow.AddDays(-10),
                    ProductDescription = "High-performance gaming desktop with Intel Core i9-12900K and NVIDIA GeForce RTX 3080.",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR9XMpErvJJ23ttmNH27I0ryXkBctPnIdY6Cw&s",
                    CategoryId = 8, 
                    ManufacturerId = 18 
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Alienware Aurora R13",
                    ProductPrice = 3299.99M,
                    StockQuantity = 3,
                    AddedOn = DateTime.UtcNow.AddDays(-12),
                    ProductDescription = "Top-tier gaming desktop featuring Intel Core i9-12900KF and NVIDIA GeForce RTX 3090.",
                    ImageUrl = "https://m.media-amazon.com/images/I/61Qsj+79gpL.jpg",
                    CategoryId = 8, 
                    ManufacturerId = 17 
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "CyberPowerPC Gamer Supreme",
                    ProductPrice = 1499.99M,
                    StockQuantity = 10,
                    AddedOn = DateTime.UtcNow.AddDays(-20),
                    ProductDescription = "Mid-range gaming PC with AMD Ryzen 7 5800X and NVIDIA GeForce RTX 3060 Ti.",
                    ImageUrl = "placeholder",
                    CategoryId = 8, 
                    ManufacturerId = 20 
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "MSI Infinite RS 13th",
                    ProductPrice = 2599.99M,
                    StockQuantity = 7,
                    AddedOn = DateTime.UtcNow.AddDays(-8),
                    ProductDescription = "Gaming desktop with Intel Core i7-13700KF and NVIDIA GeForce RTX 4070 Ti.",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQMAbfTV4NnUxIDwvzYDthWyZ78yTVZ8uASjw&s",
                    CategoryId = 8, 
                    ManufacturerId = 5 
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Intel Core i9-12900K",
                    ProductPrice = 599.99M,
                    StockQuantity = 20,
                    AddedOn = DateTime.UtcNow.AddDays(-30),
                    ProductDescription = "12th Gen Intel Core Processor with 16 cores and 24 threads.",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQXX3ZSYTZvtMtFoaUA_oki-zPXvMGkVkEl5w&s", 
                    CategoryId = 1,
                    ManufacturerId = 1
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "AMD Ryzen 9 5900X",
                    ProductPrice = 499.99M,
                    StockQuantity = 25,
                    AddedOn = DateTime.UtcNow.AddDays(-45),
                    ProductDescription = "12-core, 24-thread unlocked desktop processor.",
                    ImageUrl = "https://ardes.bg/uploads/original/amd-ryzen-9-5900x-3-70ghz-294246.jpg",
                    CategoryId = 1,
                    ManufacturerId = 2
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Intel Core i5-12600K",
                    ProductPrice = 289.99M,
                    StockQuantity = 30,
                    AddedOn = DateTime.UtcNow.AddDays(-60),
                    ProductDescription = "Mid-range processor for gaming and productivity.",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSsjmv9eF3JH-x84rITfEsNzyqROaH77VFSbg&s",
                    CategoryId = 1,
                    ManufacturerId = 1
                },

               
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "NVIDIA GeForce RTX 3080",
                    ProductPrice = 799.99M,
                    StockQuantity = 10,
                    AddedOn = DateTime.UtcNow.AddDays(-15),
                    ProductDescription = "High-performance gaming graphics card.",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSKl2rQBycoQrqhqNZ2fZxvg9b6VwYvlpVU0A&s",
                    CategoryId = 2,
                    ManufacturerId = 3
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "AMD Radeon RX 6700 XT",
                    ProductPrice = 479.99M,
                    StockQuantity = 8,
                    AddedOn = DateTime.UtcNow.AddDays(-25),
                    ProductDescription = "High-end GPU for gamers and creators.",
                    ImageUrl = "https://static.gigabyte.com/StaticFile/Image/Global/0a0848c3f652206dc7f2c5c30c6cdf51/Product/28078/Png",
                    CategoryId = 2,
                    ManufacturerId = 2
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "NVIDIA GeForce RTX 3060",
                    ProductPrice = 329.99M,
                    StockQuantity = 15,
                    AddedOn = DateTime.UtcNow.AddDays(-10),
                    ProductDescription = "Affordable GPU for gaming and rendering.",
                    ImageUrl = "https://desktop.bg/system/images/330952/original/asus_dual_geforce_rtx_3060_v2_oc.png",
                    CategoryId = 2,
                    ManufacturerId = 3
                },

               
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Corsair Vengeance LPX 16GB",
                    ProductPrice = 79.99M,
                    StockQuantity = 50,
                    AddedOn = DateTime.UtcNow.AddDays(-12),
                    ProductDescription = "High-performance DDR4 RAM for gaming PCs.",
                    ImageUrl = "https://desktop.bg/system/images/335374/original/16gb_2x8gb_ddr4_3200mhz_corsair_vengeance_lpx_black_duplicate.png",
                    CategoryId = 3,
                    ManufacturerId = 7
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "G.Skill Trident Z RGB 16GB",
                    ProductPrice = 89.99M,
                    StockQuantity = 40,
                    AddedOn = DateTime.UtcNow.AddDays(-14),
                    ProductDescription = "Stylish DDR4 RAM with RGB lighting.",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQHgmi8hPa4cN1-i4J1Yoj3pJosnb4Y9qkjuA&s",
                    CategoryId = 3,
                    ManufacturerId = 9
                },

               
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Samsung 980 PRO 1TB SSD",
                    ProductPrice = 149.99M,
                    StockQuantity = 20,
                    AddedOn = DateTime.UtcNow.AddDays(-20),
                    ProductDescription = "Fast PCIe 4.0 NVMe SSD for high-performance systems.",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRbyzFKbMYhmuBtvMIYYqKdu6mKjajg-dMchg&s",
                    CategoryId = 4,
                    ManufacturerId = 8
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Seagate Barracuda 2TB HDD",
                    ProductPrice = 49.99M,
                    StockQuantity = 35,
                    AddedOn = DateTime.UtcNow.AddDays(-5),
                    ProductDescription = "Reliable 2TB hard drive for storage solutions.",
                    ImageUrl = "https://ardes.bg/uploads/original/seagate-hdd-desktop-barracuda-guardian-3-5-2tb-sat-202842.jpg",
                    CategoryId = 4,
                    ManufacturerId = 10
                },

                
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Corsair RM750x",
                    ProductPrice = 129.99M,
                    StockQuantity = 15,
                    AddedOn = DateTime.UtcNow.AddDays(-22),
                    ProductDescription = "750W fully modular power supply.",
                    ImageUrl = "https://cdn.ozone.bg/media/catalog/product/z/a/zahranvane_corsair_rm750x_750w_1649168291_0.jpg",
                    CategoryId = 6,
                    ManufacturerId = 7
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "EVGA SuperNOVA 850 G5",
                    ProductPrice = 139.99M,
                    StockQuantity = 10,
                    AddedOn = DateTime.UtcNow.AddDays(-30),
                    ProductDescription = "850W 80+ Gold certified power supply.",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTrYGL_1tHmDzXpC5ougJrrGMSAzbmF4m96Nw&s",
                    CategoryId = 6,
                    ManufacturerId = 12
                },

                
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Cooler Master Hyper 212 Black Edition",
                    ProductPrice = 39.99M,
                    StockQuantity = 20,
                    AddedOn = DateTime.UtcNow.AddDays(-28),
                    ProductDescription = "Popular air cooler for CPUs.",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTrYLf6w4qlkOW7Fiq2rHDHMCfAUGsTJ285bw&s",
                    CategoryId = 7,
                    ManufacturerId = 14
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "NZXT Kraken Z73",
                    ProductPrice = 249.99M,
                    StockQuantity = 8,
                    AddedOn = DateTime.UtcNow.AddDays(-50),
                    ProductDescription = "Premium AIO liquid cooler with customizable display.",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQU4PVoTx-579S2oM2ogZfGlGgh4ou0pRtiQQ&s",
                    CategoryId = 7,
                    ManufacturerId = 16
                },

                
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Dell UltraSharp U2723QE",
                    ProductPrice = 649.99M,
                    StockQuantity = 12,
                    AddedOn = DateTime.UtcNow.AddDays(-18),
                    ProductDescription = "27-inch 4K UHD monitor with excellent color accuracy.",
                    ImageUrl = "https://gfx3.senetic.com/akeneo-catalog/0/b/d/1/0bd1c975a95be30f9e40a9a2a149c65aabdcf696_1651906_DELL_U2723QE_image1.jpg",
                    CategoryId = 9,
                    ManufacturerId = 17
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "LG UltraGear 27GP950-B",
                    ProductPrice = 799.99M,
                    StockQuantity = 5,
                    AddedOn = DateTime.UtcNow.AddDays(-35),
                    ProductDescription = "27-inch 4K 144Hz gaming monitor with HDR support.",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRsH4TNyBkDn0CnGXN6sXfv4nE1eF1B7rgUWA&s",
                    CategoryId = 9,
                    ManufacturerId = 19
                },
            };

            return products;
        }
    }
}
