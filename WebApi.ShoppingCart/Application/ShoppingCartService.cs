using System.Text.Json;
using AutoMapper;
using WebApi.ShoppingCart.Application.Interfaces;
using WebApi.ShoppingCart.Domain;
using WebApi.ShoppingCart.Domain.Dtos;
using WebApi.ShoppingCart.Infrastructure.Repository.Interfaces;

namespace WebApi.ShoppingCart.Application
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _ShoppingCartRepository;
        private readonly IMapper _mapper;
        public ShoppingCartService(IShoppingCartRepository ShoppingCartRepository, IMapper mapper)
        {
            _ShoppingCartRepository = ShoppingCartRepository;
            _mapper = mapper;
        }

        public async Task<ShoppingCartEntity> GetById(int id)
        {
            ShoppingCartEntity? result = new();
    
            try
            {
                if (id > 0)
                {
                    result = await _ShoppingCartRepository.GetByShoppingCartIdAsync(id);
                }
            }
            catch(Exception ex)
            {
                throw new Exception();
            }

            return result;
        }

        public Task SendQuantityToProduct(int productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public async Task ManageShoppingCart(ShoppingCartDto dto)
        {
            if(dto.Orders?.Select(x => Convert.ToInt32(x.ProductId)).First() > 0)
            {
                ShoppingCartEntity entity = _mapper.Map<ShoppingCartEntity>(dto);
                entity.DateShoppingCart = DateTime.Now;
                entity.TotalPrice = CalculeValueTotal(dto);
                entity.Orders = SerializeOrders(dto.Orders);
                await SaveShoppingCart(entity);

                //TODO: mensageria
                // foreach (var item in dto.Orders)
                // {
                //     await SendQuantityToProduct(item.ProductId, item.Quantity);
                // }
            }
        }

        private double CalculeValueTotal(ShoppingCartDto dto)
        {
            double result = 0;
            try
            {
                foreach (var item in dto.Orders)
                {
                    double total = item.Quantity * item.Price;
                    result += total;
                }
            }
            catch (System.Exception)
            {   
                throw;
            }
        
            return result;
        }

        private async Task SaveShoppingCart(ShoppingCartEntity ShoppingCart)
        {
            try
            {
                if(string.IsNullOrEmpty(ShoppingCart.Orders))
                    return;
                
                await _ShoppingCartRepository.SaveAsync(ShoppingCart);
                ShoppingCart.ShoppingCartId = await _ShoppingCartRepository.GetLastByIdAsync();
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }

        private static string SerializeOrders(List<Order> orders)
        {
            try
            {
                return JsonSerializer.Serialize(orders);
            }
            catch(Exception ex)
            {
                throw new Exception();
            } 
        }
    }
}