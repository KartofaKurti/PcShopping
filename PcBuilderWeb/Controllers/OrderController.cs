using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PcBuilder.Data.Models;
using PcBuilder.Data.Repository.Interfaces;
using PcBuilder.Services.Data;
using PcBuilder.Services.Data.Interfaces;
using PcBuilder.Web.ViewModels.Cart;
using PcBuilder.Web.ViewModels.Order;
using PcBuilder.Web.ViewModels.Product;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static PcBuilder.Common.DateValidation;
using static PcBuilder.Common.RolesValidation;

namespace PcBuilderWeb.Controllers
{
    [Authorize]
    public class OrderController : Controller
	{
		private readonly ICartService _cartService;
		private readonly IOrderService _orderService;

		public OrderController(ICartService cartService,
            IOrderService orderRepository)
		{
			_cartService = cartService;
			_orderService = orderRepository;
		}

        [Authorize(Roles = AdminRole)]
        [HttpGet]
		public async Task<IActionResult> Index()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var orders = await _orderService.GetAllOrdersAsync();
			
			return View(orders);
		}

        [Authorize(Roles = AdminRole)]
        [HttpGet]
        public async Task<IActionResult> OrderDetails(Guid orderId)
        {
            var orderDetails = await _orderService.GetOrderByIdAsync(orderId);

            if (orderDetails == null)
            {
                return NotFound();
            }

            return View(orderDetails); 
        }

        [Authorize(Roles = UserRole)]
        [HttpGet]
		public async Task<IActionResult> CreateOrder()
		{

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var cartItems = await _cartService.GetUserCartByUserIdAsync(userId);
			var totalPrice = await _cartService.CalculateTotalPriceAsync(userId);

			var orderViewModel = new OrderViewModel
			{
				CartItems = cartItems,
				TotalPrice = totalPrice,
				Address = string.Empty,  
				ProductQuantity = cartItems.Count(),
			};

			return View(orderViewModel);
		}

		[Authorize(Roles = UserRole)]
		[HttpPost]
		public async Task<IActionResult> CreateOrder(OrderViewModel orderViewModel)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var cartItems = await _cartService.GetUserCartByUserIdAsync(userId);
			
			if (cartItems == null || !cartItems.Any())
			{
				ModelState.AddModelError("", "Your cart is empty. Please add items to the cart before placing an order.");
				return View(orderViewModel);
			}

			orderViewModel.CartItems = cartItems;
			orderViewModel.TotalPrice = await _cartService.CalculateTotalPriceAsync(userId);
			orderViewModel.ProductQuantity = cartItems.Count();

			var result = await _orderService.CreateOrderAsync(userId, cartItems, orderViewModel.Address);

			if (!result)
			{
		
				ModelState.AddModelError("", "Failed to create the order. Please try again.");
				return View(orderViewModel);
			}

			await _cartService.ClearUserCartAsync(userId);

			return RedirectToAction("Index", "Cart");
		}


	}
}
