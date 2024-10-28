using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Data.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string CategoryName { get; set; }
    }
}
