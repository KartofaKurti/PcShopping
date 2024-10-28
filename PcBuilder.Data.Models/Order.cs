using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Data.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int ProductQuantity { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }

    }
}
