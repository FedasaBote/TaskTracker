using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data
{
    public class BlogDbContext :DbContext
    {

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=blog;Username=postgres;Password=fedasagete");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            var commentEntity = modelBuilder.Entity<Comment>();
            var commentHasOne = commentEntity.HasOne(c => c.Post);         // Each Comment belongs to one Post
            var commentWithMany = commentHasOne.WithMany(p => p.Comments);  // Each Post can have many Comments
            var commentHasForeignKey = commentWithMany.HasForeignKey(c => c.PostId);  // Foreign key property on Comment

        }
    }

    
}
