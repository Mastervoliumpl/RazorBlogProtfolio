using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorBlogProtfolio.Interfaces;
using RazorBlogProtfolio.Models;
using System.ComponentModel.DataAnnotations;

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
    [Required(ErrorMessage = "Please select a post type.")]
    public string SelectedPostType { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(20, ErrorMessage = "Title cannot be longer than 20 characters.")]
    public string Title { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Body text is required for BlogPost.")]
    public string BodyText { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Description is required for Portfolio.")]
    public string Description { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Author first name is required.")]
    [StringLength(20, ErrorMessage = "First name cannot be longer than 20 characters.")]
    public string AuthorFirstName { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Author last name is required.")]
    [StringLength(20, ErrorMessage = "Last name cannot be longer than 20 characters.")]
    public string AuthorLastName { get; set; }

    // Triggered after the form is submitted
    public IActionResult OnPost()
    {
        if (SelectedPostType == "BlogPost")
        {
            // Remove validation for Description
            ModelState.Remove(nameof(Description));
        }
        else if (SelectedPostType == "Portfolio")
        {
            // Remove validation for BodyText
            ModelState.Remove(nameof(BodyText));
        }

        if (!ModelState.IsValid)
        {
            // If the model state is invalid, show the form with validation errors
            return Page();
        } 
        else
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
}