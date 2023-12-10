
namespace WebApi.Products.Domain.Dtos
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductType { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}