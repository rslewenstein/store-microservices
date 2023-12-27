using Dapper;
using WebApi.ShoppingCart.Domain;
using WebApi.ShoppingCart.Infrastructure.Data.Interfaces;
using WebApi.ShoppingCart.Infrastructure.Repository.Interfaces;

namespace WebApi.ShoppingCart.Infrastructure.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ICreateConnection _createConn;

        public ShoppingCartRepository(ICreateConnection createConn)
        {
            _createConn = createConn;
        }

        public async Task<ShoppingCartEntity> GetByShoppingCartIdAsync(int shoppingCartId)
        {
            return await GetById(shoppingCartId);
        }

        public async Task<int> GetLastByIdAsync()
        {
            return await GetLastById();
        }

        public async Task SaveAsync(ShoppingCartEntity entity)
        {
            await Save(entity);
        }

        private async Task<ShoppingCartEntity> GetById(int id)
        {
            using var connection = _createConn.CreateConnectionDb();
            var sql = 
            """
                SELECT 
                ShoppingCartId, UserId, Orders, TotalPrice, DateShoppingCart  
                FROM ShoppingCarts
                WHERE ShoppingCartId  = @id
            """;

            return await connection.QueryFirstAsync<ShoppingCartEntity>(sql, new { id });
        }

        private async Task<int> GetLastById()
        {
            using var connection = _createConn.CreateConnectionDb();
            var sql = 
            """
                SELECT 
                ShoppingCartId  
                FROM ShoppingCarts
                ORDER BY ShoppingCartId Desc
            """;

            return await connection.QueryFirstAsync<int>(sql);
        }

        private async Task Save(ShoppingCartEntity entity)
        {
            using var connection = _createConn.CreateConnectionDb();
            var sql = """
                INSERT INTO 
                ShoppingCarts (UserId, Orders, TotalPrice, DateShoppingCart, Confirmed)
                VALUES 
                (@UserId, @Orders, @TotalPrice, @DateShoppingCart, @Confirmed);
            """;
            await connection.ExecuteAsync(sql, entity);
        }
    }
}