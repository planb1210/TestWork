using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace TestWorkPhysicon.DataBase
{
    public interface IDBContext
    {
        IDbConnection CreateConnection();
    }

    public class DBContext : IDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Пустая строка подключения");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
