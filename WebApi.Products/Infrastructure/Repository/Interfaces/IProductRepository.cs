using WebApi.Products.Domain;

namespace WebApi.Products.Infrastructure.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<IList<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task UpdateAsync(ProductStock entity);
        Task SaveChangesAsync();
    }
}