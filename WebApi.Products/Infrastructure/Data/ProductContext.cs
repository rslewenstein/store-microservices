using Dapper;
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
            //await _insertProducts();

            async Task _initProductsTables()
            {
                var sql = 
                """
                    CREATE TABLE IF NOT EXISTS 
                    Products (
                        ProductId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        ProductName TEXT,
                        ProductType TEXT,
                        Quantity INTEGER,
                        Price DOUBLE
                    );
                """;
                await connection.ExecuteAsync(sql);
            }

            // async Task _insertProducts()
            // {
            //     var sql = 
            //     """
            //         INSERT INTO Products (ProductName, ProductType, Quantity, Price)
            //         Values ('Camisa Manchester United', '2023', 100, 239.90);
                            
            //         INSERT INTO Products (ProductName, ProductType, Quantity, Price)
            //         Values ('Camisa Flamengo', 'Segundo uniforme - 2023', 120, 249.90);
                            
            //         INSERT INTO Products (ProductName, ProductType, Quantity, Price)
            //         Values ('Tenis Adidas', 'Runfalcon 3.0', 40, 269.99);
                            
            //         INSERT INTO Products (ProductName, ProductType, Quantity, Price)
            //         Values ('Corta Vento Puma', 'Black', 50, 159.90);
                            
            //         INSERT INTO Products (ProductName, ProductType, Quantity, Price)
            //         Values ('Jaqueta Adidas', 'White', 60, 160.90);
                            
            //         INSERT INTO Products (ProductName, ProductType, Quantity, Price)
            //         Values ('Jaqueta Adidas', 'Black', 45, 155.90);
                            
            //         INSERT INTO Products (ProductName, ProductType, Quantity, Price)
            //         Values ('Chuteira Puma King', 'Campo - Preta', 25, 281.29);    
            //     """;
            //     await connection.ExecuteAsync(sql);
            // }
        }
    }
}
