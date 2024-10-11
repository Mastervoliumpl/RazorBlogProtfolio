using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorBlogProtfolio.Interfaces;
using RazorBlogProtfolio.Models;

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
    public string Title { get; set; }

    [BindProperty]
    public string BodyText { get; set; }

    [BindProperty]
    public string Description { get; set; }

    public Post Post { get; set; }

    public async Task OnGetAsync(Guid id)
    {
        // Fetch the post using the passed id
        Post = await _postRepo.GetPostByIDAsync(id);

        if (Post.PostType == PostType.BlogPost)
        {
            Title = Post.Title;
            BodyText = ((BlogPost)Post).BodyText;
        }
        else if (Post.PostType == PostType.Portfolio)
        {
            Title = Post.Title;
            Description = ((Portfolio)Post).Description;
        }
    }

    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        // Fetch post again cause it was modified
        var post = await _postRepo.GetPostByIDAsync(id);

        if (post.PostType == PostType.BlogPost)
        {
            BlogPost blogPost = (BlogPost)post;
            blogPost.Title = Title;
            blogPost.BodyText = BodyText;
            await _blogPostRepo.EditBlogPostAsync(blogPost.Title, blogPost.BodyText, blogPost.PostID);
        }
        else if (post.PostType == PostType.Portfolio)
        {
            Portfolio portfolioPost = (Portfolio)post;
            portfolioPost.Title = Title;
            portfolioPost.Description = Description;
            await _portfolioRepo.EditPortfolioPostAsync(portfolioPost.Title, portfolioPost.Description, portfolioPost.PostID);
        }

        return RedirectToPage("/Index");
    }
}