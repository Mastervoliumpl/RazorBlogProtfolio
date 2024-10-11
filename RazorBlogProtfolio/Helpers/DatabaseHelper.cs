using System.Data;
using System.Data.SqlClient;

namespace RazorBlogProtfolio.Helpers
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RazorBlogPortfolio");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public SqlCommand GetCommand(string commandText, SqlConnection connection, CommandType commandType = CommandType.StoredProcedure)
        {
            var command = new SqlCommand(commandText, connection)
            {
                CommandType = commandType
            };
            return command;
        }
    }
}

