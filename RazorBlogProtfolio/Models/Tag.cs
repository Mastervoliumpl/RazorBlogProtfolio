namespace RazorBlogProtfolio.Models
{
    public class Tag(string name)
    {
        public Guid TagID { get; init; } = Guid.NewGuid();
        public string TagName { get; set; } = name;
    }
}
