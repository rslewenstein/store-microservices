
namespace WebApi.ShoppingCart.Domain.Dtos
{
    public class OrderDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}