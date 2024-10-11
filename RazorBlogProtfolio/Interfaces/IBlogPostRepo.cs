using RazorBlogProtfolio.Models;

namespace RazorBlogProtfolio.Interfaces
{
    public interface IBlogPostRepo
    {
        Task CreateBlogPostAsync(string title, string bodyText, Author author);
        Task EditBlogPostAsync(string title, string bodyText, Guid postID);
    }
}
