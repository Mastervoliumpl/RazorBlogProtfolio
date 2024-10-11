using RazorBlogProtfolio.Models;

namespace RazorBlogProtfolio.Interfaces
{
    public interface IAuthorRepo
    {
        Task<Guid> AddAuthorAsync(string firstName, string lastName, string username, string password, bool isPrivileged);
        Task<List<Author>> GetAllAuthorsAsync();
    }
}
