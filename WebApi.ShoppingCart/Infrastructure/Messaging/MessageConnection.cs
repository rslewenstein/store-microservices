using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WebApi.ShoppingCart.Infrastructure.Messaging.Interfaces;

namespace WebApi.ShoppingCart.Infrastructure.Messaging
{
    public class MessageConnection : IMessageConnection
    {
        private readonly ILogger _logger;

        public MessageConnection(ILogger<MessageConnection> logger)
        {
            _logger = logger;
        }
        public void SendMessageToProduct(string orders)
        {
            var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            
            using(var connection = connectionFactory.CreateConnection())
            using(var model = connection.CreateModel())
            {
                model.ConfirmSelect();
                model.BasicAcks += Confirm_Event;
                model.BasicNacks += NotConfirm_Event;

                model.QueueDeclare(
                    queue:"update_product_quantity",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var body = Encoding.UTF8.GetBytes(orders);

                model.BasicPublish(
                    exchange: "",
                    routingKey: "update_product_quantity",
                    basicProperties: null,
                    body: body);

                _logger.LogInformation(message: $"[MessageConnection] Sending message to Message Broker");    
            }
        }

        private void Confirm_Event(object sender, BasicAckEventArgs e)
        {
            _logger.LogInformation(message: $"[MessageConnection] Message ACK");
        }

        private void NotConfirm_Event(object sender, BasicNackEventArgs e)
        {
            _logger.LogInformation(message: $"[MessageConnection] Message NACK");
        }
    }
}