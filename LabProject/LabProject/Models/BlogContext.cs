using Microsoft.EntityFrameworkCore;

namespace LabProject
{
    public class BlogContext : DbContext
    {
        // not used at the moment
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        { }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
    }
}
