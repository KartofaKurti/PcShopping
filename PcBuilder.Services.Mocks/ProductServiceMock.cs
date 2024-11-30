using Moq;
using PcBuilder.Services.Data.Interfaces;
using PcBuilder.Web.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PcBuilder.Services.Mocks
{
    public class ProductServiceMock
    {
        public static IProductService Instance
        {
            get
            {
                var productServiceMock = new Mock<IProductService>();

                productServiceMock
                    .Setup(p => p.GetAllProductsAsync())
                    .ReturnsAsync(new List<AllProductsIndexViewModel>
                    {
                        new AllProductsIndexViewModel
                        {
                            Id = Guid.NewGuid().ToString(),
                            ProductName = "Mock Product",
                            ProductPrice = 100,
                            ManufacturerName = "Mock Manufacturer",
                            CategoryName = "Mock Category",
                            AddedOn = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                            ImageUrl = "http://example.com",
                            Quantity = 10,
                            isDeleted = false
                        }
                    });

                productServiceMock
                    .Setup(p => p.AddProductAsync(It.IsAny<AddProductViewModel>()))
                    .ReturnsAsync(true);

                productServiceMock
                    .Setup(p => p.EditProductAsync(It.IsAny<EditProductViewModel>()))
                    .ReturnsAsync(true);

                productServiceMock
                    .Setup(p => p.HardDeleteProductAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(true);

                productServiceMock
                    .Setup(p => p.ToggleProductVisibilityAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(true);

                return productServiceMock.Object;
            }
        }
    }
}
