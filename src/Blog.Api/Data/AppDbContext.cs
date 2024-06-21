using Microsoft.EntityFrameworkCore;
using Blog.Api.Models;

namespace Blog.Api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; } 
        public DbSet<User> Users { get; set; } 

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Author)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.AuthorId);
        }
    }
}