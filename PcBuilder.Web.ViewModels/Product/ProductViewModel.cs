﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Web.ViewModels.Product
{
	public class ProductViewModel
	{
		public Guid Id { get; set; }
		public string ProductName { get; set; }
		public string ProductDescription { get; set; }
		public decimal ProductPrice { get; set; }
		public string? ImageUrl { get; set; }
		public string ManufacturerName { get; set; }
		public string Category { get; set; }
	}
}
