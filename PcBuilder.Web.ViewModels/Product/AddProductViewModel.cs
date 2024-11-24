using System;
using System.ComponentModel.DataAnnotations;
using PcBuilder.Data.Models.Enums;
using static PcBuilder.Common.DateValidation;
using static PcBuilder.Common.ProductValidation;

namespace PcBuilder.Web.ViewModels.Product
{
    public class AddProductViewModel
    {
        public AddProductViewModel()
        {
            this.AddedOn = DateTime.UtcNow.ToString(ReleaseDateFormat);
        }

        [Required(ErrorMessage = NameError)]
        [StringLength(MaxlengthProductName, ErrorMessage = "The product name must be between {2} and {1} characters.", MinimumLength = MinlengthProductName)]
        public string ProductName { get; set; } = null!;

		[Required(ErrorMessage = DescriptionError)]
		[StringLength(MaxlengthLeDescription, ErrorMessage = "The description must not exceed {1} characters.")]
		public string ProductDescription { get; set; } = null!;

		[Required(ErrorMessage = PriceError)]
        [Range(0.01, double.MaxValue, ErrorMessage = PriceError)]
        public decimal ProductPrice { get; set; }

        [Required(ErrorMessage = QuantityError)]
        [Range(0, int.MaxValue, ErrorMessage = QuantityError)]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = ReleaseDateError)]
        [RegularExpression(@"\d{4}-\d{2}-\d{2}", ErrorMessage = $"The date format must be {ReleaseDateFormat}.")]
        public string AddedOn { get; set; }

        [StringLength(MaxlengthImageUrl, ErrorMessage = UrlError)]
        [RegularExpression(@"^(https?:\/\/.*)?$", ErrorMessage = UrlError)] 
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = CategoryError)]
        [Range(1, int.MaxValue, ErrorMessage = CategoryError)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = ManufacturerError)]
        [Range(1, int.MaxValue, ErrorMessage = ManufacturerError)]
        public int ManufacturerId { get; set; }
    }
}
