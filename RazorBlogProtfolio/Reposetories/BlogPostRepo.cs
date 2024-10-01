using RazorBlogProtfolio.Models;
using RazorBlogProtfolio.Interfaces;

namespace RazorBlogProtfolio.Reposetories
{
    public class BlogPostRepo(IPostRepo postRepo) : IBlogPostRepo
    {
        private readonly IPostRepo _postRepo = postRepo;

        public void CreateBlogPost(string title, string bodyText, Author author)
        {
            // Create a new BlogPost
            BlogPost blogPost = new(title, author, bodyText);
            _postRepo.AddPost(blogPost);
        }

        public void EditBlogPost(string title, string bodyText, Guid postID)
        {
            // Find the BlogPost
            BlogPost? blogPost = _postRepo.GetPostByID(postID) as BlogPost; // Cast to BlogPost (same as (BlogPost)_postRepo... )

            // Edit the BlogPost
            blogPost.Title = title;
            blogPost.BodyText = bodyText;
            blogPost.DateModified = DateTimeOffset.Now;
        }
    }
}
