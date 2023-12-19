using WebApi.Chart.Domain;

namespace WebApi.Chart.Infrastructure.Repository.Interfaces
{
    public interface IChartRepository
    {
        Task SaveAsync(ChartEntity entity);
        Task<ChartEntity> GetByChartIdAsync(int chartId);
        Task<int> GetLastByIdAsync();
    }
}