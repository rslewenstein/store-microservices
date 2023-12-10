using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using WebApi.Products.Infrastructure.Data.Interfaces;

namespace WebApi.Products.Infrastructure.Data
{
    public class ProductContext
    {
        private readonly ICreateConnection _createConn;

        public ProductContext(ICreateConnection createConn)
        {
            _createConn = createConn;
        }

        public async Task Init()
        {
            using var connection = _createConn.CreateConnectionDb();
            await _initProductsTables();

            async Task _initProductsTables()
            {
                var sql = """
                    CREATE TABLE IF NOT EXISTS 
                    Products (
                        ProductId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        ProductName TEXT,
                        Type TEXT
                    );

                    CREATE TABLE IF NOT EXISTS 
                    ProductStocks (
                        ProductStockId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        ProductId INTEGER,
                        Quantity INTEGER,
                        Price DOUBLE,
                        FOREIGN KEY(ProductId) REFERENCES Products(ProductId)
                    );
                """;
                await connection.ExecuteAsync(sql);
            }
        }
    }
}