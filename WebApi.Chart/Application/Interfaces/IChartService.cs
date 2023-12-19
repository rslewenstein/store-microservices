using WebApi.Chart.Domain;
using WebApi.Chart.Domain.Dtos;

namespace WebApi.Chart.Application.Interfaces
{
    public interface IChartService
    {
        Task<ChartEntity> GetById(int id);
        Task ManageChart(ChartDto dto);

        Task SendQuantityToProduct(int productId, int quantity);
    }
}