using AutoMapper;
using WebApi.ShoppingCart.Domain.Dtos;

namespace WebApi.ShoppingCart.Domain.Helper
{
    public class MapperProfile: Profile 
    {
        public MapperProfile() {
            CreateMap<ShoppingCartEntity, ShoppingCartDto>();
            CreateMap<ShoppingCartDto, ShoppingCartEntity>();
        }
    }
}