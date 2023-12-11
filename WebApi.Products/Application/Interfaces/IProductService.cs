using WebApi.Products.Domain.Dtos;

namespace WebApi.Products.Application.Interfaces
{
    public interface IProductService
    {
        Task<IList<ProductDto>> ListAll();
        Task<ProductDto> ListById(int id);
        Task UpdateQuantityProduct(int id, int qtd);
    }
}