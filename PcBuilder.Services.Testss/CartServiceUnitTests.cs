using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PcBuilder.Data;
using PcBuilder.Data.Models;
using PcBuilder.Data.Repository;
using PcBuilder.Services.Data;
using PcBuilder.Web.ViewModels.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[TestFixture]
public class CartServiceTests
{
    private DbContextOptions<PCBuilderDbContext> _dbOptions;
    private PCBuilderDbContext _dbContext;
    private CartService _cartService;

    [SetUp]
    public void SetUp()
    {
        _dbOptions = new DbContextOptionsBuilder<PCBuilderDbContext>()
            .UseInMemoryDatabase("TestCartDatabase" + Guid.NewGuid())
            .Options;

        _dbContext = new PCBuilderDbContext(_dbOptions);
        _dbContext.Database.EnsureCreated();

        var userCartRepository = new BaseRepository<ApplicationUserProduct, object>(_dbContext);
        var productRepository = new BaseRepository<Product, Guid>(_dbContext);
        _cartService = new CartService(userCartRepository, productRepository);

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
        _dbContext.ApplicationUsersProducts.RemoveRange(_dbContext.ApplicationUsersProducts);
        _dbContext.Products.RemoveRange(_dbContext.Products);
        _dbContext.Manufacturers.RemoveRange(_dbContext.Manufacturers);
        _dbContext.Categories.RemoveRange(_dbContext.Categories);
        _dbContext.SaveChanges();

        var manufacturer = new Manufacturer
        {
            Id = 1,
            ManufacturerName = "Test Manufacturer"
        };

        var category = new Category
        {
            Id = 1,
            CategoryName = "Test Category"
        };

        var product1 = new Product
        {
            Id = Guid.NewGuid(),
            ProductName = "Product 1",
            ProductPrice = 50,
            StockQuantity = 10,
            AddedOn = DateTime.UtcNow,
            ManufacturerId = manufacturer.Id,
            CategoryId = category.Id,
            ImageUrl = "http://example.com/product1.jpg",
            ProductDescription = "Description for Product 1",
            IsDeleted = false
        };

        var product2 = new Product
        {
            Id = Guid.NewGuid(),
            ProductName = "Product 2",
            ProductPrice = 100,
            StockQuantity = 5,
            AddedOn = DateTime.UtcNow,
            ManufacturerId = manufacturer.Id,
            CategoryId = category.Id,
            ImageUrl = "http://example.com/product2.jpg",
            ProductDescription = "Description for Product 2",
            IsDeleted = false
        };

        _dbContext.Manufacturers.Add(manufacturer);
        _dbContext.Categories.Add(category);
        _dbContext.Products.AddRange(product1, product2);

        var userCartItem = new ApplicationUserProduct
        {
            ApplicationUserId = Guid.Parse("4e191a73-e8d4-462d-9803-0400812d86cf"),
            ProductId = product1.Id,
            Quantity = 2
        };

        _dbContext.ApplicationUsersProducts.Add(userCartItem);
        _dbContext.SaveChanges();
    }

    [Test]
    public async Task GetUserCartByUserIdAsync_ShouldReturnUserCartItems()
    {
        var userId = "4e191a73-e8d4-462d-9803-0400812d86cf";
        var cartItems = await _cartService.GetUserCartByUserIdAsync(userId);

        Assert.That(cartItems.Count(), Is.EqualTo(1));
        var cartItem = cartItems.First();
        Assert.That(cartItem.ProductName, Is.EqualTo("Product 1"));
        Assert.That(cartItem.Quantity, Is.EqualTo(2));
    }

    [Test]
    public async Task AddProductToUserCarttAsync_ShouldAddNewProductToCart()
    {
        var productId = _dbContext.Products.First(p => p.ProductName == "Product 2").Id.ToString();
        var userId = "4e191a73-e8d4-462d-9803-0400812d86cf";

        var result = await _cartService.AddProductToUserCarttAsync(productId, userId);

        Assert.IsTrue(result);

        var userCart = await _cartService.GetUserCartByUserIdAsync(userId);
        Assert.That(userCart.Count(), Is.EqualTo(2));
        Assert.IsTrue(userCart.Any(c => c.ProductName == "Product 2"));
    }

    [Test]
    public async Task AddProductToUserCarttAsync_ShouldIncreaseQuantityIfProductAlreadyInCart()
    {
        var productId = _dbContext.Products.First(p => p.ProductName == "Product 1").Id.ToString();
        var userId = "4e191a73-e8d4-462d-9803-0400812d86cf";

        var result = await _cartService.AddProductToUserCarttAsync(productId, userId);

        Assert.IsTrue(result);

        var userCart = await _cartService.GetUserCartByUserIdAsync(userId);
        Assert.That(userCart.First(c => c.ProductName == "Product 1").Quantity, Is.EqualTo(3));
    }

    [Test]
    public async Task AddProductToUserCarttAsync_ShouldFailIfStockIsInsufficient()
    {
        var productId = _dbContext.Products.First(p => p.ProductName == "Product 2").Id.ToString();
        var userId = "4e191a73-e8d4-462d-9803-0400812d86cf";

        for (int i = 0; i < 6; i++)
        {
            await _cartService.AddProductToUserCarttAsync(productId, userId);
        }

        var result = await _cartService.AddProductToUserCarttAsync(productId, userId);

        Assert.IsFalse(result);
    }

    [Test]
    public async Task RemoveProductFromUserCartAsync_ShouldRemoveProductFromCart()
    {
        var productId = _dbContext.Products.First(p => p.ProductName == "Product 1").Id.ToString();
        var userId = "4e191a73-e8d4-462d-9803-0400812d86cf";

        var result = await _cartService.RemoveProductFromUserCartAsync(productId, userId);

        Assert.IsTrue(result);

        var userCart = await _cartService.GetUserCartByUserIdAsync(userId);
        Assert.IsFalse(userCart.Any(c => c.ProductName == "Product 1"));
    }

    [Test]
    public async Task CalculateTotalPriceAsync_ShouldReturnCorrectTotal()
    {
        var userId = "4e191a73-e8d4-462d-9803-0400812d86cf";

        var totalPrice = await _cartService.CalculateTotalPriceAsync(userId);

        Assert.AreEqual(100, totalPrice);
    }

    [Test]
    public async Task ClearUserCartAsync_ShouldRemoveAllItemsFromCart()
    {
        var userId = "4e191a73-e8d4-462d-9803-0400812d86cf";

        await _cartService.ClearUserCartAsync(userId);

        var userCart = await _cartService.GetUserCartByUserIdAsync(userId);

        Assert.IsEmpty(userCart);
    }

    [Test]
    public async Task UpdateQuantityAsync_ShouldUpdateProductQuantity()
    {
        var productId = _dbContext.Products.First(p => p.ProductName == "Product 1").Id;
        var userId = Guid.Parse("4e191a73-e8d4-462d-9803-0400812d86cf");

        var result = await _cartService.UpdateQuantityAsync(productId, userId, 5);

        Assert.IsTrue(result.Success);

        var userCart = await _cartService.GetUserCartByUserIdAsync(userId.ToString());
        Assert.AreEqual(5, userCart.First(c => c.ProductName == "Product 1").Quantity);
    }

    [Test]
    public async Task UpdateQuantityAsync_ShouldFailIfQuantityExceedsStock()
    {
        var productId = _dbContext.Products.First(p => p.ProductName == "Product 1").Id;
        var userId = Guid.Parse("4e191a73-e8d4-462d-9803-0400812d86cf");

        var result = await _cartService.UpdateQuantityAsync(productId, userId, 15);

        Assert.IsFalse(result.Success);
    }

    [Test]
    public async Task UpdateQuantityAsync_ShouldFailForNonExistentCartItem()
    {
        var productId = Guid.NewGuid();
        var userId = Guid.Parse("4e191a73-e8d4-462d-9803-0400812d86cf");

        var result = await _cartService.UpdateQuantityAsync(productId, userId, 1);

        Assert.IsFalse(result.Success);
    }
}
