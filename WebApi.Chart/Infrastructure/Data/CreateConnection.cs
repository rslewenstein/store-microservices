using System.Data;
using Microsoft.Data.Sqlite;
using WebApi.Chart.Infrastructure.Data.Interfaces;

namespace WebApi.Chart.Infrastructure.Data
{
    public class CreateConnection : ICreateConnection
    {
        protected readonly IConfiguration Configuration;
        public CreateConnection(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IDbConnection CreateConnectionDb()
        {
            return new SqliteConnection(Configuration.GetConnectionString("DefaultConnection"));
        }
    }
}