using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcBuilder.Data.Models.Enums;

namespace PcBuilder.Web.ViewModels.Product
{
    public class AllProductsIndexViewModel
    {
	    public string Id { get; set; } = null!;

	    public string ProductName { get; set; } = null!;

	    public decimal ProductPrice { get; set; } 

	    public string AddedOn { get; set; } = null!;
	    public string? ImageUrl { get; set; }

		public string ManufacturerName { get; set; }  
		public string CategoryName { get; set; }
	}
}
