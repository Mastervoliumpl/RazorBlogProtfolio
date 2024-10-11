using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorBlogProtfolio.Interfaces;
using RazorBlogProtfolio.Models;

public class CreatePostModel : PageModel
{
    private readonly IBlogPostRepo _blogPostRepo;
    private readonly IPortfolioRepo _portfolioRepo;
    private readonly IAuthorRepo _authorRepo;

    private Author _author;

    public CreatePostModel(IBlogPostRepo blogPostRepo, IPortfolioRepo portfolioRepo, IAuthorRepo authorRepo)
    {
        _blogPostRepo = blogPostRepo;
        _portfolioRepo = portfolioRepo;
        _authorRepo = authorRepo;
    }

    [BindProperty]
    public string SelectedPostType { get; set; }

    [BindProperty]
    public string Title { get; set; }

    [BindProperty]
    public string BodyText { get; set; }

    [BindProperty]
    public string Description { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        List<Author> allAuthors = await _authorRepo.GetAllAuthorsAsync();
        if (allAuthors.Count == 1)
        {
            _author = allAuthors.First();
        }
        else if (!allAuthors.Any())
        {
            await _authorRepo.AddAuthorAsync("Jakub", "Fijalkowski", "JAFI", "password123", true);
            allAuthors = await _authorRepo.GetAllAuthorsAsync();
            _author = allAuthors.First();
        }
        else
        {
            throw new Exception("More than one author found, which is unexpected.");
        }

        if (SelectedPostType == "BlogPost")
        {
            await _blogPostRepo.CreateBlogPostAsync(Title, BodyText, _author);
        }
        else if (SelectedPostType == "Portfolio")
        {
            await _portfolioRepo.CreatePortfolioPostAsync(Title, Description, _author);
        }

        return RedirectToPage("/Index");
    }
}
