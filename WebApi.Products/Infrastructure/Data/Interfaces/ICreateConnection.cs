using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Products.Infrastructure.Data.Interfaces
{
    public interface ICreateConnection
    {
        IDbConnection CreateConnectionDb();
    }
}