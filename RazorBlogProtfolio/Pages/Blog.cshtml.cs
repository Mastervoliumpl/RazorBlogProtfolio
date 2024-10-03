using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorBlogProtfolio.Models;
using RazorBlogProtfolio.Interfaces;

public class BlogModel : PageModel
{
    private readonly IPostRepo _postRepo;
    
    public List<BlogPostViewModel> BlogPosts { get; set; }

    public BlogModel(IPostRepo postRepo)
    {
        _postRepo = postRepo;
    }

    public void OnGet()
    {
        var posts = _postRepo.GetAllPosts().Where(p => p.PostType == PostType.BlogPost).Cast<BlogPost>().ToList();

        // Assume a default timezone (e.g., America/New_York)
        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        BlogPosts = posts.Select(post => new BlogPostViewModel
        {
            Title = post.Title,
            BodyText = post.BodyText,
            DateCreated = TimeZoneInfo.ConvertTimeFromUtc(post.DateCreated.UtcDateTime, userTimeZone),
            Author = post.Author,
            PostID = post.PostID
        }).ToList();
    }
}

public class BlogPostViewModel
{
    public string Title { get; set; }
    public string BodyText { get; set; }
    public DateTime DateCreated { get; set; }
    public Author Author { get; set; }
    public Guid PostID { get; set; }
}
