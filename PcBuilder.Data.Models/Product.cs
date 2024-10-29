using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PcBuilder.Common.ProductCons;


namespace PcBuilder.Data.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MinLength(MinlengthProductName)]
        [MaxLength(MaxlengthProductName)]
        public string ProductName { get; set; } = null!;

        [Required]
        public decimal ProductPrice { get; set; } 

        [Required]
        public int StockQuantity { get; set; }

        [Required]
        public DateTime AddedOn { get; set; }

        [Required]
        [MinLength(MinlengthDescription)]
        public string ProductDescription { get; set; } = null!;

        [Required]
        [MaxLength(MaxlengthImageUrl)]
        public string ImageUrl { get; set; } = null!;

        [ForeignKey(nameof(ProductCategory))]
        public int ProductCategoryId { get; set; }
        public virtual Category ProductCategory { get; set; } = null!;

        [ForeignKey(nameof(Manufacturer))]
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; } = null!;
    }
}
