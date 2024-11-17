using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PcBuilder.Common.DateValidation;
using static PcBuilder.Common.ProductValidation;


namespace PcBuilder.Web.ViewModels.Cart
{
	public class ApplicationUserCartViewModel
	{
		public string ProductId { get; set; }

		[Required]
		[MaxLength(MaxlengthProductName)]
		public string ProductName { get; set; } = null!;

		[Required]
        [Range(0.01, double.MaxValue, ErrorMessage = PriceError)]
        public decimal ProductPrice { get; set; }

		public string AddedOn { get; set; } = null!;

		public string Manufacturer { get; set; }

        [Url]
        [StringLength(MaxlengthImageUrl)]
        public string? ImageUrl { get; set; }

		public string Category { get; set; }

		public int Quantity { get; set; }

		public decimal TotalPrice { get; set; }
	}
}
