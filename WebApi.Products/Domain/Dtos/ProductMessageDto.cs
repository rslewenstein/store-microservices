using Newtonsoft.Json;

namespace WebApi.Products.Domain.Dtos
{
    public class ProductMessageDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}