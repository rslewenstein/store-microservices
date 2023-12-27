namespace WebApi.ShoppingCart.Infrastructure.Messaging.Interfaces
{
    public interface IMessageConnection
    {
        void SendMessageToProduct(string orders);
    }
}