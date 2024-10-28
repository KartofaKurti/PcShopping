using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using static PcBuilder.Common.UserValidation;

namespace PcBuilder.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
            SecurityStamp = Guid.NewGuid().ToString();
            Products = new List<Product>();
        }
        [Required, MaxLength(MaxlengthFirstName)]
        public string FirstName { get; set; } = null!;

        [Required, MaxLength(MaxlengthSecondName)]
        public string LastName { get; set; } = null!;

        public virtual IEnumerable<Product> Products { get; set; }


    }
}
