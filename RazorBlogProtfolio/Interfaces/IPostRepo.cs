using RazorBlogProtfolio.Models;

namespace RazorBlogProtfolio.Interfaces
{
    public interface IPostRepo
    {
        Task DeletePostAsync(Guid postID);
        Task<List<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIDAsync(Guid postID);
        Task<List<Tag>> GetTagsByPostIDAsync(Guid postID);
        Task AddTagToPostAsync(Tag tag, Guid postID);
        Task RemoveTagFromPostAsync(Tag tag, Guid postID);
    }
}
