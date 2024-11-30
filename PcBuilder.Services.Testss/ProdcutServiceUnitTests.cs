using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PcBuilder.Data;
using PcBuilder.Data.Models;
using PcBuilder.Data.Repository;
using PcBuilder.Services.Data;
using PcBuilder.Web.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PcBuilder.Common.DateValidation;

[TestFixture]
public class ProductServiceTests
{
    private DbContextOptions<PCBuilderDbContext> _dbOptions;
    private PCBuilderDbContext _dbContext;
    private ProductService _productService;

    [SetUp]
    public void SetUp()
    {
        _dbOptions = new DbContextOptionsBuilder<PCBuilderDbContext>()
            .UseInMemoryDatabase("TestDatabase" + Guid.NewGuid())
            .Options;

        _dbContext = new PCBuilderDbContext(_dbOptions);
        _dbContext.Database.EnsureCreated();

        var productRepository = new BaseRepository<Product, Guid>(_dbContext);
        _productService = new ProductService(productRepository);

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
        _dbContext.SaveChanges();

        var manufacturer = new Manufacturer { Id = 1, ManufacturerName = "Test Manufacturer" };
        var category = new Category { Id = 1, CategoryName = "Test Category" };

        _dbContext.Manufacturers.Add(manufacturer);
        _dbContext.Categories.Add(category);

        _dbContext.Products.Add(new Product
        {
            Id = Guid.NewGuid(),
            ProductName = "Test Product",
            ProductPrice = 100,
            StockQuantity = 10,
            AddedOn = DateTime.Today,
            ProductDescription = "Test Description",
            ManufacturerId = manufacturer.Id,
            CategoryId = category.Id,
            ImageUrl = "http://example.com/product.jpg",
            IsDeleted = false
        });

        _dbContext.SaveChanges();
    }

    [Test]
    public async Task GetAllProductsAsync_ShouldReturnProducts()
    {
        var result = await _productService.GetAllProductsAsync();

        Assert.That(result.Count(), Is.EqualTo(1));
        var product = result.First();
        Assert.That(product.ProductName, Is.EqualTo("Test Product"));
        Assert.That(product.ProductPrice, Is.EqualTo(100));
    }

    [Test]
    public async Task AddProductAsync_ShouldAddProduct()
    {
        var model = new AddProductViewModel
        {
            ProductName = "Valid Product",
            ProductPrice = 200,
            StockQuantity = 20,
            AddedOn = DateTime.UtcNow.ToString(ReleaseDateFormat),
            ProductDescription = "A valid description.",
            ImageUrl = "http://validurl.com",
            ManufacturerId = 1,
            CategoryId = 1
        };

        var result = await _productService.AddProductAsync(model);

        Assert.IsTrue(result);

        var products = await _dbContext.Products.ToListAsync();
        Assert.AreEqual(2, products.Count);
        Assert.IsTrue(products.Any(p => p.ProductName == "Valid Product"));
    }

    [Test]
    public async Task AddProductAsync_WithInvalidDate_ShouldFail()
    {
        var model = new AddProductViewModel
        {
            ProductName = "Invalid Date Product",
            ProductPrice = 150,
            StockQuantity = 10,
            AddedOn = "Invalid Date",
            ProductDescription = "Invalid date description.",
            ManufacturerId = 1,
            CategoryId = 1
        };

        var result = await _productService.AddProductAsync(model);

        Assert.IsFalse(result);
    }

    [Test]
    public async Task AddProductAsync_WithInvalidPrice_ShouldFail()
    {
        var model = new AddProductViewModel
        {
            ProductName = "Negative Price Product",
            ProductPrice = -10,
            StockQuantity = 5,
            AddedOn = DateTime.UtcNow.ToString(ReleaseDateFormat),
            ProductDescription = "Negative price description.",
            ManufacturerId = 1,
            CategoryId = 1
        };

        var result = await _productService.AddProductAsync(model);

        Assert.IsFalse(result);
    }

    [Test]
    public async Task HardDeleteProductAsync_ShouldDeleteProduct()
    {
        var productId = _dbContext.Products.First().Id;

        var result = await _productService.HardDeleteProductAsync(productId);

        Assert.IsTrue(result);

        var deletedProduct = await _dbContext.Products.FindAsync(productId);
        Assert.IsNull(deletedProduct);
    }

    [Test]
    public async Task HardDeleteProductAsync_WithInvalidId_ShouldFail()
    {
        var invalidProductId = Guid.NewGuid();

        var result = await _productService.HardDeleteProductAsync(invalidProductId);

        Assert.IsFalse(result);
    }

    [Test]
    public async Task ToggleProductVisibilityAsync_ShouldToggleIsDeletedFlag()
    {
        var productId = _dbContext.Products.First().Id;

        var result = await _productService.ToggleProductVisibilityAsync(productId);

        Assert.IsTrue(result);

        var updatedProduct = await _dbContext.Products.FindAsync(productId);
        Assert.IsTrue(updatedProduct.IsDeleted);

        result = await _productService.ToggleProductVisibilityAsync(productId);

        Assert.IsTrue(result);
        updatedProduct = await _dbContext.Products.FindAsync(productId);
        Assert.IsFalse(updatedProduct.IsDeleted);
    }

    [Test]
    public async Task EditProductAsync_ShouldUpdateProductDetails()
    {
        var productId = _dbContext.Products.First().Id;

        var editModel = new EditProductViewModel
        {
            Id = productId.ToString(),
            ProductName = "Updated Product",
            ProductDescription = "Updated Description",
            ProductPrice = 120,
            StockQuantity = 15,
            ImageUrl = "http://example.com/updated-product.jpg"
        };

        var result = await _productService.EditProductAsync(editModel);

        Assert.IsTrue(result);

        var updatedProduct = await _dbContext.Products.FindAsync(productId);
        Assert.AreEqual("Updated Product", updatedProduct.ProductName);
        Assert.AreEqual(120, updatedProduct.ProductPrice);
    }

    [Test]
    public async Task EditProductAsync_WithInvalidId_ShouldFail()
    {
        var invalidEditModel = new EditProductViewModel
        {
            Id = Guid.NewGuid().ToString(),
            ProductName = "Invalid Product",
            ProductDescription = "Invalid Description",
            ProductPrice = 100,
            StockQuantity = 10,
            ImageUrl = "http://example.com/invalid-product.jpg"
        };

        var result = await _productService.EditProductAsync(invalidEditModel);

        Assert.IsFalse(result);
    }
}
