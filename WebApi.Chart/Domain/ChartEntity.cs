
namespace WebApi.Chart.Domain
{
    public class ChartEntity
    {
        public int ChartId { get; set; }
        public int UserId { get; set; }
        public string? Order { get; set; }
        // public int ProductId { get; set; }
        // public int Quantity { get; set; }
        // price
        public double TotalPrice { get; set; }
        public DateTime DateChart { get; set; }
        public bool Confirmed { get; set; }
    }
}