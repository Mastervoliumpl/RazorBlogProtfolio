using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorBlogProtfolio.Models;
using RazorBlogProtfolio.Interfaces;

public class PortfolioModel : PageModel
{
    private readonly IPostRepo _postRepo;

    public List<PortfolioViewModel> PortfolioItems { get; set; }

    public PortfolioModel(IPostRepo postRepo)
    {
        _postRepo = postRepo;
    }

    public async Task OnGetAsync()
    {
        var posts = (await _postRepo.GetAllPostsAsync())
                    .Where(p => p.PostType == PostType.Portfolio)
                    .Cast<Portfolio>()
                    .ToList();

        // Assume a default timezone
        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");

        PortfolioItems = posts.Select(post => new PortfolioViewModel
        {
            Title = post.Title,
            Description = post.Description,
            DateCreated = TimeZoneInfo.ConvertTimeFromUtc(post.DateCreated.UtcDateTime, userTimeZone),
            Author = post.Author,
            PostID = post.PostID
        }).ToList();
    }
}

public class PortfolioViewModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateCreated { get; set; }
    public Author Author { get; set; }
    public Guid PostID { get; set; }
}