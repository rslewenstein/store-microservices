using System.Data;

namespace WebApi.Chart.Infrastructure.Data.Interfaces
{
    public interface ICreateConnection
    {
        IDbConnection CreateConnectionDb();
    }
}