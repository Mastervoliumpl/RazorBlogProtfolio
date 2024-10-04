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
        var blogPosts = _postRepo.GetAllPosts().Where(p => p.PostType == PostType.BlogPost).Cast<BlogPost>().ToList();

        BlogPosts = blogPosts.Select(post => new BlogPostViewModel
        {
            Title = post.Title,
            BodyText = post.BodyText,
            DateCreated = post.DateCreated,
            Author = post.Author,
            PostID = post.PostID
        }).ToList();
    }
}

public class BlogPostViewModel
{
    public string Title { get; set; }
    public string BodyText { get; set; }
    public DateTimeOffset DateCreated { get; set; }
    public Author Author { get; set; }
    public Guid PostID { get; set; }
}
