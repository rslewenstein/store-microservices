using Dapper;
using WebApi.ShoppingCart.Infrastructure.Data.Interfaces;

namespace WebApi.ShoppingCart.Infrastructure.Data
{
    public class ShoppingCartContext
    {
        private readonly ICreateConnection _createConn;

        public ShoppingCartContext(ICreateConnection createConn)
        {
            _createConn = createConn;
        }

        public async Task Init()
        {
            using var connection = _createConn.CreateConnectionDb();
            await _initShoppingCartTables();

            async Task _initShoppingCartTables()
            {
                var sql = 
                """
                    CREATE TABLE IF NOT EXISTS 
                    ShoppingCarts (
                        ShoppingCartId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        UserId INT,
                        Orders TEXT,
                        TotalPrice DOUBLE,
                        DateShoppingCart TEXT,
                        Confirmed BOOL
                    );
                """;
                await connection.ExecuteAsync(sql);
            }
        }
    }
}
