using RazorBlogProtfolio.Interfaces;
using RazorBlogProtfolio.Models;
using RazorBlogProtfolio.Reposetories;
using Moq;

namespace UnitTests.Repositories
{
    public class BlogPostRepoTests
    {
        private readonly BlogPostRepo _blogPostRepo;

        /// <summary>
        /// Creates a mock of the IPostRepo interface. This allows to verify that the BlogPostRepo interacts with its dependencies correctly without needing implementation of IPostRepo.
        /// </summary>
        private readonly Mock<IPostRepo> _mockPostRepo;

        public BlogPostRepoTests()
        {
            _mockPostRepo = new Mock<IPostRepo>();
            _blogPostRepo = new BlogPostRepo(_mockPostRepo.Object); // Creates a new instance of the BlogPostRepo class and passes the mock object of IPostRepo as a parameter. (Injecting a mock object of the IPostRepo interface into it.)
        }

        [Fact]
        public void CreateBlogPost_ShouldAddBlogPostToPostRepo()
        {
            // Arrange
            var author = new Author("John", "Doe");
            string title = "Test Blog Post";
            string bodyText = "This is a test blog post body.";

            // Act
            _blogPostRepo.CreateBlogPost(title, bodyText, author);

            // Assert
            /// <summary>
            /// It.IsAny<T>() is used in the here to check that the AddPost method was called with any BlogPost object.
            /// Verify checks if the AddPost method was called exactly once through the mocked interface.
            /// </summary>
            _mockPostRepo.Verify(pr => pr.AddPost(It.IsAny<BlogPost>()), Times.Once);
        }

        [Fact]
        public void EditBlogPost_ShouldEditExistingBlogPost()
        {
            // Arrange
            var author = new Author("John", "Doe");
            string originalTitle = "Original Blog Post";
            string originalBodyText = "Original body text.";
            string updatedTitle = "Updated Blog Post";
            string updatedBodyText = "Updated body text.";

            var blogPost = new BlogPost(originalTitle, author, originalBodyText);
            Guid postId = blogPost.PostID;

            _mockPostRepo.Setup(pr => pr.GetPostByID(postId)) // The .Setup method takes a lambda expression that specifies which method or property of the mocked object to configure, and then allows to define what should happen when that method or property is called.
                .Returns(blogPost); // (List<Post> { blogPost }) Setup the GetPostByID method to return the blogPost object with PostID. (Runs GetPostByID without the actual implementation)

            // Act
            _blogPostRepo.EditBlogPost(updatedTitle, updatedBodyText, postId);

            // Assert
            Assert.Equal(updatedTitle, blogPost.Title);
            Assert.Equal(updatedBodyText, blogPost.BodyText);
            Assert.True(blogPost.DateModified > blogPost.DateCreated);
        }
    }
}