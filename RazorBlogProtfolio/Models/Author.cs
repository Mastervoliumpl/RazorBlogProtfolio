namespace RazorBlogProtfolio.Models
{
    public class Author(string firstName, string lastName)
    {
        public Guid AuthorID { get; init; } = Guid.NewGuid();
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public string Password { get; set; }
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.Now;
    }
}
