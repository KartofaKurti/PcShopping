using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PcBuilder.Data.Models;
using PcBuilder.Data.Models.Enums;
using static PcBuilder.Common.DateValidation;


namespace PcBuilder.Web.ViewModels.Product
{
    public class AddProductViewModel
    {
	    public AddProductViewModel()
	    {
			this.AddedOn = DateTime.UtcNow.ToString(ReleaseDateFormat);
	    }
		public string ProductName { get; set; }
		public string ProductDescription { get; set; }
		[Required]
		public decimal ProductPrice { get; set; }
		[Required]
		public int StockQuantity { get; set; }
		[Required]
		public string AddedOn { get; set; }
		public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "Please select a category.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please select a manufacturer.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid manufacturer.")]
        public int ManufacturerId { get; set; }

    }
}
