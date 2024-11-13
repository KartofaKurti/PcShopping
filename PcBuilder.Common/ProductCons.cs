using System.Security.Policy;

namespace PcBuilder.Common
{
    public class ProductCons
    {
        public const int MaxlengthProductName = 100;
        public const int MinlengthProductName = 5;
        public const int MinlengthDescription = 5;
        public const int MaxlengthImageUrl = 2048;
    }

    public class UserValidation
    {
        public const int MinLengthFirstName = 3;
        public const int MaxlengthFirstName = 20;
        public const int MinLengthSecondName = 3;
        public const int MaxlengthSecondName = 30;
    }

    public class ProductValidation
    {
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
    


}
