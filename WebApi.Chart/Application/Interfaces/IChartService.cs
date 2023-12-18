using WebApi.Chart.Domain;

namespace WebApi.Chart.Application.Interfaces
{
    public interface IChartService
    {
        Task<ChartEntity> GetById(int id);
        Task CheckCartReceived(ChartEntity chart);
        Task SaveChart(ChartEntity chart);

        Task SendQuantityToProduct(int productId, int quantity);
        Task SendToPayment(ChartEntity chart);
    }
}