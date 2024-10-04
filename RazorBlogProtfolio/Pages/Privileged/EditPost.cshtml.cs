using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorBlogProtfolio.Interfaces;
using RazorBlogProtfolio.Models;
using System.ComponentModel.DataAnnotations;

public class EditPostModel : PageModel
{
    private readonly IPostRepo _postRepo;
    private readonly IBlogPostRepo _blogPostRepo;
    private readonly IPortfolioRepo _portfolioRepo;

    public EditPostModel(IPostRepo postRepo, IBlogPostRepo blogPostRepo, IPortfolioRepo portfolioRepo)
    {
        _postRepo = postRepo;
        _blogPostRepo = blogPostRepo;
        _portfolioRepo = portfolioRepo;
    }

    [BindProperty]
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(20, ErrorMessage = "Title cannot be longer than 20 characters.")]
    public string Title { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Body Text is required for BlogPost.")]
    public string BodyText { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Description is required for Portfolio.")]
    public string Description { get; set; }

    public Post Post { get; set; }

    public IActionResult OnGet(Guid id)
    {
        // Fetch the post using the passed id
        Post = _postRepo.GetPostByID(id);

        if (Post == null)
        {
            return RedirectToPage("/NotFound"); // Or handle it as per your needs
        }

        // Set the form fields based on the post type
        Title = Post.Title;

        if (Post.PostType == PostType.BlogPost)
        {
            BodyText = ((BlogPost)Post).BodyText;
        }
        else if (Post.PostType == PostType.Portfolio)
        {
            Description = ((Portfolio)Post).Description;
        }

        return Page();
    }

    public IActionResult OnPost(Guid id)
    {
        // Fetch the post again to ensure that the post exists, and isn't null
        Post = _postRepo.GetPostByID(id);

        if (Post == null)
        {
            return RedirectToPage("/Error");
        }

        // Remove validation for irrelevant fields
        if (Post.PostType == PostType.BlogPost)
        {
            ModelState.Remove(nameof(Description));
        }
        else if (Post.PostType == PostType.Portfolio)
        {
            ModelState.Remove(nameof(BodyText));
        }

        if (!ModelState.IsValid)
        {
            // Redisplay the form with validation errors
            return Page();
        }

        // Proceed with saving the changes
        if (Post.PostType == PostType.BlogPost)
        {
            var blogPost = (BlogPost)Post;
            blogPost.Title = Title;
            blogPost.BodyText = BodyText;
            _blogPostRepo.EditBlogPost(blogPost.Title, blogPost.BodyText, blogPost.PostID);
        }
        else if (Post.PostType == PostType.Portfolio)
        {
            var portfolioPost = (Portfolio)Post;
            portfolioPost.Title = Title;
            portfolioPost.Description = Description;
            _portfolioRepo.EditPortfolioPost(portfolioPost.Title, portfolioPost.Description, portfolioPost.PostID);
        }

        return RedirectToPage("/Index");
    }
}
