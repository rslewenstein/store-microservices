using WebApi.Products.Domain.Dtos;

namespace WebApi.Products.Infrastructure.Messaging.Interfaces
{
    public interface IMessageConnection
    {
        Task ManageMessageFromShoppingCart();
    }
}