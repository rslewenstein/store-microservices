using Dapper;
using WebApi.Chart.Domain;
using WebApi.Chart.Infrastructure.Data.Interfaces;
using WebApi.Chart.Infrastructure.Repository.Interfaces;

namespace WebApi.Chart.Infrastructure.Repository
{
    public class ChartRepository : IChartRepository
    {
        private readonly ICreateConnection _createConn;

        public ChartRepository(ICreateConnection createConn)
        {
            _createConn = createConn;
        }

        public async Task<ChartEntity> GetByChartIdAsync(int chartId)
        {
            return await GetById(chartId);
        }

        public async Task<int> SaveAsync(ChartEntity entity)
        {
            return await Save(entity);
        }

        private async Task<ChartEntity> GetById(int id)
        {
            using var connection = _createConn.CreateConnectionDb();
            var sql = 
            """
                SELECT 
                ChartId, UserId, Order, TotalPrice, DateChart  
                FROM Charts
                WHERE ChartId  = @id
            """;

            return await connection.QueryFirstAsync<ChartEntity>(sql, new { id });
        }

        private async Task<int> Save(ChartEntity entity)
        {
            using var connection = _createConn.CreateConnectionDb();
            var sql = """
                INSERT INTO 
                Charts (UserId, Order, TotalPrice, DateChart, Confirmed)
                VALUES 
                (@UserId, @Order, @TotalPrice, @DateChart, @Confirmed);
            """;
            var id = await connection.ExecuteAsync(sql, entity);

            return id;
        }
    }
}