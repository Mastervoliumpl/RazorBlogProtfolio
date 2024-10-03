using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorBlogProtfolio.Interfaces;
using RazorBlogProtfolio.Models;

public class CreatePostModel : PageModel
{
    private readonly IBlogPostRepo _blogPostRepo;
    private readonly IPortfolioRepo _portfolioRepo;

    public CreatePostModel(IBlogPostRepo blogPostRepo, IPortfolioRepo portfolioRepo)
    {
        _blogPostRepo = blogPostRepo;
        _portfolioRepo = portfolioRepo;
    }

    [BindProperty]
    public string SelectedPostType { get; set; }

    [BindProperty]
    public string Title { get; set; }

    [BindProperty]
    public string BodyText { get; set; }

    [BindProperty]
    public string Description { get; set; }

    [BindProperty]
    public string AuthorFirstName { get; set; }

    [BindProperty]
    public string AuthorLastName { get; set; }

    public IActionResult OnPost()
    {
        var author = new Author(AuthorFirstName, AuthorLastName);

        if (SelectedPostType == "BlogPost")
        {
            _blogPostRepo.CreateBlogPost(Title, BodyText, author);
        }
        else if (SelectedPostType == "Portfolio")
        {
            _portfolioRepo.CreatePortfolioPost(Title, Description, author);
        }

        return RedirectToPage("/Index");
    }
}