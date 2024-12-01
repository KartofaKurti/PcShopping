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
		public IActionResult Error(int? statusCode)
		{
			var code = statusCode ?? 500;

			if (code == 401)
			{
				return View("Error401"); 
			}

			if (code == 404)
			{
				return View("Error404"); 
			}

			if (code == 500)
			{
				ViewBag.Message = "An unexpected error occurred.";
				return View("Error500"); 
			}

			ViewBag.StatusCode = code;
			ViewBag.Message = "An error occurred.";
			return View(); 
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
