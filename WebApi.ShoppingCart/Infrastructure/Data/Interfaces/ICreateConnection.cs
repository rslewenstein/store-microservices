using System.Data;

namespace WebApi.ShoppingCart.Infrastructure.Data.Interfaces
{
    public interface ICreateConnection
    {
        IDbConnection CreateConnectionDb();
    }
}