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

        public ShoppingCartController(IShoppingCartService ShoppingCartService)
        {
            _ShoppingCartService = ShoppingCartService;
        }

        [HttpGet("{ShoppingCartId}")]
        public async Task<ShoppingCartEntity> GetShoppingCartById(int ShoppingCartId)
        {
            return await _ShoppingCartService.GetById(ShoppingCartId);
        }

        [HttpPost]
        public async Task<IActionResult> AddShoppingCart(ShoppingCartDto dto)
        {
            await _ShoppingCartService.ManageShoppingCart(dto);
            return Ok(new { message = "ShoppingCart added" });
        }
    }
}