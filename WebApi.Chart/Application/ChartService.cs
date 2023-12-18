using WebApi.Chart.Application.Interfaces;
using WebApi.Chart.Domain;
using WebApi.Chart.Infrastructure.Repository.Interfaces;

namespace WebApi.Chart.Application
{
    public class ChartService : IChartService
    {
        private readonly IChartRepository _chartRepository;
        public ChartService(IChartRepository chartRepository)
        {
            _chartRepository = chartRepository;
        }

        public Task CheckCartReceived(ChartEntity chart)
        {
            throw new NotImplementedException();
        }

        public async Task<ChartEntity> GetById(int id)
        {
            ChartEntity? result = new();
    
            try
            {
                if (id > 0)
                {
                    result = await _chartRepository.GetByChartIdAsync(id);
                }
            }
            catch(Exception ex)
            {
                throw new Exception();
            }

            return result;
        }

        public async Task SaveChart(ChartEntity chart)
        {
            try
            {
                if (string.IsNullOrEmpty(chart.Order))
                    return;

                var id = await _chartRepository.SaveAsync(chart);
                chart.ChartId = id;

                if(id > 0)
                {
                    await SendToPayment(chart);
                    await ManageChart(chart);
                }

            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }

        public Task SendQuantityToProduct(int productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task SendToPayment(ChartEntity chart)
        {
            throw new NotImplementedException();
        }

        private async Task ManageChart(ChartEntity chart)
        {
            int productId = 0;
            int quantity = 0;
            if(!string.IsNullOrEmpty(chart.Order))
            {
                foreach (var item in chart.Order.ToArray())
                {
                    await SendQuantityToProduct(productId, quantity);
                }
            }
        }
    }
}