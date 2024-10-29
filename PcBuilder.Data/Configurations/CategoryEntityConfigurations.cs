using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PcBuilder.Data.Configurations
{
    public class CategoryEntityConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new List<Category>
            {
                new Category { Id = 1, CategoryName = "Processor" },
                new Category { Id = 2, CategoryName = "Graphics Card" },
                new Category { Id = 3, CategoryName = "Memory" },
                new Category { Id = 4, CategoryName = "Storage" }
            });
        }
    }
}
