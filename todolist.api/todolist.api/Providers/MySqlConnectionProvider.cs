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
        
        public MySqlConnectionProvider()
        {
            _connectionString = "server=127.0.0.1;database=todoapp;user ID=root;password=my-secret-pw;port=3306;";
        }        
 
        public IDbConnection GetDbConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}