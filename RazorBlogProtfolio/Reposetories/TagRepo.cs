using RazorBlogProtfolio.Models;
using RazorBlogProtfolio.Interfaces;
using RazorBlogProtfolio.Helpers;
using System.Data;
using System.Data.SqlClient;

namespace RazorBlogProtfolio.Reposetories
{
    public class TagRepo : ITagRepo
    {
        private readonly IDatabaseHelper _databaseHelper;

        public TagRepo(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public async Task CreateTagAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Tag name cannot be null or empty.");
            }

            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                using (SqlCommand command = _databaseHelper.GetCommand("CreateTag", connection))
                {
                    command.Parameters.AddWithValue("@TagName", name);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteTagAsync(Tag tag)
        {
            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                using (SqlCommand command = _databaseHelper.GetCommand("DeleteTag", connection))
                {
                    command.Parameters.AddWithValue("@TagID", tag.TagID);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<Tag>> GetTagsAsync()
        {
            List<Tag> tags = new List<Tag>();
            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                using (SqlCommand command = _databaseHelper.GetCommand("SELECT * FROM Tag", connection, CommandType.Text))
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            tags.Add(new Tag
                            {
                                TagID = reader.GetGuid(reader.GetOrdinal("TagID")),
                                TagName = reader.GetString(reader.GetOrdinal("TagName"))
                            });
                        }
                    }
                }
            }
            return tags;
        }

        public async Task<Tag> GetTagByIDAsync(Guid tagID)
        {
            Tag tag = null;
            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                using (SqlCommand command = _databaseHelper.GetCommand("SELECT * FROM Tag WHERE TagID = @TagID", connection, CommandType.Text))
                {
                    command.Parameters.AddWithValue("@TagID", tagID);
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            tag = new Tag
                            {
                                TagID = reader.GetGuid(reader.GetOrdinal("TagID")),
                                TagName = reader.GetString(reader.GetOrdinal("TagName"))
                            };
                        }
                    }
                }
            }
            return tag;
        }
    }
}
