using Azure;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLabProject
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime dateofCreation { get; set; } = DateTime.Now;
        public Author Author { get; set; }
        public int AuthorId { get; set; } = 0;
        public Post(int id, string title, string body, int authorId = 0)
        {
            Id = id;
            Title = title;
            Body = body;
            dateofCreation = DateTime.Now;
            AuthorId = authorId;
        }
    }
}
