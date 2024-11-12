using Microsoft.AspNetCore.Identity;
using PcBuilder.Data.Models;
using PcBuilder.Data.Repository.Interfaces;
using PcBuilder.Services.Data.Interfaces;
using PcBuilder.Services.Data;
using PcBuilder.Web.ViewModels.Cart;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Web.ViewModels.Order;

public class OrderService : BaseService, IOrderService
{
	private readonly IRepository<Order, Guid> _orderRepository;
	private readonly IRepository<Product, Guid> _productRepository;
	private readonly IRepository<AplicationUserOrder, object> _usersOrdersRepository;
	private readonly IRepository<OrderProduct, Guid> _orderItemRepository; 

	public OrderService(
		IRepository<Order, Guid> orderRepository,
		IRepository<Product, Guid> productRepository,
		IRepository<AplicationUserOrder, object> usersOrdersRepository,
		IRepository<OrderProduct, Guid> orderItemRepository) 
	{
		_orderRepository = orderRepository;
		_productRepository = productRepository;
		_usersOrdersRepository = usersOrdersRepository;
		_orderItemRepository = orderItemRepository; 
	}

	public async Task<bool> CreateOrderAsync(string userId, IEnumerable<ApplicationUserCartViewModel> cartItems, string address)
	{
		try
		{
			var userGuid = Guid.Parse(userId);
			var orderGuid = Guid.NewGuid();
			var totalPrice = cartItems.Sum(item => item.ProductPrice * item.Quantity);

			var order = new Order
			{
				Id = orderGuid,
				OrderDate = DateTime.Now,
				ProductQuantity = cartItems.Sum(item => item.Quantity),
				TotalPrice = totalPrice,
				Address = address
			};

			await _orderRepository.AddAsync(order);

			foreach (var item in cartItems)
			{
				var orderItem = new OrderProduct()
				{
					OrderId = orderGuid,
					ProductId = Guid.Parse(item.ProductId),
					Quantity = item.Quantity,
					Price = item.ProductPrice,
					ProductName = item.ProductName,
				};

				await _orderItemRepository.AddAsync(orderItem);

				var product = await _productRepository.GetByIdAsync(Guid.Parse(item.ProductId));
				if (product != null)
				{
					product.StockQuantity -= item.Quantity; 
					await _productRepository.UpdateAsync(product); 
				}
			}


			var userOrder = new AplicationUserOrder
			{
				ApplicationUserId = userGuid,
				OrderId = orderGuid
			};

			await _usersOrdersRepository.AddAsync(userOrder);


			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error creating order: {ex.Message}");
			return false;
		}
	}

	public async Task<IEnumerable<OrderDetailsViewModel>> GetAllOrdersAsync()
	{
        var userOrders = await _usersOrdersRepository
            .GetAllAttached()
            .Include(uo => uo.ApplicationUser)  
            .Include(uo => uo.Order)
            .ThenInclude(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .ToListAsync();

        return userOrders.Select(uo => new OrderDetailsViewModel
        {
            OrderId = uo.Order.Id,
            OrderDate = uo.Order.OrderDate,
            ProductQuantity = uo.Order.ProductQuantity,
            TotalPrice = uo.Order.TotalPrice,
            Address = uo.Order.Address,
            UserName = uo.ApplicationUser.UserName,  
            OrderItems = uo.Order.OrderProducts.Select(op => new OrderItemViewModel
            {
                ProductName = op.ProductName,
                ProductPrice = op.Price,
                Quantity = op.Quantity
            }).ToList()
        }).ToList(); ;
	}

	public async Task<IEnumerable<OrderViewModel>> GetOrdersByUserAsync(string userId)
	{
		var userGuid = Guid.Parse(userId);

		var userOrders = await _usersOrdersRepository
			.GetAllAttached()
			.Where(uo => uo.ApplicationUserId == userGuid)  
			.Include(uo => uo.Order)  
			.ThenInclude(o => o.OrderProducts)  
			.ThenInclude(op => op.Product)  
			.ToListAsync();

		
		return userOrders.Select(uo => new OrderViewModel
		{
			OrderId = uo.Order.Id.ToString(),
			OrderDate = uo.Order.OrderDate.ToString("yyyy-MM-dd"),
			Address = uo.Order.Address,
			TotalPrice = uo.Order.TotalPrice,
			ProductQuantity = uo.Order.ProductQuantity,
			CartItems = uo.Order.OrderProducts.Select(op => new ApplicationUserCartViewModel
			{
				ProductId = op.ProductId.ToString(),
				ProductName = op.ProductName,
				ProductPrice = op.Price,
				Quantity = op.Quantity,
				Category = op.Product.Category.ToString(),
				Manufacturer = op.Product.Manufacturer.ToString(),
				ImageUrl = op.Product.ImageUrl
			}).ToList()
		});

	}

    public async Task<OrderDetailsViewModel> GetOrderByIdAsync(Guid orderId)
    {
        var order = await _orderRepository
            .GetAllAttached()
            .Where(o => o.Id == orderId)  
            .Include(o => o.OrderProducts)  
            .ThenInclude(op => op.Product)  
            .FirstOrDefaultAsync();

        if (order == null)
        {
            return null; 
        }

    
        var orderDetailsViewModel = new OrderDetailsViewModel
        {
            OrderId = order.Id,
            OrderDate = order.OrderDate,
            TotalPrice = order.TotalPrice,
            Address = order.Address,
            ProductQuantity = order.ProductQuantity,
            OrderItems = order.OrderProducts.Select(op => new OrderItemViewModel
            {
                ProductName = op.Product.ProductName,
                ProductPrice = op.Price,
                Quantity = op.Quantity
            }).ToList()
        };

        return orderDetailsViewModel;
    }
}
