using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Products.Application.Interfaces;
using WebApi.Products.Domain;
using WebApi.Products.Domain.Dtos;

namespace WebApi.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
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