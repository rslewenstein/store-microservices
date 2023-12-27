using WebApi.ShoppingCart.Domain;
using WebApi.ShoppingCart.Domain.Dtos;

namespace WebApi.ShoppingCart.Application.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartEntity> GetById(int id);
        Task ManageShoppingCart(ShoppingCartDto dto);
    }
}