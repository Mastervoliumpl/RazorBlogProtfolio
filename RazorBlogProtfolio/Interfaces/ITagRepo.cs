using RazorBlogProtfolio.Models;

namespace RazorBlogProtfolio.Interfaces
{
    public interface ITagRepo
    {
        Task CreateTagAsync(string name);
        Task DeleteTagAsync(Tag tag);
        Task<List<Tag>> GetTagsAsync();
        Task<Tag> GetTagByIDAsync(Guid tagID);
    }
}
