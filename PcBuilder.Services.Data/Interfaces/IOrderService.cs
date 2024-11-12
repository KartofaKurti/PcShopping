using PcBuilder.Data.Models;
using PcBuilder.Web.ViewModels.Cart;
using PcBuilder.Web.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Services.Data.Interfaces
{
	public interface IOrderService
	{
		Task<bool> CreateOrderAsync(string userId, IEnumerable<ApplicationUserCartViewModel> cartItems, string address);
		Task<IEnumerable<OrderViewModel>> GetOrdersByUserAsync(string userId);
		Task<IEnumerable<OrderDetailsViewModel>> GetAllOrdersAsync();
        public Task<OrderDetailsViewModel> GetOrderByIdAsync(Guid orderId);

    }
}
