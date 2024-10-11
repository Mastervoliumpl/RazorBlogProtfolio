using RazorBlogProtfolio.Models;

namespace RazorBlogProtfolio.Interfaces
{
    public interface IPortfolioRepo
    {
        Task CreatePortfolioPostAsync(string title, string description, Author author);
        Task EditPortfolioPostAsync(string title, string description, Guid postID);
    }
}
