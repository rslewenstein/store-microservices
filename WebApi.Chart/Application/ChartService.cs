using System.Text.Json;
using AutoMapper;
using WebApi.Chart.Application.Interfaces;
using WebApi.Chart.Domain;
using WebApi.Chart.Domain.Dtos;
using WebApi.Chart.Infrastructure.Repository.Interfaces;

namespace WebApi.Chart.Application
{
    public class ChartService : IChartService
    {
        private readonly IChartRepository _chartRepository;
        private readonly IMapper _mapper;
        public ChartService(IChartRepository chartRepository, IMapper mapper)
        {
            _chartRepository = chartRepository;
            _mapper = mapper;
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

        public Task SendQuantityToProduct(int productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public async Task ManageChart(ChartDto dto)
        {
            if(dto.Orders?.Select(x => Convert.ToInt32(x.ProductId)).First() > 0)
            {
                ChartEntity entity = _mapper.Map<ChartEntity>(dto);
                entity.DateChart = DateTime.Now;
                entity.Orders = SerializeOrders(dto.Orders);
                await SaveChart(entity);

                foreach (var item in dto.Orders)
                {
                    await SendQuantityToProduct(item.ProductId, item.Quantity);
                }
            }
        }

        private async Task SaveChart(ChartEntity chart)
        {
            try
            {
                if(string.IsNullOrEmpty(chart.Orders))
                    return;
                
                await _chartRepository.SaveAsync(chart);
                chart.ChartId = await _chartRepository.GetLastByIdAsync();
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }

        private static string SerializeOrders(List<Order> orders)
        {
            try
            {
                return JsonSerializer.Serialize(orders);
            }
            catch(Exception ex)
            {
                throw new Exception();
            } 
        }
    }
}