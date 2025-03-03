﻿using PcBuilder.Web.ViewModels.Cart;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PcBuilder.Common.DateValidation;
using static PcBuilder.Common.OrderValidation;

namespace PcBuilder.Web.ViewModels.Order
{
	public class OrderViewModel
	{
		public string OrderId { get; set; }  

		[Display(Name = "Order Date")]
		public string OrderDate { get; set; } 

		public IEnumerable<ApplicationUserCartViewModel> CartItems { get; set; } = new List<ApplicationUserCartViewModel>();

		[Display(Name = "Total Quantity")]
		public int ProductQuantity { get; set; }

		[Display(Name = "Total Price")]
		[DataType(DataType.Currency)]
		public decimal TotalPrice { get; set; }

		[Required(ErrorMessage = MissingAddress)]
		[StringLength(MaxLenghtAdress, ErrorMessage = AddressError)]
		public string Address { get; set; } = string.Empty;
	}
}
