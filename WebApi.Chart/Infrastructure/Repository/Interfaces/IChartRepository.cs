using WebApi.Chart.Domain;

namespace WebApi.Chart.Infrastructure.Repository.Interfaces
{
    public interface IChartRepository
    {
        Task<int> SaveAsync(ChartEntity entity);
        Task<ChartEntity> GetByChartIdAsync(int chartId);
    }
}