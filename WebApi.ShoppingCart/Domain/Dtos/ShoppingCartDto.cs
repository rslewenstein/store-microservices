
namespace WebApi.ShoppingCart.Domain.Dtos
{
    public class ShoppingCartDto
    {
        public int ShoppingCartId { get; set; }
        public int UserId { get; set; }
        public List<OrderDto>? Orders { get; set; }
        public DateTime DateShoppingCart { get; set; }
        public bool Confirmed { get; set; }
    }
}