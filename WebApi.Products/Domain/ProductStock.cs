namespace WebApi.Products.Domain
{
    public class ProductStock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public Product? Product { get; set; }
    }
}