using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Crux.Core.Extensions;

namespace Crux.Domain.Persistence
{
    public class SqlConnectionProvider : IDbConnectionProvider
    {
        private readonly string _connectionString;

        public static SqlConnectionProvider FromConnectionString(string connectionString)
        {
            return new SqlConnectionProvider(connectionString);
        }

        public static SqlConnectionProvider FromConnectionStringKey(string connectionStringKey)
        {
            var connectionStringConfig = ConfigurationManager.ConnectionStrings[connectionStringKey];

            if (connectionStringConfig == null) {
                throw new Exception("Could not find connection string for key {0}".ToFormat(connectionStringKey));
            }

            return new SqlConnectionProvider(connectionStringConfig.ConnectionString);
        }

        private SqlConnectionProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
