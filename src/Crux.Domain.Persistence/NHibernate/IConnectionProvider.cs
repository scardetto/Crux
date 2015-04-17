using System.Data;

namespace Crux.Domain.Persistence.NHibernate
{
    public interface IDbConnectionProvider
    {
        IDbConnection GetConnection();
    }
}
