using WebApi.Chart.Domain;
using WebApi.Chart.Infrastructure.Repository.Interfaces;

namespace WebApi.Chart.Infrastructure.Repository
{
    public class ChartRepository : IChartRepository
    {
        public Task<ChartEntity> GetByChartIdAsync(int chartId)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(ChartEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}