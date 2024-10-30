using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Data.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; } 

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int ProductQuantity { get; set; }

        [Required]
        public string Adress { get; set; }

        public IEnumerable<Product> Product { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
