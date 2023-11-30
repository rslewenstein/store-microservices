using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace WebApi.Products.Infrastructure.Data
{
    public class ProductContext
    {
        protected readonly IConfiguration Configuration;

        public ProductContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new SqliteConnection(Configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task Init()
        {
            using var connection = CreateConnection();
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