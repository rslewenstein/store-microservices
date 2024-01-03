using Dapper;
using WebApi.Products.Domain;
using WebApi.Products.Infrastructure.Data.Interfaces;
using WebApi.Products.Infrastructure.Repository.Interfaces;

namespace WebApi.Products.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICreateConnection _createConn;
        private readonly ILogger _logger;

        public ProductRepository(ICreateConnection createConn, ILogger<ProductRepository> logger)
        {
            _createConn = createConn;
            _logger = logger;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            _logger.LogInformation(message: $"[ProductRepository] Getting all products async.");
            return await GetAll();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            _logger.LogInformation(message: $"[ProductRepository] Getting product by id {id}");
            return await GetById(id);
        }

        public async Task UpdateAsync(Product entity)
        {
            _logger.LogInformation(message: $"[ProductRepository] Updating product.");
            await Update(entity);
        }

        private async Task<IEnumerable<Product>> GetAll()
        {
            using var connection = _createConn.CreateConnectionDb();
            var sql = 
            """
                SELECT 
                ProductId, ProductName, ProductType, Quantity, Price  
                FROM Products
                WHERE ProductId  > 0
            """;

            return await connection.QueryAsync<Product>(sql);
        }

        private async Task<Product> GetById(int id)
        {
            using var connection = _createConn.CreateConnectionDb();
            var sql = 
            """
                SELECT 
                ProductId, ProductName, ProductType, Quantity, Price  
                FROM Products
                WHERE ProductId  = @id
            """;

            return await connection.QueryFirstAsync<Product>(sql, new { id });
        }

        private async Task Update(Product entity)
        {
            using var connection = _createConn.CreateConnectionDb();
            var sql = """
                UPDATE Products 
                SET Quantity = Quantity - @Quantity
                WHERE ProductId = @ProductId
            """;
            await connection.ExecuteAsync(sql, entity);
        }
    }
}