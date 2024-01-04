using System.Text;
using AutoMapper;
using RabbitMQ.Client;
using WebApi.Products.Domain;
using WebApi.Products.Domain.Dtos;
using WebApi.Products.Infrastructure.Messaging.Interfaces;
using WebApi.Products.Infrastructure.Repository.Interfaces;

namespace WebApi.Products.Infrastructure.Messaging
{
    public class MessageConnection : IMessageConnection
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public MessageConnection(IProductRepository productRepository, 
                                 IMapper mapper, 
                                 ILogger<MessageConnection> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task ManageMessageFromShoppingCart()
        {
            await ReceiveMessageFromShoppingCart();     
        }

        private async Task ReceiveMessageFromShoppingCart()
        {
            var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            ProductDto dto = new();

            using(var connection = connectionFactory.CreateConnection())
            using(var model = connection.CreateModel())
            {
                model.QueueDeclare(
                    queue:"update_product_quantity",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var data = model.BasicGet("update_product_quantity", true);  

                if (data != null)
                {
                    //var dataArray = Encoding.UTF8.GetString(data.Body.ToArray());
                    // Newtonsoft.Json.JsonConvert.DeserializeObject(dataArray);
                    
                    byte[] body = data.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var listDto = RemoveQuotes(message);
                    await UpdateQuantityRepositoryAsync(listDto);
                    _logger.LogInformation(message: $"[MessageConnection] Receiving message from ShoppingCart Api");
                }

            }
        }

        // I needed to do it because Json deserialize didn't work.
        // I tryied to get a object, a ListProductMessageDto or a ProductMessageDto, but didn't work.
        private List<ProductMessageDto>? RemoveQuotes(string text)
        {
            text = text.Replace("[", "").Trim();
            text = text.Replace("]", "").Trim();
            string[] products = text.Split(',');
            ProductMessageDto dto = new();
            List<ProductMessageDto> listDto = new();

            foreach (var item in products)
            {
                if (item.Contains("Prod"))
                    dto.ProductId = Convert.ToInt32(item.Replace("{\"ProductId\":", "").Trim());

                if (item.Contains("Quant"))    
                    dto.Quantity = Convert.ToInt32(item.Replace("\"Quantity\":", "").Trim());

                if (item.Contains("Price"))
                    dto = new(); 

                listDto.Add(dto);    
            }

            _logger.LogInformation(message: $"[MessageConnection] Removing quotes");
            return listDto.Distinct().ToList();
        }

        private async Task UpdateQuantityRepositoryAsync(List<ProductMessageDto>? listDto)
        {
            foreach (var item in listDto)
            {
                if(item.ProductId > 0)
                    await _productRepository.UpdateAsync(_mapper.Map<Product>(item));

                _logger.LogInformation(message: $"[MessageConnection] Updating quantity.");    
            }
        }
    }
}