using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Data.Models
{
    public class Order
    {
		[Key]
		public Guid Id { get; set; }

		[Required]
		public DateTime OrderDate { get; set; }

		[Required]
		public int ProductQuantity { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		public decimal TotalPrice { get; set; } 

		public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
	}
}
