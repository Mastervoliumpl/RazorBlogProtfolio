using RazorBlogProtfolio.Models;

namespace RazorBlogProtfolio.Interfaces
{
    public interface IPortfolioRepo
    {
        void CreatePortfolioPost(string title, string description, Author author);
        void EditPortfolioPost(string title, string description, Guid postID);
    }
}
