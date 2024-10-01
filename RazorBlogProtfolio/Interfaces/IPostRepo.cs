using RazorBlogProtfolio.Models;

namespace RazorBlogProtfolio.Interfaces
{
    public interface IPostRepo
    {
        void AddPost(Post post);
        void DeletePost(Guid postID);
        List<Post> GetAllPosts();
        Post GetPostByID(Guid postID);
        List<Tag> GetTagsByPostID(Guid postID);
        void AddTagToTagList(Tag tag, Guid postID);
        void RemoveTagFromList(Tag tag, Guid postID);
    }
}
