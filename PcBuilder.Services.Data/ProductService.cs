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
using PcBuilder.Data.Models.Enums;
using NuGet.Protocol.Core.Types;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

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
					Quantity = product.StockQuantity,
					isDeleted = product.IsDeleted,
					
					
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
			if (string.IsNullOrWhiteSpace(inputModel.ProductName))
			{
				return false;  
			}

			if (inputModel.ProductPrice <= 0)
			{
				return false; 
			}

			if (inputModel.StockQuantity <= 0)
			{
				return false;  
			}

		
			if (!string.IsNullOrEmpty(inputModel.ImageUrl))
			{
				var regex = new Regex(@"^(https?:\/\/.*)?$");
				if (!regex.IsMatch(inputModel.ImageUrl))
				{
					return false;  
				}
			}

		
			Product product = new Product
			{
				ProductName = inputModel.ProductName,
				ProductDescription = inputModel.ProductDescription ?? "No description available",
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
            Product? product = await GetProductByIdAsync(id); 

            ProductDetailsViewModel? viewModel = null;
            if (product != null)
            {
                viewModel = new ProductDetailsViewModel
                {
                    Id = product.Id.ToString(),
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

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            return await _productRepository
                .GetAllAttached()  
                .Include(p => p.Manufacturer)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

       

        public async Task<IEnumerable<Product>> GetAvailableProductsAsync()
        {
            return await _productRepository.GetAllAttached()
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<bool> ToggleProductVisibilityAsync(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return false; 
            }

            product.IsDeleted = !product.IsDeleted;
            await _productRepository.UpdateAsync(product);
            return true;
        }

        public async Task<bool> HardDeleteProductAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }

            await _productRepository.DeleteAsync(product);
            return true;
        }

        public async Task<bool> EditProductAsync(EditProductViewModel model)
        {
            var product = await GetProductByIdAsync(Guid.Parse(model.Id));

            if (product == null)
            {
                return false;
            }

            product.ProductName = model.ProductName;
            product.ProductDescription = model.ProductDescription;
            product.ProductPrice = model.ProductPrice;
            product.StockQuantity = model.StockQuantity;
            product.ImageUrl = model.ImageUrl;

            await _productRepository.UpdateAsync(product);
            return true;
        }

        public async Task<PaginatedProductsViewModel> SearchProductsAsync(
    string? name,
    int? manufacturerId,
    int? categoryId,
    decimal? minPrice,
    decimal? maxPrice,
    int page = 1,
    int pageSize = 18)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 18;

            var query = _productRepository.GetAllAttached();

            query = query.Where(p => !p.IsDeleted);

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => EF.Functions.Like(p.ProductName, $"%{name}%"));
            }

            if (manufacturerId.HasValue)
            {
                query = query.Where(p => p.ManufacturerId == manufacturerId.Value);
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.ProductPrice >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.ProductPrice <= maxPrice.Value);
            }

            
            query = query
                .Include(p => p.Manufacturer)
                .Include(p => p.Category);

            var totalResults = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalResults / (double)pageSize);

            var products = await query
                .OrderBy(p => p.ProductName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var paginatedViewModel = new PaginatedProductsViewModel
            {
                Products = products.Select(p => new AllProductsIndexViewModel
                {
                    Id = p.Id.ToString(),
                    ProductName = p.ProductName,
                    ProductPrice = p.ProductPrice,
                    ImageUrl = p.ImageUrl ?? ImageNotFoundUrl,
                    Quantity = p.StockQuantity,
                    isDeleted = p.IsDeleted,
                    AddedOn = p.AddedOn.ToString(ReleaseDateFormat),
                    ManufacturerName = p.Manufacturer?.ManufacturerName,
                    CategoryName = p.Category?.CategoryName
                }).ToList(),
                CurrentPage = page,
                TotalPages = totalPages
            };

            return paginatedViewModel;
        }
    }
}
