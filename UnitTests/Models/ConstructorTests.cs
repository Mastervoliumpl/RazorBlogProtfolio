using RazorBlogProtfolio.Models;

namespace UnitTests.Models;

public class ConstructorTests
{
    [Fact]
    public void BlogPostConstructorTest()
    {
        // Arange
        string title = "Title1";
        string bodyText = "Body text";
        Author author = new("FirstName", "LastName");

        // Act
        BlogPost blogPost = new(title, author, bodyText);

        // Assert
        Assert.Equal(title, blogPost.Title);
        Assert.Equal(bodyText, blogPost.BodyText);
        Assert.Equal(author, blogPost.Author);
    }

    [Fact]
    public void PortfolioConstructorTest()
    {
        // Arrange
        string title = "Title1";
        string description = "Description";
        Author author = new("FirstName", "LastName");

        // Act
        Portfolio portfolio = new(title, description, author);

        // Assert
        Assert.Equal(title, portfolio.Title);
        Assert.Equal(description, portfolio.Description);
        Assert.Equal(author, portfolio.Author);
    }

    [Fact]
    public void TagConstructorTest()
    {
        // Arrange
        string name = "TagName";

        // Act
        Tag tag = new(name);

        // Assert
        Assert.Equal(name, tag.TagName);
    }
}