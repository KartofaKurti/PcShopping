using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Data.Models;

namespace PcBuilder.Data.Configurations
{
    public class ManufacturerEntityConfigurations : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.HasData(new List<Manufacturer>
            {
                new Manufacturer { Id = 1, ManufacturerName = "Intel" },
                new Manufacturer { Id = 2, ManufacturerName = "AMD" },
                new Manufacturer { Id = 3, ManufacturerName = "NVIDIA" }
               
            });
        }
    }
}
