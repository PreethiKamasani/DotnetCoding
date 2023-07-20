using Microsoft.AspNetCore.Mvc;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;
using DotnetCoding.Services.Contracts;

namespace DotnetCoding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get the list of product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            var productDetailsList = await _productService.GetAllProducts();
            if(productDetailsList == null)
            {
                return NotFound();
            }
            return Ok(productDetailsList);
        }

        [HttpPost("Product")]
        public async Task<IActionResult> CreateProduct(ProductRequest request)
        {

            var productDetails = await _productService.CreateProduct(request);
            if (productDetails == null)
            {
                return NotFound();
            }
            return StatusCode(201,productDetails);
        }

        [HttpPut("Product/Id")]
        public async Task<IActionResult> UpdateProduct([FromBody]ProductRequest request,[FromQuery] int productId)
        {

            var productDetails = await _productService.UpdateProduct(request,productId);
            if (productDetails == null)
            {
                return NotFound();
            }
            return Ok(productDetails);
        }

        [HttpDelete("Product/Id")]
        public async Task<IActionResult> DeleteProduct([FromQuery] int productId)
        {

           await _productService.DeleteProduct(productId);

            return NoContent();
        }

        [HttpGet("Product/Id")]
        public async Task<IActionResult> GetProductById([FromQuery] int productId)
        {

            var productDetails = await _productService.GetProductById(productId);
            if (productDetails == null)
            {
                return NotFound();
            }
            return Ok(productDetails);
        }

        [HttpPost("Products")]
        public async Task<IActionResult> GetActiveProducts([FromBody]SearchParams request,[FromQuery] int userId)
        {

            var productDetails = await _productService.GetActiveProductList(request,userId);
            if (productDetails == null)
            {
                return NotFound();
            }
            return Ok(productDetails);
        }

        [HttpGet("QueuedProducts")]
        public async Task<IActionResult> GetInActiveProducts([FromQuery] int userId)
        {

            var productDetails = await _productService.GetQueuedProductList(userId);
            if (productDetails == null)
            {
                return NotFound();
            }
            return Ok(productDetails);
        }

        [HttpPost("ApproveReject")]
        public async Task<IActionResult> ApproveOrRejectProdcut(ApprovalRequest request)
        {

            await _productService.ApproveOrRejectProduct(request);
           
            return Ok();
        }
    }
}
