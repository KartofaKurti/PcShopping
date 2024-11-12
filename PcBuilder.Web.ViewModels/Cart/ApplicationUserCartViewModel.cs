using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Web.ViewModels.Cart
{
	public class ApplicationUserCartViewModel
	{
		public string ProductId { get; set; }
		public string ProductName { get; set; } = null!;

		public decimal ProductPrice { get; set; }

		public string AddedOn { get; set; } = null!;

		public string Manufacturer { get; set; }
		public string? ImageUrl { get; set; }

		public string Category { get; set; }
		public int Quantity { get; set; }

		public decimal TotalPrice { get; set; }
	}
}
