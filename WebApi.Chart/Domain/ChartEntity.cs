
namespace WebApi.Chart.Domain
{
    public class ChartEntity
    {
        public int ChartId { get; set; }
        public int UserId { get; set; }
        public string? Order { get; set; } // productid, qtd, price
        public double TotalPrice { get; set; }
        public DateTime DateChart { get; set; }
        public bool Confirmed { get; set; }
    }
}