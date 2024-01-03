using Microsoft.AspNetCore.Mvc;
using WebApi.Products.Application.Interfaces;
using WebApi.Products.Domain.Dtos;

namespace WebApi.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task <IList<ProductDto>> GetAllProducts()
        {
            _logger.LogInformation(message: $"[ProductController] Getting all products.");
            return await _productService.ListAll();
        }

        [HttpGet("{productId}")]
        public async Task<ProductDto> GetProductById(int productId)
        {
            _logger.LogInformation(message: $"[ProductController] Getting product by id: {productId}");
            return await _productService.ListById(productId);
        }

        [HttpPut("{id}, {qtd}")]
        public async Task<IActionResult> Update(int id, int qtd)
        {
            await _productService.UpdateQuantityProduct(id, qtd);
            _logger.LogInformation(message: $"[ProductController] Updating product by id {id} and quantity {qtd}");
            return Ok(new { message = "Product updated" });
        }
    }
}