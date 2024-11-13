using System;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "Please enter the product name.")]
        [StringLength(100, ErrorMessage = "The product name must be between {2} and {1} characters.", MinimumLength = 2)]
        public string ProductName { get; set; } = null!;

        [Required(ErrorMessage = "Please enter a description.")]
        [StringLength(500, ErrorMessage = "The description must not exceed {1} characters.")]
        public string ProductDescription { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the product price.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The price must be a positive value.")]
        public decimal ProductPrice { get; set; }

        [Required(ErrorMessage = "Please enter the stock quantity.")]
        [Range(0, int.MaxValue, ErrorMessage = "The stock quantity must be zero or a positive number.")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "Please enter the date when the product was added.")]
        [RegularExpression(@"\d{4}-\d{2}-\d{2}", ErrorMessage = $"The date format must be ${ReleaseDateFormat}.")]
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
