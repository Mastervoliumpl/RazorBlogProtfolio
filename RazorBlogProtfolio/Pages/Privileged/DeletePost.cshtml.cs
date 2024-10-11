using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorBlogProtfolio.Interfaces;
using RazorBlogProtfolio.Models;

public class DeletePostModel : PageModel
{
    private readonly IPostRepo _postRepo;

    public DeletePostModel(IPostRepo postRepo)
    {
        _postRepo = postRepo;
    }

    public Post Post { get; set; }

    public async Task OnGetAsync(Guid id)
    {
        // Fetch post using the id
        Post = await _postRepo.GetPostByIDAsync(id);
    }

    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        await _postRepo.DeletePostAsync(id);
        return RedirectToPage("/Index");
    }
}