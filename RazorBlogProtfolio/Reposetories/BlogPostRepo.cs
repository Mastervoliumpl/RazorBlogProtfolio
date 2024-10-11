using RazorBlogProtfolio.Models;
using RazorBlogProtfolio.Interfaces;
using RazorBlogProtfolio.Helpers;
using System.Data;
using System.Data.SqlClient;

namespace RazorBlogProtfolio.Reposetories
{
    public class BlogPostRepo : IBlogPostRepo
    {
        private readonly IDatabaseHelper _databaseHelper;

        public BlogPostRepo(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public async Task CreateBlogPostAsync(string title, string bodyText, Author author)
        {
            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                using (SqlCommand cmd = _databaseHelper.GetCommand("AddBlogPost", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Set parameters
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@BodyText", bodyText);
                    cmd.Parameters.AddWithValue("@AuthorID", author.AuthorID);

                    await connection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task EditBlogPostAsync(string title, string bodyText, Guid postID)
        {
            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                using (SqlCommand cmd = _databaseHelper.GetCommand("EditPost", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Set parameters for the update
                    cmd.Parameters.AddWithValue("@PostID", postID);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@BodyText", bodyText);

                    await connection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
