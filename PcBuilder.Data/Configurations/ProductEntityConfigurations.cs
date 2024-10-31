using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Models;

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
            var products = new List<Product>()
            {
            new Product()
            {
                Id = Guid.NewGuid(),
                ProductName = "Intel Core I9",
                ProductPrice = 300,
                StockQuantity = 5,
                AddedOn = DateTime.Today,
                ProductDescription = "16 Threats 4.5Ghz",
                ImageUrl = "Balls",
                CategoryId = 1,
                ManufacturerId = 1,
            }};

            return products;
        }
    }
}
