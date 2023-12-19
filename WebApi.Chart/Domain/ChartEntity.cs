
namespace WebApi.Chart.Domain
{
    public class ChartEntity
    {
        public int ChartId { get; set; }
        public int UserId { get; set; }
        public string? Orders { get; set; }
        public double TotalPrice { get; set; }
        public DateTime DateChart { get; set; }
        public bool Confirmed { get; set; }
    }
}