using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WebApi.ShoppingCart.Infrastructure.Messaging.Interfaces;

namespace WebApi.ShoppingCart.Infrastructure.Messaging
{
    public class MessageConnection : IMessageConnection
    {
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

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(orders);
                var body = Encoding.UTF8.GetBytes(json);

                model.BasicPublish(
                    exchange: "",
                    routingKey: "update_product_quantity",
                    basicProperties: null,
                    body: body);   
            }
        }

        private void Confirm_Event(object sender, BasicAckEventArgs e)
        {
            Console.WriteLine("Ack");
        }

        private void NotConfirm_Event(object sender, BasicNackEventArgs e)
        {
            Console.WriteLine("Nack");
        }
    }
}