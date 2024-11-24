using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PcBuilder.Web.ViewModels.Home
{
    public class HomePageViewModel
    {
        public IEnumerable<Data.Models.Product> Products { get; set; }
    }
}
