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
        private readonly ILogger _logger;

        public ShoppingCartService(
            IShoppingCartRepository shoppingCartRepository, 
            IMapper mapper, 
            IMessageConnection messageConnection,
            ILogger<ShoppingCartService> logger)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
            _messageConnection = messageConnection;
            _logger = logger;
        }

        public async Task<ShoppingCartEntity> GetById(int id)
        {
            ShoppingCartEntity? result = new();
    
            try
            {
                if (id > 0)
                {
                    result = await _shoppingCartRepository.GetByShoppingCartIdAsync(id);
                    _logger.LogInformation(message: $"[ShoppingCartService] Getting Shopping By Id: {id}");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(message: $"[ShoppingCartService] Error happend when it got to post shopping cart. Error: "+ ex);
            }

            return result;
        }

        public async Task ManageShoppingCart(ShoppingCartDto dto)
        {
            if(dto.Orders?.Select(x => Convert.ToInt32(x.ProductId)).First() > 0)
            {
                ShoppingCartEntity entity = _mapper.Map<ShoppingCartEntity>(dto);
                entity.DateShoppingCart = DateTime.Now;
                entity.TotalPrice = CalculateTotalValue(dto);
                entity.Orders = SerializeOrders(dto.Orders);
                await SaveShoppingCart(entity);
                SendQuantityToProduct(entity.Orders);
            }
        }

        private double CalculateTotalValue(ShoppingCartDto dto)
        {
            double result = 0;
            try
            {
                foreach (var item in dto.Orders)
                {
                    double total = item.Quantity * item.Price;
                    result += total;
                    _logger.LogInformation(message: $"[ShoppingCartService] Calculating total value");
                }
            }
            catch (Exception ex)
            {   
                _logger.LogError(message: $"[ShoppingCartService] Error happend when it tried to calculate total value. Error: "+ ex);
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
                _logger.LogInformation(message: $"[ShoppingCartService] Save Shopping Cart");
            }
            catch (Exception ex)
            {   
                _logger.LogError(message: $"[ShoppingCartService] Error happend when it tried to Save Shopping Cart. Error: "+ ex);
            }
        }

        private string SerializeOrders(List<OrderDto> orders)
        {
            string? result = null;
            try
            {
                _logger.LogInformation(message: $"[ShoppingCartService] Serializing...");
                return Newtonsoft.Json.JsonConvert.SerializeObject(orders);
            }
            catch(Exception ex)
            {
                _logger.LogError(message: $"[ShoppingCartService] Error happend when it tried to Save Shopping Cart. Error: "+ ex);
            } 

            return result;
        }

        private void SendQuantityToProduct(string orders)
        {
            try
            {
                _messageConnection.SendMessageToProduct(orders);
                _logger.LogInformation(message: $"[ShoppingCartService] Send quantity to product");
            }
            catch (Exception ex)
            {   
                _logger.LogError(message: $"[ShoppingCartService] Error happend when it tried to Send quantity to product. Error: "+ ex);
            }
        }
    }
}