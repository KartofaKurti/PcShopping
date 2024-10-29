using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Data.Models
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public string ManufacturerName { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
