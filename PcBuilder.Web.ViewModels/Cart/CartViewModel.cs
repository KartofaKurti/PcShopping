using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Web.ViewModels.Cart
{
	public class CartViewModel
	{
		public List<ApplicationUserCartViewModel> Items { get; set; } = new List<ApplicationUserCartViewModel>();
		public decimal TotalPrice { get; set; }
	}
}
