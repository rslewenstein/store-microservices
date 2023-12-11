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

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task <IList<ProductDto>> GetAllProducts()
        {
            return await _productService.ListAll();
        }

        [HttpGet("{productId}")]
        public async Task<ProductDto> GetProductById(int productId)
        {
            return await _productService.ListById(productId);
        }

        [HttpPut("{id}, {qtd}")]
        public async Task<IActionResult> Update(int id, int qtd)
        {
            await _productService.UpdateQuantityProduct(id, qtd);
            return Ok(new { message = "Product updated" });
        }
    }
}