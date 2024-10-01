using RazorBlogProtfolio.Models;

namespace RazorBlogProtfolio.Interfaces
{
    public interface IBlogPostRepo
    {
        void CreateBlogPost(string title, string bodyText, Author author);
        void EditBlogPost(string title, string bodyText, Guid postID);
    }
}
