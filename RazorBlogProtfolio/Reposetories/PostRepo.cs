using RazorBlogProtfolio.Models;
using RazorBlogProtfolio.Interfaces;

namespace RazorBlogProtfolio.Reposetories
{
    public class PostRepo : IPostRepo
    {
        private readonly List<Post> _posts = [];

        public void AddPost(Post post)
        {
            _posts.Add(post);
        }

        public void DeletePost(Guid postID)
        {
            // Find the post and delete the post
            _posts.Remove(_posts.Find(p => p.PostID == postID));
        }

        public List<Post> GetAllPosts()
        {
            return _posts;
        }

        public Post GetPostByID(Guid postID)
        {
            return _posts.Find(p => p.PostID == postID);
        }

        public List<Tag> GetTagsByPostID(Guid postID)
        {
            Post? post = GetPostByID(postID);
            return post?.Tags;
        }

        public void AddTagToTagList(Tag tag, Guid postID)
        {
            Post? post = GetPostByID(postID);
            post?.Tags.Add(tag);
        }

        public void RemoveTagFromList(Tag tag, Guid postID)
        {
            Post? post = GetPostByID(postID);
            post?.Tags.Remove(tag);
        }
    }
}
