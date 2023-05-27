using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLabProject
{
    public class Author
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime dateofRegister { get; set; } = DateTime.Now;
        public Boolean IsActive { get; set; } = true;
        public Boolean IsAdmin { get; set; } = false;
        public Author(int id, string username, string email)
        {
            Id = id;
            Username = username;
            Email = email;
            dateofRegister = DateTime.Now;
            IsAdmin = false;
            IsActive = true;
        }
        public IList<Post> Posts { get; set; } = new List<Post>();
    }
}
