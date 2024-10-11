using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using RazorBlogProtfolio.Models;
using RazorBlogProtfolio.Interfaces;

public class BlogPostDetailsModel : PageModel
{
    private readonly IPostRepo _postRepo;

    public BlogPost BlogPost { get; set; }

    public BlogPostDetailsModel(IPostRepo postRepo)
    {
        _postRepo = postRepo;
    }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        BlogPost = await _postRepo.GetPostByIDAsync(id) as BlogPost;

        if (BlogPost == null)
        {
            return NotFound();
        }

        return Page();
    }
}
