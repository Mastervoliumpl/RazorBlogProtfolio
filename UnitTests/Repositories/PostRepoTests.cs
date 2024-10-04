using RazorBlogProtfolio.Interfaces;
using RazorBlogProtfolio.Models;
using RazorBlogProtfolio.Reposetories;

namespace UnitTests.Repositories
{
    public class PostRepoTests
    {
        private readonly PostRepo _postRepo;
        private readonly Author _testAuthor;

        public PostRepoTests()
        {
            _postRepo = new PostRepo();
            _testAuthor = new Author("John", "Doe");
        }

        [Fact]
        public void AddPost_ShouldAddPostToList()
        {
            // Arrange
            var post = new BlogPost("Test Post", _testAuthor, "This is a test blog post body.");

            // Act
            _postRepo.AddPost(post);

            // Assert
            Assert.Single(_postRepo.GetAllPosts());
            Assert.Equal(post, _postRepo.GetPostByID(post.PostID));
        }

        [Fact]
        public void DeletePost_ShouldRemovePostFromList()
        {
            // Arrange
            var post = new BlogPost("Test Post", _testAuthor, "This is a test blog post body.");
            _postRepo.AddPost(post);

            // Act
            _postRepo.DeletePost(post.PostID);

            // Assert
            Assert.Empty(_postRepo.GetAllPosts());
            Assert.Null(_postRepo.GetPostByID(post.PostID));
        }

        [Fact]
        public void GetPostByID_ShouldReturnCorrectPost()
        {
            // Arrange
            var post = new BlogPost("Test Post", _testAuthor, "This is a test blog post body.");
            _postRepo.AddPost(post);

            // Act
            var result = _postRepo.GetPostByID(post.PostID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(post, result);
        }

        [Fact]
        public void AddTagToTagList_ShouldAddTagToPost()
        {
            // Arrange
            var post = new BlogPost("Test Post", _testAuthor, "This is a test blog post body.");
            var tag = new Tag("Test Tag");
            _postRepo.AddPost(post);

            // Act
            _postRepo.AddTagToTagList(tag, post.PostID);

            // Assert
            Assert.Single(_postRepo.GetTagsByPostID(post.PostID));
            Assert.Equal(tag, _postRepo.GetTagsByPostID(post.PostID)[0]);
        }

        [Fact]
        public void RemoveTagFromList_ShouldRemoveTagFromPost()
        {
            // Arrange
            var post = new BlogPost("Test Post", _testAuthor, "This is a test blog post body.");
            var tag = new Tag("Test Tag");
            _postRepo.AddPost(post);
            post.Tags.Add(tag);

            // Act
            _postRepo.RemoveTagFromList(tag, post.PostID);

            // Assert
            Assert.Empty(_postRepo.GetTagsByPostID(post.PostID));
        }

        [Fact]
        public void GetTagsByPostID_ShouldReturnTagsForPost()
        {
            // Arrange
            var post = new BlogPost("Test Post", _testAuthor, "This is a test blog post body.");
            var tag1 = new Tag("Tag 1");
            var tag2 = new Tag("Tag 2");
            _postRepo.AddPost(post);
            post.Tags.Add(tag1);
            post.Tags.Add(tag2);

            // Act
            var tags = _postRepo.GetTagsByPostID(post.PostID);

            // Assert
            Assert.Equal(2, tags.Count);
            Assert.Contains(tag1, tags);
            Assert.Contains(tag2, tags);
        }
    }
}