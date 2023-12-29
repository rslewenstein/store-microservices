using WebApi.Products.Infrastructure.Messaging.Interfaces;

namespace WebApi.Products.Infrastructure.Worker
{
    public class ProductWorker : BackgroundService
    {
        private readonly IMessageConnection _messageConnection;

        public ProductWorker(IMessageConnection messageConnection)
        {
            _messageConnection = messageConnection;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                await _messageConnection.ManageMessageFromShoppingCart();
                await Task.Delay(500, stoppingToken);
            }
        }
    }
}