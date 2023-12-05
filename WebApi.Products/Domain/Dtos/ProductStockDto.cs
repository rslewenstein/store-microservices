namespace WebApi.Products.Domain.Dtos
{
    public class ProductStockDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}