using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcBuilder.Data;
using PcBuilder.Data.Models;
using PcBuilder.Services.Data;
using PcBuilder.Services.Data.Interfaces;
using PcBuilder.Services.Mapping;
using PcBuilder.Web.ViewModels.Product;
using static PcBuilder.Common.DateValidation;

namespace PcBuilderWeb.Controllers
{
	[AllowAnonymous]
    public class ProductController : BaseController
    {
	    private readonly IProductService productService;

		public ProductController( IProductService productService)
		{
			this.productService = productService;
		}


		[HttpGet]
		public async Task<IActionResult> Index()
		{
			IEnumerable<AllProductsIndexViewModel> allProducts =
				await this.productService.GetAllProductsAsync();

			return this.View(allProducts);
		}


		[Authorize(Roles = "ADMIN")]
		[HttpGet]
		public async Task<IActionResult> AddProduct()
		{
			return View("AddProduct", new AddProductViewModel());
		}



		[Authorize(Roles = "ADMIN")]
		[HttpPost]
		public async Task<IActionResult> AddProduct(AddProductViewModel inputModel)
		{
			if (!this.ModelState.IsValid)
			{
				// Render the same form with user entered values + model errors 
				return this.View(inputModel);
			}

			bool result = await this.productService.AddProductAsync(inputModel);
			if (result == false)
			{
				this.ModelState.AddModelError(nameof(inputModel.AddedOn),
					String.Format("The Release Date must be in the following format: {0}", ReleaseDateFormat));
				return this.View(inputModel);
			}

			return this.RedirectToAction(nameof(Index));
		}




		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
			var productDetails = await productService.GetProductDetailsByIdAsync(id);
			if (productDetails == null)
			{
				return NotFound(); 
			}
			return View(productDetails);
		}
	}
}
