using System.Data;

namespace todolist.api.DAL
{
    public interface IDbConnectionProvider
    {
        IDbConnection GetDbConnection();
    }

    public class DbConnectionProviderSettings
    {
        public string ConnectionString { get; set; }
    }
}