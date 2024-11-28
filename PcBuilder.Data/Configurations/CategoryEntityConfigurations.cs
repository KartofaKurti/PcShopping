using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Data.Models.Enums;

namespace PcBuilder.Data.Configurations
{
    public class CategoryEntityConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(Enum.GetValues(typeof(CategoryType))
                .Cast<CategoryType>()
                .Select(e => new Category
                {
                    Id = (int)e,
                    CategoryName = e.ToString()
                }));
        }
    }
}
