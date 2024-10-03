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

    public void OnGet(Guid id)
    {
        // Fetch the post using the passed id
        Post = _postRepo.GetPostByID(id);
    }

    public IActionResult OnPost(Guid id)
    {
        _postRepo.DeletePost(id);
        return RedirectToPage("/Index");
    }
}
