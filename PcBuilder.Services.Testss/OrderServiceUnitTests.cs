using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PcBuilder.Data;
using PcBuilder.Data.Models;
using PcBuilder.Data.Repository;
using PcBuilder.Services.Data;
using PcBuilder.Web.ViewModels.Cart;
using PcBuilder.Web.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[TestFixture]
public class OrderServiceTests
{
    private DbContextOptions<PCBuilderDbContext> _dbOptions;
    private PCBuilderDbContext _dbContext;
    private OrderService _orderService;

    [SetUp]
    public void SetUp()
    {
        _dbOptions = new DbContextOptionsBuilder<PCBuilderDbContext>()
            .UseInMemoryDatabase("TestOrderDatabase" + Guid.NewGuid())
            .Options;

        _dbContext = new PCBuilderDbContext(_dbOptions);
        _dbContext.Database.EnsureCreated();

        var orderRepository = new BaseRepository<Order, Guid>(_dbContext);
        var productRepository = new BaseRepository<Product, Guid>(_dbContext);
        var usersOrdersRepository = new BaseRepository<AplicationUserOrder, object>(_dbContext);
        var orderItemRepository = new BaseRepository<OrderProduct, Guid>(_dbContext);

        _orderService = new OrderService(orderRepository, productRepository, usersOrdersRepository, orderItemRepository);

        SeedDatabase();
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }

    private void SeedDatabase()
    {
        _dbContext.Products.RemoveRange(_dbContext.Products);
        _dbContext.Manufacturers.RemoveRange(_dbContext.Manufacturers);
        _dbContext.Categories.RemoveRange(_dbContext.Categories);
        _dbContext.Orders.RemoveRange(_dbContext.Orders);
        _dbContext.OrderProducts.RemoveRange(_dbContext.OrderProducts);
        _dbContext.AplicationUsersOrders.RemoveRange(_dbContext.AplicationUsersOrders);
        _dbContext.SaveChanges();

        // Add unique Manufacturers and Categories
        var manufacturer = new Manufacturer { Id = 1, ManufacturerName = "Test Manufacturer" };
        var category = new Category { Id = 1, CategoryName = "Test Category" };

        if (!_dbContext.Manufacturers.Any(m => m.Id == manufacturer.Id))
        {
            _dbContext.Manufacturers.Add(manufacturer);
        }

        if (!_dbContext.Categories.Any(c => c.Id == category.Id))
        {
            _dbContext.Categories.Add(category);
        }

        // Add unique Products
        var productId = Guid.NewGuid();
        if (!_dbContext.Products.Any(p => p.Id == productId))
        {
            _dbContext.Products.Add(new Product
            {
                Id = productId,
                ProductName = "Test Product",
                ProductPrice = 100,
                StockQuantity = 10,
                AddedOn = DateTime.UtcNow,
                ProductDescription = "Description for Test Product",
                ManufacturerId = manufacturer.Id,
                CategoryId = category.Id,
                ImageUrl = "http://example.com/test-product.jpg",
                IsDeleted = false
            });
        }

        _dbContext.SaveChanges();
    }

    [Test]
    public async Task CreateOrderAsync_ShouldCreateOrderSuccessfully()
    {
        var userId = Guid.NewGuid().ToString();
        var product = _dbContext.Products.First();

        var cartItems = new List<ApplicationUserCartViewModel>
        {
            new ApplicationUserCartViewModel
            {
                ProductId = product.Id.ToString(),
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                Quantity = 2
            }
        };

        var address = "123 Test Street";

        var result = await _orderService.CreateOrderAsync(userId, cartItems, address);

        Assert.IsTrue(result);

        var orders = await _dbContext.Orders.ToListAsync();
        Assert.AreEqual(1, orders.Count);
        Assert.AreEqual(cartItems.Sum(c => c.Quantity), orders.First().ProductQuantity);
    }

    [Test]
    public async Task GetAllOrdersAsync_ShouldReturnAllOrders()
    {
        var orderId = Guid.NewGuid();
        var userId = Guid.NewGuid();

      
        var order = new Order
        {
            Id = orderId,
            OrderDate = DateTime.UtcNow,
            ProductQuantity = 2,
            TotalPrice = 100,
            Address = "123 Test Street",
            OrderProducts = new List<OrderProduct> 
            {
                new OrderProduct
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Test Product",
                    Price = 50,
                    Quantity = 2
                }
            }
        };

       
        var userOrder = new AplicationUserOrder
        {
            ApplicationUserId = userId,
            OrderId = orderId,
            ApplicationUser = new ApplicationUser
            {
                Id = userId,
                UserName = "TestUser"
            },
            Order = order
        };

        
        _dbContext.Orders.Add(order);
        _dbContext.AplicationUsersOrders.Add(userOrder);
        await _dbContext.SaveChangesAsync(); 

        
        var orders = await _orderService.GetAllOrdersAsync();

        
        Assert.AreEqual(1, orders.Count());
        Assert.AreEqual(orderId, orders.First().OrderId);
        Assert.AreEqual("TestUser", orders.First().UserName);
        Assert.AreEqual("123 Test Street", orders.First().Address);
        Assert.AreEqual(2, orders.First().ProductQuantity);
        Assert.AreEqual(100, orders.First().TotalPrice);
    }

    [Test]
    public async Task GetOrdersByUserAsync_ShouldReturnOrdersForUser()
    {
        var userId = Guid.NewGuid();
        var orderId = Guid.NewGuid();

        var order = new Order
        {
            Id = orderId,
            OrderDate = DateTime.UtcNow,
            ProductQuantity = 1,
            TotalPrice = 50,
            Address = "Test Address"
        };

        var userOrder = new AplicationUserOrder
        {
            ApplicationUserId = userId,
            OrderId = orderId
        };

        _dbContext.Orders.Add(order);
        _dbContext.AplicationUsersOrders.Add(userOrder);
        _dbContext.SaveChanges();

        var result = await _orderService.GetOrdersByUserAsync(userId.ToString());

        Assert.AreEqual(1, result.Count());
        Assert.AreEqual(orderId.ToString(), result.First().OrderId);
    }

    [Test]
    public async Task GetOrderByIdAsync_ShouldReturnOrderDetails()
    {
        var orderId = Guid.NewGuid();
        var product = _dbContext.Products.First();

        var order = new Order
        {
            Id = orderId,
            OrderDate = DateTime.UtcNow,
            ProductQuantity = 2,
            TotalPrice = product.ProductPrice * 2,
            Address = "123 Test Street"
        };

        var orderItem = new OrderProduct
        {
            OrderId = orderId,
            ProductId = product.Id,
            Quantity = 2,
            Price = product.ProductPrice,
            ProductName = product.ProductName
        };

        _dbContext.Orders.Add(order);
        _dbContext.OrderProducts.Add(orderItem);
        _dbContext.SaveChanges();

        var result = await _orderService.GetOrderByIdAsync(orderId);

        Assert.IsNotNull(result);
        Assert.AreEqual(orderId, result.OrderId);
        Assert.AreEqual(orderItem.ProductName, result.OrderItems.First().ProductName);
    }

    [Test]
    public async Task CreateOrderAsync_ShouldFailForInsufficientStock()
    {
        var userId = Guid.NewGuid().ToString();
        var product = _dbContext.Products.First();
        product.StockQuantity = 1;

        await _dbContext.SaveChangesAsync();

        var cartItems = new List<ApplicationUserCartViewModel>
        {
            new ApplicationUserCartViewModel
            {
                ProductId = product.Id.ToString(),
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                Quantity = 2
            }
        };

        var address = "123 Test Street";

        var result = await _orderService.CreateOrderAsync(userId, cartItems, address);

        Assert.IsFalse(result);
    }
}
