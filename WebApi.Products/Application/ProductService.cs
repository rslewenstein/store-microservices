using AutoMapper;
using WebApi.Products.Application.Interfaces;
using WebApi.Products.Domain;
using WebApi.Products.Domain.Dtos;
using WebApi.Products.Infrastructure.Repository.Interfaces;

namespace WebApi.Products.Application
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ProductService(IProductRepository productRepository, 
                              IMapper mapper,
                              ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IList<ProductDto>> ListAll()
        {   
            List<ProductDto>? result = new();
            try
            {
               var resultList = await _productRepository.GetAllAsync();

               if (resultList.Count() > 0)
               {
                    foreach (var item in resultList)
                    {
                        result?.Add(_mapper.Map<ProductDto>(item));
                    }
                    _logger.LogInformation(message: $"[ProductService] Listing all products.");
               }               
            }
            catch(Exception ex)
            {
                _logger.LogError(message: $"[ProductService] Error happend when it tried to list all products. Error: {ex}");
            }

            return result;
        }

        public async Task<ProductDto> ListById(int id)
        {
            ProductDto? result = new();
    
            try
            {
                if (id > 0)
                {
                    result = _mapper.Map<ProductDto>(await _productRepository.GetByIdAsync(id));
                    _logger.LogInformation(message: $"[ProductService] Listing product by id: {id}");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(message: $"[ProductService] Error happend when it tried to list product by id. Error: {ex}");
            }

            return result;
        }

        public async Task UpdateQuantityProduct(int id, int qtd)
        {
            try
            {
                if (id < 0 && qtd < 1)
                    return;

                Product result = _mapper.Map<Product>(await _productRepository.GetByIdAsync(id));

                if (result.ProductId > 0)
                {
                    result.Quantity = qtd;
                   await _productRepository.UpdateAsync(result);
                   _logger.LogInformation(message: $"[ProductService] Updating product quantity by id: {id}");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(message: $"[ProductService] Error happend when it tried to update quantity of products. Error: {ex}");
            }
        }
    }
}