using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcBuilder.Data;
using PcBuilder.Data.Models;
using PcBuilder.Data.Models.Enums;
using PcBuilder.Services.Data;
using PcBuilder.Services.Data.Interfaces;
using PcBuilder.Services.Mapping;
using PcBuilder.Web.ViewModels.Product;
using static PcBuilder.Common.DateValidation;
using static PcBuilder.Common.RolesValidation;

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

        [AllowAnonymous]
        [HttpGet]
		public async Task<IActionResult> Index(int page = 1, int pageSize = 6)
		{
            IEnumerable<AllProductsIndexViewModel> allProducts =
                await this.productService.GetAllProductsAsync();


            if (!User.IsInRole(AdminRole))
            {
                allProducts = allProducts.Where(p => !p.isDeleted);
            }

            int totalProducts = allProducts.Count();

            IEnumerable<AllProductsIndexViewModel> paginatedProducts =
                allProducts.Skip((page - 1) * pageSize).Take(pageSize);

            var paginationModel = new PaginatedProductsViewModel
            {
                Products = paginatedProducts,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize)
            };

            return View(paginationModel);
        }


		[Authorize(Roles = AdminRole)]
		[HttpGet]
		public async Task<IActionResult> AddProduct()
		{
			return View("AddProduct", new AddProductViewModel());
		}



		[Authorize(Roles = AdminRole)]
		[HttpPost]
		public async Task<IActionResult> AddProduct(AddProductViewModel inputModel)
		{
			if (!ModelState.IsValid)
			{
				return View(inputModel);
			}
			bool result = await this.productService.AddProductAsync(inputModel);
			if (result == false)
			{
				this.ModelState.AddModelError(nameof(inputModel.AddedOn),
					String.Format($"The Release Date must be in the following format: {ReleaseDateFormat}"));
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

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await productService.GetProductDetailsByIdAsync(id);
            if (product == null)
            {
                return NotFound();  
            }

            var viewModel = new EditProductViewModel
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                StockQuantity = product.StockQuantity,
                ImageUrl = product.ImageUrl
            };

            return View(viewModel);  
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await productService.EditProductAsync(model);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
    }
}
