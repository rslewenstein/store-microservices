using System.Data;
using Microsoft.Data.Sqlite;
using WebApi.Products.Infrastructure.Data.Interfaces;

namespace WebApi.Products.Infrastructure.Data
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