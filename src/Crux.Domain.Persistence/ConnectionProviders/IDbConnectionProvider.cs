using System.Data;

namespace Crux.Domain.Persistence
{
    public interface IDbConnectionProvider
    {
        IDbConnection GetConnection();
    }
}
