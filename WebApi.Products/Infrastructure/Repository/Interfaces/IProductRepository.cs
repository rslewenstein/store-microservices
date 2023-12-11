using WebApi.Products.Domain;

namespace WebApi.Products.Infrastructure.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task UpdateAsync(Product entity);
    }
}