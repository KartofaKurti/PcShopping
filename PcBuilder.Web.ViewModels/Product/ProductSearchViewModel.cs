using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Web.ViewModels.Product
{
    public class ProductSearchViewModel
    {
        public string? Name { get; set; }
        public Guid? ManufacturerId { get; set; }
        public Guid? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public List<ProductDetailsViewModel> Products { get; set; } = new();
        public int TotalResults { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
