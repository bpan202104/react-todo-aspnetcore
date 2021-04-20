using System.Data;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace todolist.api.DAL.Providers
{


    public class MySqlConnectionProvider : IDbConnectionProvider
    {
        private readonly string _connectionString;
 
        public MySqlConnectionProvider(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public MySqlConnectionProvider(IOptions<DbConnectionProviderSettings> settings)
        {
            _connectionString = settings.Value.ConnectionString;
        }        
 
        public IDbConnection GetDbConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}