using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Web.ViewModels.Product
{
    public class ProductSearchViewModel
    {
		public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int TotalResults { get; set; }
	}
}
