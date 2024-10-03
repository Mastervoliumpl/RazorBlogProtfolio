using RazorBlogProtfolio.Interfaces;
using RazorBlogProtfolio.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorBlogProtfolio.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IBlogPostRepo _blogPostRepo;
        private IPortfolioRepo _portfolioRepo;
        private IPostRepo _postRepo;

        public List<BlogPost> BlogPostsList { get; set; } = new();
        public List<Portfolio> PortfolioPostsList { get; set; } = new();

        public IndexModel(ILogger<IndexModel> logger, IBlogPostRepo blogPostRepo, IPostRepo postRepo, IPortfolioRepo portfolioRepo)
        {
            _logger = logger;
            _blogPostRepo = blogPostRepo;
            _postRepo = postRepo;
            _portfolioRepo = portfolioRepo;
        }

        public void OnGet()
        {
            // Check if there are any existing posts
            if (!_postRepo.GetAllPosts().Any())
            {
                // Blog posts
                _blogPostRepo.CreateBlogPost("Blog Title 1", "This is the body of blog post 1", new Author("Author1", "LastName1"));
                _blogPostRepo.CreateBlogPost("Blog Title 2", "This is the body of blog post 2", new Author("Author2", "LastName2"));
                _blogPostRepo.CreateBlogPost("Blog Title 3", "This is the body of blog post 3", new Author("Author3", "LastName3"));

                // Portfolio posts
                _portfolioRepo.CreatePortfolioPost("Portfolio Title 1", "This is the description of portfolio 1", new Author("Author1", "LastName1"));
                _portfolioRepo.CreatePortfolioPost("Portfolio Title 2", "This is the description of portfolio 2", new Author("Author2", "LastName2"));
                _portfolioRepo.CreatePortfolioPost("Portfolio Title 3", "This is the description of portfolio 3", new Author("Author3", "LastName3"));
            }

            // Retrieve all posts
            var allPosts = _postRepo.GetAllPosts();

            // Filter and assign the posts to their respective lists
            BlogPostsList = allPosts.Where(post => post.PostType == PostType.BlogPost).Cast<BlogPost>().ToList();
            PortfolioPostsList = allPosts.Where(post => post.PostType == PostType.Portfolio).Cast<Portfolio>().ToList();
        }
    }
}
