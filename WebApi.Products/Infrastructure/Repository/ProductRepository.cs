using Dapper;
using WebApi.Products.Domain;
using WebApi.Products.Infrastructure.Data.Interfaces;
using WebApi.Products.Infrastructure.Repository.Interfaces;

namespace WebApi.Products.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICreateConnection _createConn;

        public ProductRepository(ICreateConnection createConn)
        {
            _createConn = createConn;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await GetAll();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await GetById(id);
        }

        public async Task SaveChangesAsync()
        {
            await Save();
        }

        public async Task UpdateAsync(ProductStock entity)
        {
            await Update(entity);
        }

        private async Task<IEnumerable<Product>> GetAll()
        {
            using var connection = _createConn.CreateConnectionDb();
            var sql = 
            """
                SELECT ProductId, ProductName, Type 
                FROM Products
            """;

            return await connection.QueryAsync<Product>(sql);
        }

        private Task<Product> GetById(int id)
        {
            throw new NotImplementedException();
        }

        private Task Save()
        {
            throw new NotImplementedException();
        }

        private Task Update(ProductStock entity)
        {
            throw new NotImplementedException();
        }
    }
}