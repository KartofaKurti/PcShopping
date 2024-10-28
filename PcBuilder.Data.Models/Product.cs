using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Data.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int StockQuantity { get; set; }
        public string ProductDescription { get; set; }
        public string ImageUrl { get; set; }
        public string ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
