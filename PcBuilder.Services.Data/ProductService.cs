using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Core;
using PcBuilder.Data.Models;
using PcBuilder.Data.Repository.Interfaces;
using PcBuilder.Services.Data.Interfaces;
using PcBuilder.Services.Mapping;
using PcBuilder.Web.ViewModels.Product;
using Manufacturer = PcBuilder.Data.Models.Enums.ManufacturerType;
using static PcBuilder.Common.DateValidation;
using static PcBuilder.Common.ProductValidation;

namespace PcBuilder.Services.Data
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductService(IRepository<Product, Guid> productRepository)
        {
            this._productRepository = productRepository;
        }


        public async Task<IEnumerable<AllProductsIndexViewModel>> GetAllProductsAsync()
        {
	        return await _productRepository
		        .GetAllAttached() 
		        .Include(p => p.Manufacturer) 
		        .Include(p => p.Category) 
		        .Select(product => new AllProductsIndexViewModel
		        {
			        Id = product.Id.ToString(),
			        ProductName = product.ProductName,
			        ProductPrice = product.ProductPrice,
			        AddedOn = product.AddedOn.ToString(ReleaseDateFormat),
			        ManufacturerName = product.Manufacturer.ManufacturerName, 
			        CategoryName = product.Category.CategoryName,
					ImageUrl = product.ImageUrl ?? ImageNotFoundUrl,
					
		        })
		        .ToListAsync();

		}

        public async Task<bool> AddProductAsync(AddProductViewModel inputModel)
        {
			bool isReleaseDateValid = DateTime
				.TryParseExact(inputModel.AddedOn, ReleaseDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
					out DateTime releaseDate);
			if (!isReleaseDateValid)
			{
				return false;
			}

			Product product = new Product
			{
				ProductName = inputModel.ProductName,
				ProductDescription = inputModel.ProductDescription,
				ProductPrice = inputModel.ProductPrice,
				StockQuantity = inputModel.StockQuantity,
				AddedOn = releaseDate,
				ImageUrl = inputModel.ImageUrl ?? ImageNotFoundUrl,
				ManufacturerId = inputModel.ManufacturerId,
				CategoryId = inputModel.CategoryId,
				IsDeleted = false
				
			};

			await this._productRepository.AddAsync(product);

			return true;
		}

        public async Task<ProductDetailsViewModel?> GetProductDetailsByIdAsync(Guid id)
        {
	        Product? product = await _productRepository
		        .GetAllAttached()
		        .Include(p => p.Manufacturer)
		        .Include(p => p.Category)
		        .FirstOrDefaultAsync(p => p.Id == id); 

	        ProductDetailsViewModel? viewModel = null;
	        if (product != null)
	        {
		        viewModel = new ProductDetailsViewModel
		        {
			        ProductName = product.ProductName,
			        ProductDescription = product.ProductDescription,
			        ProductPrice = product.ProductPrice,
			        StockQuantity = product.StockQuantity,
			        AddedOn = product.AddedOn.ToString(ReleaseDateFormat),
			        ImageUrl = product.ImageUrl,
			        ManufacturerName = product.Manufacturer?.ManufacturerName, 
			        Category = product.Category?.CategoryName 
		        };
	        }

	        return viewModel;
		}
    }
}
