using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Data.Models
{
	public  class AplicationUserOrder
	{
		public Guid OrderId { get; set; }
		public virtual Order Order { get; set; }

		public Guid ApplicationUserId { get; set; }

		public virtual ApplicationUser ApplicationUser { get; set; } = null!;

		

	}

}
