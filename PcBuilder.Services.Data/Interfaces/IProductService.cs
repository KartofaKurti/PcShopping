using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcBuilder.Web.ViewModels.Product;

namespace PcBuilder.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<AllProductsIndexViewModel>> GetAllProductsAsync();

        Task<bool> AddProductAsync(AddProductViewModel  product);
        Task<ProductDetailsViewModel?> GetProductDetailsByIdAsync(Guid id);

    }
}
