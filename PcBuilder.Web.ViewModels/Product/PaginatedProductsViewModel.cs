using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Web.ViewModels.Product
{
    public class PaginatedProductsViewModel
    {
        public IEnumerable<AllProductsIndexViewModel> Products { get; set; } = Enumerable.Empty<AllProductsIndexViewModel>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
