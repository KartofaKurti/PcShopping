using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Data.Models
{
    public class ApplicationUserProduct
    {
		public Guid ApplicationUserId { get; set; }

		public virtual ApplicationUser ApplicationUser { get; set; } = null!;

		public Guid ProductId { get; set; }

		public virtual Product Product { get; set; } = null!;

		public int Quantity { get; set; }
	}
}
