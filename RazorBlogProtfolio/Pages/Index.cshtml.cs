using RazorBlogProtfolio.Interfaces;
using RazorBlogProtfolio.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorBlogProtfolio.Reposetories;

namespace RazorBlogProtfolio.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBlogPostRepo _blogPostRepo;
        private readonly IPortfolioRepo _portfolioRepo;
        private readonly IPostRepo _postRepo;
        private readonly IAuthorRepo _authorRepo;

        public List<BlogPost> BlogPostsList { get; set; } = new();
        public List<Portfolio> PortfolioPostsList { get; set; } = new();
        private static Author _hardcodedAuthor;

        public IndexModel(ILogger<IndexModel> logger, IBlogPostRepo blogPostRepo, IPostRepo postRepo, IPortfolioRepo portfolioRepo, IAuthorRepo authorRepo)
        {
            _logger = logger;
            _blogPostRepo = blogPostRepo;
            _postRepo = postRepo;
            _portfolioRepo = portfolioRepo;
            _authorRepo = authorRepo;
        }

        public async Task OnGetAsync()
        {
            var allAuthors = await _authorRepo.GetAllAuthorsAsync();
            if (allAuthors.Count == 1)
            {
                _hardcodedAuthor = allAuthors.First();
            }
            else if (!allAuthors.Any())
            {
                await _authorRepo.AddAuthorAsync("Jakub", "Fijalkowski", "JAFI", "password123", true);
                allAuthors = await _authorRepo.GetAllAuthorsAsync();
                _hardcodedAuthor = allAuthors.First();
            }
            else
            {
                throw new Exception("More than one author found, which is unexpected.");
            }

            var allPosts = await _postRepo.GetAllPostsAsync();
            if (!allPosts.Any())
            {
                // Blog posts
                await _blogPostRepo.CreateBlogPostAsync("Blog Title 1", "This is the body of blog post 1", _hardcodedAuthor);
                await _blogPostRepo.CreateBlogPostAsync("Blog Title 2", "This is the body of blog post 2", _hardcodedAuthor);
                await _blogPostRepo.CreateBlogPostAsync("Blog Title 3", "This is the body of blog post 3", _hardcodedAuthor);

                // Portfolio posts
                await _portfolioRepo.CreatePortfolioPostAsync("Portfolio Title 1", "This is the description of portfolio 1", _hardcodedAuthor);
                await _portfolioRepo.CreatePortfolioPostAsync("Portfolio Title 2", "This is the description of portfolio 2", _hardcodedAuthor);
                await _portfolioRepo.CreatePortfolioPostAsync("Portfolio Title 3", "This is the description of portfolio 3", _hardcodedAuthor);

                // Retrieve posts
                allPosts = await _postRepo.GetAllPostsAsync();
            }

            // Assign posts to their respective lists
            BlogPostsList = allPosts.Where(post => post.PostType == PostType.BlogPost).Cast<BlogPost>().ToList();
            PortfolioPostsList = allPosts.Where(post => post.PostType == PostType.Portfolio).Cast<Portfolio>().ToList();
        }
    }
}