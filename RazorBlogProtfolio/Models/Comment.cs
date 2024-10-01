namespace RazorBlogProtfolio.Models
{
    public class Comment(string commentText, Author author)
    {
        public Guid CommentID { get; set; } = Guid.NewGuid();
        public string CommentText { get; set; } = commentText;
        public Author Author { get; set; } = author;
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset DateModified { get; set; } = DateTimeOffset.Now;
    }
}
