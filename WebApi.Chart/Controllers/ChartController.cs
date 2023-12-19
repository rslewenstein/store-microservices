using Microsoft.AspNetCore.Mvc;
using WebApi.Chart.Application.Interfaces;
using WebApi.Chart.Domain;
using WebApi.Chart.Domain.Dtos;

namespace WebApi.Chart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : Controller
    {
        private readonly IChartService _chartService;

        public ChartController(IChartService chartService)
        {
            _chartService = chartService;
        }

        [HttpGet("{chartId}")]
        public async Task<ChartEntity> GetChartById(int chartId)
        {
            return await _chartService.GetById(chartId);
        }

        [HttpPost]
        public async Task<IActionResult> AddChart(ChartDto dto)
        {
            await _chartService.ManageChart(dto);
            return Ok(new { message = "Chart added" });
        }
    }
}