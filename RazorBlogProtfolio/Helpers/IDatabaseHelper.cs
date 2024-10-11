using System.Data;
using System.Data.SqlClient;

namespace RazorBlogProtfolio.Helpers
{
    public interface IDatabaseHelper
    {
        SqlConnection GetConnection();
        SqlCommand GetCommand(string commandText, SqlConnection connection, CommandType commandType = CommandType.StoredProcedure);
    }
}
