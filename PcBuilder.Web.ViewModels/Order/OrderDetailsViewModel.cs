using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Web.ViewModels.Order
{
	public class OrderDetailsViewModel
	{
		public Guid OrderId { get; set; }
		public DateTime OrderDate { get; set; }
		public int ProductQuantity { get; set; }
		public decimal TotalPrice { get; set; }
		public string Address { get; set; }
		public List<OrderItemViewModel> OrderItems { get; set; }
		public string UserName { get; set; }
	}
}
