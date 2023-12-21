
namespace WebApi.ShoppingCart.Domain.Dtos
{
    public class Order
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}