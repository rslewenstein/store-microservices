
namespace WebApi.Products.Domain
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductType { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}