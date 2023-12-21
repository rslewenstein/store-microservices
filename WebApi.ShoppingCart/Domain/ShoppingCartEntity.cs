
namespace WebApi.ShoppingCart.Domain
{
    public class ShoppingCartEntity
    {
        public int ShoppingCartId { get; set; }
        public int UserId { get; set; }
        public string? Orders { get; set; }
        public double TotalPrice { get; set; }
        public DateTime DateShoppingCart { get; set; }
        public bool Confirmed { get; set; }
    }
}