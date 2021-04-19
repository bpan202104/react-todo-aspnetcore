using System.Data;
using MySql.Data.MySqlClient;

namespace todolist.api.Providers
{
    public interface IDbConnectionProvider
    {
        IDbConnection GetDbConnection();
    }

    public class MySqlConnectionProvider : IDbConnectionProvider
    {
        private readonly string _connectionString;
 
        public MySqlConnectionProvider(string connectionString)
        {
            _connectionString = connectionString;
        }
 
        public IDbConnection GetDbConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}