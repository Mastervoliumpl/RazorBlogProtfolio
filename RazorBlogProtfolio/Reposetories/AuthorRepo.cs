using RazorBlogProtfolio.Models;
using RazorBlogProtfolio.Interfaces;
using RazorBlogProtfolio.Helpers;
using System.Data;
using System.Data.SqlClient;

namespace RazorBlogProtfolio.Reposetories
{
    public class AuthorRepo : IAuthorRepo
    {
        private readonly IDatabaseHelper _databaseHelper;

        public AuthorRepo(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public async Task<Guid> AddAuthorAsync(string firstName, string lastName, string username, string password, bool isPrivileged)
        {
            try
            {
                using (SqlConnection connection = _databaseHelper.GetConnection())
                {
                    using (SqlCommand cmd = _databaseHelper.GetCommand("AddAuthor", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Set output parameters
                        SqlParameter authorIDParam = new SqlParameter("@AuthorID", SqlDbType.UniqueIdentifier)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(authorIDParam);

                        SqlParameter dateCreatedParam = new SqlParameter("@DateCreated", SqlDbType.DateTimeOffset)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(dateCreatedParam);

                        // Set input parameters
                        cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = firstName;
                        cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = lastName;
                        cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username;
                        cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 254).Value = password;
                        cmd.Parameters.Add("@IsPrivileged", SqlDbType.Bit).Value = isPrivileged;

                        await connection.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        // Get/Return the output AuthorID
                        return (Guid)authorIDParam.Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred while adding an author", ex);
            }
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            List<Author> authors = new List<Author>();

            try
            {
                using (SqlConnection connection = _databaseHelper.GetConnection())
                {
                    using (SqlCommand cmd = _databaseHelper.GetCommand("GetAllAuthors", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Author author = new Author(
                                    (Guid)reader["AuthorID"],
                                    (DateTimeOffset)reader["DateCreated"],
                                    reader["FirstName"].ToString(),
                                    reader["LastName"].ToString(),
                                    reader["Username"].ToString(),
                                    reader["Password"].ToString(),
                                    (bool)reader["IsPrivileged"]
                                );
                                authors.Add(author);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred while retrieving authors.", ex);
            }

            return authors;
        }
    }
}