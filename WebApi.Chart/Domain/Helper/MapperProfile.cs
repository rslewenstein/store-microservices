using AutoMapper;
using WebApi.Chart.Domain.Dtos;

namespace WebApi.Chart.Domain.Helper
{
    public class MapperProfile: Profile 
    {
        public MapperProfile() {
            CreateMap<ChartEntity, ChartDto>();
            CreateMap<ChartDto, ChartEntity>();
        }
    }
}