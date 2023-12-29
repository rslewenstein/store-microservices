using Microsoft.AspNetCore.Mvc;
using WebApi.ShoppingCart.Application.Interfaces;
using WebApi.ShoppingCart.Domain;
using WebApi.ShoppingCart.Domain.Dtos;

namespace WebApi.ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _ShoppingCartService;
        private readonly ILogger _logger;

        public ShoppingCartController(IShoppingCartService ShoppingCartService, ILogger<ShoppingCartController> logger)
        {
            _ShoppingCartService = ShoppingCartService;
            _logger = logger;
        }

        [HttpGet("{ShoppingCartId}")]
        public async Task<ShoppingCartEntity> GetShoppingCartById(int ShoppingCartId)
        {
            ShoppingCartEntity? result = new();
            try
            {
                _logger.LogInformation(message: $"Getting Shopping By Id: {ShoppingCartId}");
                result = await _ShoppingCartService.GetById(ShoppingCartId);
            }
            catch(Exception ex)
            {
                _logger.LogError(message: $"Error when tryied Got Shopping By Id: {ShoppingCartId}. Error: "+ ex);
            }
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> AddShoppingCart(ShoppingCartDto dto)
        {
            try
            {
                await _ShoppingCartService.ManageShoppingCart(dto);
                _logger.LogInformation(message: $"Posted Shopping Cart");
            }
            catch(Exception ex)
            {
                _logger.LogError(message: $"Error when tryied post shopping cart. Error: "+ ex);
                return BadRequest(new { message = "Error to post shopping cart" });
            }

            return Ok(new { message = "ShoppingCart added" });
        }
    }
}