using WebApi.Products.Domain;
using WebApi.Products.Infrastructure.Repository.Interfaces;

namespace WebApi.Products.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        public async Task<IList<Product>> GetAllAsync()
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

        private Task<IList<Product>> GetAll()
        {
            throw new NotImplementedException();
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