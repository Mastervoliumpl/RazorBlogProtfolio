namespace RazorBlogProtfolio.Models
{
    public class BlogPost : Post
    {
        public string BodyText { get; set; }

        // Constructor for creating a new blog post
        public BlogPost(string title, Author author, string bodyText) : base(title, author)
        {
            BodyText = bodyText;
            PostType = PostType.BlogPost;
        }

        // Empty constructor for deserialization/database object fetching
        public BlogPost() { }
    }
}
