using RazorBlogProtfolio.Models;
using RazorBlogProtfolio.Interfaces;
using RazorBlogProtfolio.Helpers;
using System.Data;
using System.Data.SqlClient;

namespace RazorBlogProtfolio.Reposetories
{
    public class PortfolioRepo : IPortfolioRepo
    {
        private readonly IDatabaseHelper _databaseHelper;

        public PortfolioRepo(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public async Task CreatePortfolioPostAsync(string title, string description, Author author)
        {
            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                using (SqlCommand cmd = _databaseHelper.GetCommand("AddPortfolioItem", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Set parameters
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@AuthorID", author.AuthorID);

                    await connection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task EditPortfolioPostAsync(string title, string description, Guid postID)
        {
            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                using (SqlCommand cmd = _databaseHelper.GetCommand("EditPost", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Set parameters for the update
                    cmd.Parameters.AddWithValue("@PostID", postID);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Description", description);

                    await connection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
