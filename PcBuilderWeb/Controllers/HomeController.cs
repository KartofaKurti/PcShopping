using Microsoft.AspNetCore.Mvc;
using PcBuilder.Services.Data.Interfaces;
using PcBuilder.Web.ViewModels;
using System.Diagnostics;
using PcBuilder.Web.ViewModels.Home;

namespace PcBuilderWeb.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult AboutUs()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Index()
        {
            
            var products = (await _productService.GetAvailableProductsAsync()).Take(4); 

            var viewModel = new HomePageViewModel
            {
                Products = products
            };

            return View(viewModel);
        }
    }
}
