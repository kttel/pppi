using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace MyLabProject
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Author> Authors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Author)
                .WithMany(a => a.Posts)
                .HasForeignKey(p => p.AuthorId);

            modelBuilder.Entity<Author>()
                .HasMany(a => a.Posts)
                .WithOne(p => p.Author);
        }
    }
}
