using RazorBlogProtfolio.Models;
using RazorBlogProtfolio.Reposetories;

namespace UnitTests.Repositories;

public class TagRepoTests
{
    private readonly TagRepo _tagRepo;

    /// <summary>
    ///  Creates a new instance of TagRepo for each test, this ensure that the tests are independent of each other and do not interfere with each other's state.
    /// </summary>
    public TagRepoTests()
    {
        _tagRepo = new TagRepo();
    }

    [Fact]
    public void CreateTag_ShouldAddTagToList()
    {
        // Arrange
        string tagName = "Test Tag";

        // Act
        _tagRepo.CreateTag(tagName);

        // Assert
        Assert.Single(_tagRepo.GetTags());
        Assert.Equal(tagName, _tagRepo.GetTags()[0].TagName);
    }

    [Fact]
    public void CreateTag_ShouldThrowArgumentException_WhenNameIsNullOrEmpty()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => _tagRepo.CreateTag(null)); // Lambda defies anonymous function in which CreatTag is ran with null as an argument
        Assert.Throws<ArgumentException>(() => _tagRepo.CreateTag(string.Empty)); // Same thing but empty string
    }

    [Fact]
    public void DeleteTag_ShouldRemoveTagFromList()
    {
        // Arrange
        var tagName = "Test Tag";
        _tagRepo.CreateTag(tagName); // Create the tag first
        var tag = _tagRepo.GetTags()[0]; // Get the tag from the list

        // Act
        _tagRepo.DeleteTag(tag);

        // Assert
        Assert.Empty(_tagRepo.GetTags());
    }

    [Fact]
    public void DeleteTag_ShouldThrowArgumentException_WhenTagDoesNotExist()
    {
        // Arrange
        var nonExistentTag = new Tag("Non-existent Tag"); // Create a tag that doesn't exist in TagRepo

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _tagRepo.DeleteTag(nonExistentTag)); // Tried to use the tag that does not exist in TagRepo to delete a tag in TagRepo
    }

    [Fact]
    public void GetTags_ShouldReturnAllTags()
    {
        // Arrange
        _tagRepo.CreateTag("Tag 1");
        _tagRepo.CreateTag("Tag 2");

        // Act
        var tags = _tagRepo.GetTags();

        // Assert
        Assert.Equal(2, tags.Count);
    }

    [Fact]
    public void GetTagByID_ShouldReturnCorrectTag()
    {
        // Arrange
        var tagName = "Test Tag";
        _tagRepo.CreateTag(tagName);
        var tag = _tagRepo.GetTags()[0];

        // Act
        var result = _tagRepo.GetTagByID(tag.TagID);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(tag.TagID, result.TagID);
    }
}