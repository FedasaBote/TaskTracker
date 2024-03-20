using BlogApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BlogApi.Data
{
    public class BlogDbContext : DbContext
    {

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=blogApi;Username=postgres;Password=fedasagete");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var commentEntity = modelBuilder.Entity<Comment>();
            var commentHasOne = commentEntity.HasOne(c => c.Post);         // Each Comment belongs to one Post
            var commentWithMany = commentHasOne.WithMany(p => p.Comments);  // Each Post can have many Comments
            var commentHasForeignKey = commentWithMany.HasForeignKey(c => c.PostId);  // Foreign key property on Comment

        }
    }
}
