using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Serialization;
using PcBuilder.Data.Models.Enums;
using static PcBuilder.Common.DateValidation;
using static PcBuilder.Common.ProductValidation;


namespace PcBuilder.Web.ViewModels.Product
{
    public class EditProductViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(MaxlengthProductName)]
        public string ProductName { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = QuantityError)]
        public int StockQuantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = PriceError)]
        public decimal ProductPrice { get; set; }


        [StringLength(MaxlengthLeDescription)]
        public string ProductDescription { get; set; }

        [Url]
        [StringLength(MaxlengthImageUrl)]
        public string? ImageUrl { get; set; }
    }
}