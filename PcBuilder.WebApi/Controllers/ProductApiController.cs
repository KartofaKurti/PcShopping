using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcBuilder.Data.Models;
using PcBuilder.Data.Models.Enums;
using PcBuilder.Services.Data;
using PcBuilder.Services.Data.Interfaces;
using PcBuilder.Web.ViewModels.Product;

[ApiController]
[Authorize(Roles = "ADMIN")]
[Route("api/[controller]")]
public class ProductApiController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductApiController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("togglevisibility/{productId}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ToggleVisibility(Guid productId)
    {
        var result = await _productService.ToggleProductVisibilityAsync(productId);
        if (result)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            return Ok(new
            {
                success = true, isDeleted = product.IsDeleted, message = "Product visibility toggled successfully."
            });
        }

        return NotFound(new { success = false, message = "Product not found." });
    }


    [HttpDelete("DeleteProduct/{productId:guid}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        bool deleted = await _productService.HardDeleteProductAsync(productId);
        if (!deleted)
        {
            return StatusCode(500, new { success = false, message = "Error deleting the product." });
        }

        return Ok(new { success = true, message = "Product deleted successfully." });
    }

    [HttpGet("GetFilters")]
    public IActionResult GetFilters()
    {
        var manufacturers = Enum.GetValues(typeof(ManufacturerType))
            .Cast<ManufacturerType>()
            .Select(e => new { Id = (int)e, Name = e.ToString() });

        var categories = Enum.GetValues(typeof(CategoryType))
            .Cast<CategoryType>()
            .Select(e => new { Id = (int)e, Name = e.ToString() });

        return Ok(new
        {
            Manufacturers = manufacturers,
            Categories = categories
        });
    }

}
