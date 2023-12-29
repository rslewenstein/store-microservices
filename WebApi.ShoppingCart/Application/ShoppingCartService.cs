using System.Text.Json;
using AutoMapper;
using WebApi.ShoppingCart.Application.Interfaces;
using WebApi.ShoppingCart.Domain;
using WebApi.ShoppingCart.Domain.Dtos;
using WebApi.ShoppingCart.Infrastructure.Messaging.Interfaces;
using WebApi.ShoppingCart.Infrastructure.Repository.Interfaces;

namespace WebApi.ShoppingCart.Application
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;
        private readonly IMessageConnection _messageConnection;
        public ShoppingCartService(
            IShoppingCartRepository shoppingCartRepository, 
            IMapper mapper, 
            IMessageConnection messageConnection)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
            _messageConnection = messageConnection;
        }

        public async Task<ShoppingCartEntity> GetById(int id)
        {
            ShoppingCartEntity? result = new();
    
            try
            {
                if (id > 0)
                {
                    result = await _shoppingCartRepository.GetByShoppingCartIdAsync(id);
                }
            }
            catch(Exception ex)
            {
                throw new Exception();
            }

            return result;
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
                SendQuantityToProduct(entity.Orders);
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

        private async Task SaveShoppingCart(ShoppingCartEntity shoppingCart)
        {
            try
            {
                if(string.IsNullOrEmpty(shoppingCart.Orders))
                    return;
                
                await _shoppingCartRepository.SaveAsync(shoppingCart);
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }

        private static string SerializeOrders(List<OrderDto> orders)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(orders);
            }
            catch(Exception ex)
            {
                throw new Exception();
            } 
        }

        private void SendQuantityToProduct(string orders)
        {
            try
            {
                _messageConnection.SendMessageToProduct(orders);
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }
    }
}