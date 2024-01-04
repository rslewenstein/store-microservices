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
                _logger.LogInformation(message: $"[ShoppingCartController] Getting Shopping By Id: {ShoppingCartId}");
                result = await _ShoppingCartService.GetById(ShoppingCartId);
            }
            catch(Exception ex)
            {
                _logger.LogError(message: $"[ShoppingCartController] Error happend when it tried to Get Shopping By Id: {ShoppingCartId}. Error: "+ ex);
            }
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> AddShoppingCart(ShoppingCartDto dto)
        {
            try
            {
                await _ShoppingCartService.ManageShoppingCart(dto);
                return Ok(new { message = "Success!" });
                _logger.LogInformation(message: $"[ShoppingCartController] Posted Shopping Cart");
            }
            catch(Exception ex)
            {
                _logger.LogError(message: $"[ShoppingCartController] Error happend when it tried to post shopping cart. Error: "+ ex);
                return BadRequest(new { message = "Error to post shopping cart" });
            }
        }
    }
}