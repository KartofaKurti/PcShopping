using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcBuilder.Services.Data;
using PcBuilder.Services.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace PcBuilderWeb.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [Route("api/products")]  
    [ApiController]  
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductApiController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("togglevisibility/{productId}")]
        public async Task<IActionResult> ToggleVisibility(Guid productId)
        {
            var result = await _productService.ToggleProductVisibilityAsync(productId);
            if (result)
            {
                return Ok(new { success = true, message = "Product visibility toggled successfully." });
            }
            return NotFound(new { success = false, message = "Product not found." });
            
        }


        [Authorize(Roles = "ADMIN")]
        [HttpDelete("api/products/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _productService.GetProductDetailsByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            bool deleted = await _productService.HardDeleteProductAsync(id);
            if (!deleted)
            {
                return StatusCode(500, "Error deleting the product.");
            }

            return Ok();
        }
    }
}
