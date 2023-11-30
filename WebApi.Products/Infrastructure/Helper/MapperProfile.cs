using AutoMapper;
using WebApi.Products.Domain;
using WebApi.Products.Domain.Dtos;

namespace WebApi.Products.Infrastructure.Helper
{
    public class MapperProfile: Profile 
    {
        public MapperProfile() {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<ProductStock, ProductStockDto>();
            CreateMap<ProductStockDto, ProductStock>();
        }
    }
}