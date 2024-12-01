using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PcBuilder.Data;
using PcBuilder.Data.Models;
using PcBuilder.Data.Repository.Interfaces;
using PcBuilder.Services.Data;
using PcBuilder.Services.Data.Interfaces;
using PcBuilder.Services.Mapping;
using PcBuilder.Web.ViewModels.Cart;
using PcBuilder.Web.ViewModels.Product;
using System.Security.Claims;
using static PcBuilder.Common.DateValidation;
using static PcBuilder.Common.RolesValidation;

namespace PcBuilderWeb.Controllers
{

    [Authorize]
	public class CartController : BaseController
    {
        private readonly ICartService _cartService;
        private readonly UserManager<ApplicationUser> userManager;

		public CartController(ICartService cartService, UserManager<ApplicationUser> userManager)
		{
			this._cartService = cartService;
			this.userManager = userManager;
		}

		[Authorize(Roles = UserRole)]
		[HttpGet]
        public async Task<IActionResult> Index()
        {
	        if (User.IsInRole(AdminRole))
	        {
		        return RedirectToAction("Error", "Home", new { statusCode = 401 });
	        }

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

			var cartItems = await _cartService.GetUserCartByUserIdAsync(userId); 
			var totalPrice = await _cartService.CalculateTotalPriceAsync(userId); 

			var cartViewModel = new CartViewModel
			{
				Items = cartItems.ToList(),
				TotalPrice = totalPrice
			};

			return View(cartViewModel);
		}

        [Authorize(Roles = UserRole)]
		[HttpPost]
        public async Task<IActionResult> AddToCart(string? productId)
        {
            string userId = this.userManager.GetUserId(User)!;
            if (String.IsNullOrEmpty(userId))
            {
                return this.RedirectToAction("Login", "User");
            }

            bool result = await this._cartService
                .AddProductToUserCarttAsync(productId, userId);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = UserRole)]
		[HttpPost]
        public async Task<IActionResult> RemoveFromCart(string? productId)
        {
			string userId = this.userManager.GetUserId(User)!;
			if (String.IsNullOrWhiteSpace(userId))
			{
				return this.RedirectToAction("Login", "User");
			}

			bool result = await this._cartService
				.RemoveProductFromUserCartAsync(productId, userId);
			if (result == false)
			{
				return this.RedirectToAction("Index", "Cart");
			}

			return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(Guid productId, int quantity)
        {
            if (quantity < 1)
            {
                return BadRequest("Quantity must be at least 1.");
            }

            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); 

            var result = await _cartService.UpdateQuantityAsync(productId, userId, quantity);

            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
