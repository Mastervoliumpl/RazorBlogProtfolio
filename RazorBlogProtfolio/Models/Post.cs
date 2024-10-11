using System;
using System.Collections.Generic;

namespace RazorBlogProtfolio.Models
{
    public abstract class Post
    {
        public Guid PostID { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public Author Author { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public string Title { get; set; }
        public PostType PostType { get; set; }
        public Guid? UpdatedBy { get; set; }

        // Protected constructor
        protected Post(string title, Author author)
        {
            Title = title;
            Author = author;
            DateCreated = DateTimeOffset.UtcNow;
            DateModified = DateTimeOffset.UtcNow;
        }

        // Empty constructor for deserialization/database object fetching
        protected Post() { }
    }

    public enum PostType
    {
        BlogPost = 0,
        Portfolio = 1
    }
}
