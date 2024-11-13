using System;
using System.ComponentModel.DataAnnotations;
using PcBuilder.Data.Models.Enums;
using static PcBuilder.Common.DateValidation;

namespace PcBuilder.Web.ViewModels.Product
{
    public class EditProductViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
        public int StockQuantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Product price must be greater than zero.")]
        public decimal ProductPrice { get; set; }

        [StringLength(1000)]
        public string ProductDescription { get; set; }

        [Url]
        [StringLength(500)]
        public string ImageUrl { get; set; }
    }
}