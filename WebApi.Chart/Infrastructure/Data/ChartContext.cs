using Dapper;
using WebApi.Chart.Infrastructure.Data.Interfaces;

namespace WebApi.Chart.Infrastructure.Data
{
    public class ChartContext
    {
        private readonly ICreateConnection _createConn;

        public ChartContext(ICreateConnection createConn)
        {
            _createConn = createConn;
        }

        public async Task Init()
        {
            using var connection = _createConn.CreateConnectionDb();
            await _initProductsTables();

            async Task _initProductsTables()
            {
                var sql = 
                """
                    CREATE TABLE IF NOT EXISTS 
                    Charts (
                        ChartId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        UserId INT,
                        Order TEXT,
                        TotalPrice DOUBLE,
                        DateChart TEXT,
                        Confirmed BOOL
                    );
                """;
                await connection.ExecuteAsync(sql);
            }
        }
    }
}
