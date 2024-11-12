using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Web.ViewModels.Order
{
	public class OrderItemViewModel
	{
		public string ProductName { get; set; }
		public decimal ProductPrice { get; set; }
		public int Quantity { get; set; }
	}
}
