using System.Security.Policy;

namespace PcBuilder.Common
{
    public class UserValidation
    {
        public const int MinLengthFirstName = 3;
        public const int MaxlengthFirstName = 20;
        public const int MinLengthSecondName = 3;
        public const int MaxlengthSecondName = 30;
    }

    public class ProductValidation
    {
        public const string QuantityError = "Quantity must be a non-negative number.";
        public const string PriceError = "Product price must be greater than zero.";
        public const string NameError = "Please enter the product name.";
        public const string DescriptionError = "Please enter a description.";
        public const string ReleaseDateError = "Please enter the date when the product was added.";
        public const string CategoryError = "Please select a category.";
        public const string ManufacturerError = "Please select a manufacturer.";
        public const string UrlError = "The image URL must not exceed {1} characters.";

        public const int MaxlengthProductName = 100;
        public const int MinlengthProductName = 5;
        public const int MinlengthDescription = 5;
        public const int MaxlengthImageUrl = 2048;
        public const int MaxlengthLeDescription = 1024;

        public const string ImageNotFoundUrl =
			"https://media.istockphoto.com/id/1409329028/vector/no-picture-available-placeholder-thumbnail-icon-illustration-design.jpg?s=612x612&w=0&k=20&c=_zOuJu755g2eEUioiOUdz_mHKJQJn-tDgIAhQzyeKUQ=";
    }

    public class DateValidation()
    {
	    public const string ReleaseDateFormat = "MM/yyyy";
    }

    public class RolesValidation()
    {
        public const string AdminRole = "ADMIN";
        public const string UserRole = "USER";
    }

    public class OrderValidation()
    {
        public const string MissingAddress = "Please enter a shipping address.";
        public const int MaxLenghtAdress = 200;
        public const string AddressError = "Address cannot exceed 200 characters.";
    }
    


}
