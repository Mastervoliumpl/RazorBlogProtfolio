namespace RazorBlogProtfolio.Models
{
    public class BlogPost : Post
    {
        public string BodyText { get; set; }

        public BlogPost(string title, Author author, string bodyText) : base(author)
        {
            Title = title;
            BodyText = bodyText;
            PostType = PostType.BlogPost;
        }

    }
}
