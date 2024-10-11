using RazorBlogProtfolio.Interfaces;
using System;

namespace RazorBlogProtfolio.Models
{
    public class Author
    {
        public Guid AuthorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsPrivileged { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public bool IsDeleted { get; set; }

        public Author(Guid authorID, DateTimeOffset dateTimeOffset, string firstName, string lastName, string username, string password, bool isPrivileged)
        {
            AuthorID = authorID;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            IsPrivileged = isPrivileged;
            DateCreated = dateTimeOffset;
            IsDeleted = false;
        }

        // Parameterless constructor for database deserialization
        public Author() { }
    }
}
