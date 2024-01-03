using WebApi.ShoppingCart.Domain;

namespace WebApi.ShoppingCart.Infrastructure.Repository.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task SaveAsync(ShoppingCartEntity entity);
        Task<ShoppingCartEntity> GetByShoppingCartIdAsync(int shoppingCartId);
        Task<int> GetLastIdAsync();
    }
}