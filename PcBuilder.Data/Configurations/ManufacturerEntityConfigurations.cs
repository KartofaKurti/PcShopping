using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcBuilder.Data.Models;
using PcBuilder.Data.Models.Enums;

namespace PcBuilder.Data.Configurations
{
    public class ManufacturerEntityConfigurations : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.HasData(Enum.GetValues(typeof(ManufacturerType))
                .Cast<ManufacturerType>()
                .Select(e => new Manufacturer
                {
                    Id = (int)e,
                    ManufacturerName = e.ToString()
                }));
        }
    }
}
