namespace RazorBlogProtfolio.Models
{
    public class Portfolio : Post
    {
        public string Description { get; set; }

        // Constructor for creating a new portfolio post
        public Portfolio(string title, string description, Author author) : base(title, author)
        {
            Description = description;
            PostType = PostType.Portfolio;
        }

        // Empty constructor for deserialization/database object fetching
        public Portfolio() { }
    }
}
