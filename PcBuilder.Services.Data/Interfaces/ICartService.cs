using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcBuilder.Web.ViewModels.Cart;

namespace PcBuilder.Services.Data.Interfaces
{
	public interface ICartService
	{
        Task<IEnumerable<ApplicationUserCartViewModel>> GetUserCartByUserIdAsync(string userId);

        Task<bool> AddProductToUserCarttAsync(string? movieId, string userId);

        Task<bool> RemoveProductFromUserCartAsync(string? movieId, string userId);

        Task<decimal> CalculateTotalPriceAsync(string userId);

        Task ClearUserCartAsync(string userId);
        public Task<(bool Success, string Message)> UpdateQuantityAsync(Guid productId, Guid userId, int quantity);

    }
}
