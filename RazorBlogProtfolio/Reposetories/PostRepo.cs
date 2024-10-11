using RazorBlogProtfolio.Models;
using RazorBlogProtfolio.Interfaces;
using RazorBlogProtfolio.Helpers;
using System.Data;
using System.Data.SqlClient;

namespace RazorBlogProtfolio.Reposetories
{
    public class PostRepo : IPostRepo
    {
        private readonly IDatabaseHelper _databaseHelper;

        public PostRepo(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public async Task DeletePostAsync(Guid postID)
        {
            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                using (SqlCommand cmd = _databaseHelper.GetCommand("SoftDeletePost", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PostID", postID);

                    await connection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            List<Post> posts = new List<Post>();

            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                using (SqlCommand cmd = _databaseHelper.GetCommand("GetAllPosts", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await connection.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int postType = (int)reader["PostType"];
                            Post post;

                            if (postType == 0) // BlogPost
                            {
                                post = new BlogPost(
                                    reader["Title"].ToString(),
                                    new Author(
                                        (Guid)reader["AuthorID"],
                                        (DateTimeOffset)reader["DateCreated"],
                                        reader["FirstName"].ToString(),
                                        reader["LastName"].ToString(),
                                        reader["Username"].ToString(),
                                        reader["Password"].ToString(),
                                        (bool)reader["IsPrivileged"]
                                    ),
                                    reader["BodyText"].ToString()
                                )
                                {
                                    PostID = (Guid)reader["PostID"],
                                    DateCreated = (DateTimeOffset)reader["DateCreated"],
                                    DateModified = (DateTimeOffset)reader["DateModified"]
                                };
                            }
                            else // Portfolio
                            {
                                post = new Portfolio(
                                    reader["Title"].ToString(),
                                    reader["Description"].ToString(),
                                    new Author(
                                        (Guid)reader["AuthorID"],
                                        (DateTimeOffset)reader["DateCreated"],
                                        reader["FirstName"].ToString(),
                                        reader["LastName"].ToString(),
                                        reader["Username"].ToString(),
                                        reader["Password"].ToString(),
                                        (bool)reader["IsPrivileged"]
                                    )
                                )
                                {
                                    PostID = (Guid)reader["PostID"],
                                    DateCreated = (DateTimeOffset)reader["DateCreated"],
                                    DateModified = (DateTimeOffset)reader["DateModified"]
                                };
                            }

                            posts.Add(post);
                        }
                    }
                }
            }

            return posts;
        }


        public async Task<Post> GetPostByIDAsync(Guid postID)
        {
            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                using (SqlCommand cmd = _databaseHelper.GetCommand("GetPostByID", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PostID", postID);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            int postType = (int)reader["PostType"];
                            Post post;

                            Author author = new Author(
                                (Guid)reader["AuthorID"],
                                (DateTimeOffset)reader["DateCreated"],
                                reader["FirstName"].ToString(),
                                reader["LastName"].ToString(),
                                reader["Username"].ToString(),
                                reader["Password"].ToString(),
                                (bool)reader["IsPrivileged"]
                            );

                            if (postType == 0) // BlogPost
                            {
                                post = new BlogPost(
                                    reader["Title"].ToString(),
                                    author,
                                    reader["BodyText"].ToString()
                                )
                                {
                                    PostID = (Guid)reader["PostID"],
                                    DateCreated = (DateTimeOffset)reader["DateCreated"],
                                    DateModified = (DateTimeOffset)reader["DateModified"]
                                };
                            }
                            else // Portfolio
                            {
                                post = new Portfolio(
                                    reader["Title"].ToString(),
                                    reader["Description"].ToString(),
                                    author
                                )
                                {
                                    PostID = (Guid)reader["PostID"],
                                    DateCreated = (DateTimeOffset)reader["DateCreated"],
                                    DateModified = (DateTimeOffset)reader["DateModified"]
                                };
                            }

                            return post;
                        }
                    }
                }
            }

            return null;
        }


        public async Task<List<Tag>> GetTagsByPostIDAsync(Guid postID)
        {
            List<Tag> tags = new List<Tag>();

            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                using (SqlCommand cmd = _databaseHelper.GetCommand("GetTagsByPostID", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PostID", postID);
                    await connection.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Tag tag = new Tag
                            {
                                TagID = (Guid)reader["TagID"],
                                TagName = reader["TagName"].ToString()
                            };
                            tags.Add(tag);
                        }
                    }
                }
            }

            return tags;
        }

        public async Task AddTagToPostAsync(Tag tag, Guid postID)
        {
            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                using (SqlCommand cmd = _databaseHelper.GetCommand("AddTagToPost", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TagName", tag.TagName);
                    cmd.Parameters.AddWithValue("@PostID", postID);

                    await connection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task RemoveTagFromPostAsync(Tag tag, Guid postID)
        {
            using (SqlConnection connection = _databaseHelper.GetConnection())
            {
                using (SqlCommand cmd = _databaseHelper.GetCommand("RemoveTagFromPost", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TagID", tag.TagID);
                    cmd.Parameters.AddWithValue("@PostID", postID);

                    await connection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}