
namespace WebApi.Chart.Domain.Dtos
{
    public class ChartDto
    {
        public int ChartId { get; set; }
        public int UserId { get; set; }
        public List<Order>? Orders { get; set; }
        public DateTime DateChart { get; set; }
        public bool Confirmed { get; set; }
    }
}