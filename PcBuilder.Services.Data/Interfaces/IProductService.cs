using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcBuilder.Data.Models;
using PcBuilder.Web.ViewModels.Product;

namespace PcBuilder.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<AllProductsIndexViewModel>> GetAllProductsAsync();

        Task<bool> AddProductAsync(AddProductViewModel  product);
        Task<ProductDetailsViewModel?> GetProductDetailsByIdAsync(Guid id);
        public Task<IEnumerable<Product>> GetAvailableProductsAsync();
        Task<bool> ToggleProductVisibilityAsync(Guid productId);
        public Task<bool> HardDeleteProductAsync(Guid id);
        public Task<bool> EditProductAsync(EditProductViewModel model);
        public Task<Product?> GetProductByIdAsync(Guid id);
        public Task<PaginatedProductsViewModel> SearchProductsAsync(string? name,
	        int? manufacturerId,
	        int? categoryId,
	        decimal? minPrice,
	        decimal? maxPrice,
	        int page = 1,
	        int pageSize = 18);
    }
}
