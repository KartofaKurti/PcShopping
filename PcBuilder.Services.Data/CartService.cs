using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Models;
using PcBuilder.Data.Repository.Interfaces;
using PcBuilder.Services.Data.Interfaces;
using PcBuilder.Web.ViewModels.Cart;
using static PcBuilder.Common.DateValidation;
using static PcBuilder.Common.ProductValidation;

namespace PcBuilder.Services.Data
{

	public class CartService : BaseService, ICartService
    {
        private readonly IRepository<ApplicationUserProduct, object> _userCartRepository;
        private readonly IRepository<Product, Guid> _productRepository;

        public CartService(IRepository<ApplicationUserProduct, object> cartRepository, IRepository<Product, Guid> productRepository)
        {
            this._userCartRepository = cartRepository;
            this._productRepository = productRepository;
        }
        public async Task<IEnumerable<ApplicationUserCartViewModel>> GetUserCartByUserIdAsync(string userId)
        {
			Guid userGuid = Guid.Parse(userId);

			var cartItems = await _userCartRepository
				.GetAllAttached() 
				.Where(c => c.ApplicationUserId == userGuid) 
				.Include(c => c.Product)
				.ThenInclude(p => p.Manufacturer) 
				.Include(c => c.Product.Category) 
				.ToListAsync();

		
			var cartViewModels = cartItems.Select(cartItem => new ApplicationUserCartViewModel
			{
				ProductId = cartItem.Product.Id.ToString(),
				ProductName = cartItem.Product.ProductName,
				ProductPrice = cartItem.Product.ProductPrice,
				AddedOn = cartItem.Product.AddedOn.ToString(ReleaseDateFormat),
				Manufacturer = cartItem.Product.Manufacturer?.ManufacturerName ?? "Unknown",
				ImageUrl = cartItem.Product.ImageUrl,
				Category = cartItem.Product.Category?.CategoryName ?? "Unknown",
				Quantity = cartItem.Quantity
			}).ToList();

			return cartViewModels;
		}

        public async Task<bool> AddProductToUserCarttAsync(string? productId, string userId)
        {
			Guid productGuid = Guid.Empty;
			if (!this.IsGuidValid(productId, ref productGuid))
			{
				return false;
			}

			Product? product = await this._productRepository
				.GetByIdAsync(productGuid);
			if (product == null)
			{
				return false;
			}

			Guid userGuid = Guid.Parse(userId);

		
			ApplicationUserProduct? addedToCartAlready = await this._userCartRepository
				.FirstOrDefaultAsync(um => um.ProductId == productGuid &&
				                           um.ApplicationUserId == userGuid);

			if (addedToCartAlready == null)
			{
				ApplicationUserProduct newUserProduct = new ApplicationUserProduct()
				{
					ApplicationUserId = userGuid,
					ProductId = productGuid,
					Quantity = 1
				};

				await this._userCartRepository.AddAsync(newUserProduct);
			}
			else
			{
				addedToCartAlready.Quantity += 1;
				await this._userCartRepository.UpdateAsync(addedToCartAlready);
			}

			return true;
		}

        public async Task<bool> RemoveProductFromUserCartAsync(string? productId, string userId)
        {
	        Guid productGuid = Guid.Empty;
	        if (!this.IsGuidValid(productId, ref productGuid))
	        {
		        return false;
	        }

	        Product? product = await this._productRepository
		        .GetByIdAsync(productGuid);
	        if (product == null)
	        {
		        return false;
	        }

	        Guid userGuid = Guid.Parse(userId);

	        ApplicationUserProduct? applicationUserProduct = await this._userCartRepository
		        .FirstOrDefaultAsync(uc => uc.ProductId == productGuid &&
		                                   uc.ApplicationUserId == userGuid);

	        if (applicationUserProduct != null)
	        {
		        
		        await this._userCartRepository.DeleteAsync(applicationUserProduct);
	        }

	        return true;
		}

        public async Task<decimal> CalculateTotalPriceAsync(string userId)
        {
			Guid userGuid = Guid.Parse(userId);

			var cartItems = await _userCartRepository
				.GetAllAttached()
				.Where(c => c.ApplicationUserId == userGuid)
				.Include(c => c.Product)
				.ToListAsync();

			decimal totalPrice = cartItems.Sum(ci => ci.Product.ProductPrice * ci.Quantity);

			return totalPrice;
		}

        public async Task ClearUserCartAsync(string userId)
        {
	        if (!Guid.TryParse(userId, out Guid userGuid))
	        {
		        return; 
	        }

	       
	        var userCartItems = await _userCartRepository
		        .GetAllAttached()
		        .Where(c => c.ApplicationUserId == userGuid)
		        .ToListAsync();  
	       
	        if (userCartItems.Any())
	        {
		        foreach (var item in userCartItems)
		        {
			        await _userCartRepository.DeleteAsync(item);
		        }
	        }
		}
    }
}
