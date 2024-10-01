using RazorBlogProtfolio.Interfaces;
using RazorBlogProtfolio.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorBlogProtfolio.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IBlogPostRepo IblogPostRepoInterface;
        private IPostRepo IPostRepoInterface;

        public List<BlogPost> postsList { get; set; } = new();

        public IndexModel(ILogger<IndexModel> logger, IBlogPostRepo BlogRepo, IPostRepo PostRepo)
        {
            _logger = logger;
            IblogPostRepoInterface = BlogRepo;
            IPostRepoInterface = PostRepo;
        }

        public void OnGet()
        {
            IblogPostRepoInterface.CreateBlogPost("SomeRandomTitle", "This is my really long bodytext of a blog post", new Author("FirstName", "LastName"));
            IblogPostRepoInterface.CreateBlogPost("SomeRandomTitle2", "This is my really long bodytext of a blog post2", new Author("FirstName", "LastName"));
            IblogPostRepoInterface.CreateBlogPost("SomeRandomTitle342", "This is my really long bodytext of a blog post4234", new Author("FirstName", "LastName"));
            var temporaryPostList = IPostRepoInterface.GetAllPosts();
            postsList = temporaryPostList.Select(post => post as BlogPost).ToList();
        }
    }
}
