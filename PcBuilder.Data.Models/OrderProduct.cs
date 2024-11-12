using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Data.Models
{
	public class OrderProduct
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		[ForeignKey(nameof(Order))]
		public Guid OrderId { get; set; }
		public virtual Order Order { get; set; }

		[Required]
		[ForeignKey(nameof(Product))]
		public Guid ProductId { get; set; }

		public virtual Product Product { get; set; }

		[Required]
		public string ProductName { get; set; }

		[Required]
		public decimal Price { get; set; }

		[Required]
		public int Quantity { get; set; }
	}
}
