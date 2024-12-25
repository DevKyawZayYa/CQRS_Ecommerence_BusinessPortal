using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient; // Import MySQL library
using System.Data;

namespace BusinessPortal.Persistence.Contexts
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connectionString = _configuration.GetConnectionString("NorthwindConnection") ?? throw new ArgumentNullException("Connection string not found.");
        }

        public IDbConnection CreateConnection() => new MySqlConnection(_connectionString); // Use MySqlConnection
    }
}
