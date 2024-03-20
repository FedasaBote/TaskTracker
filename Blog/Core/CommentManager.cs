using Blog.Data;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Core
{
    public class CommentManager
    {
        private readonly BlogDbContext _context;

        public CommentManager(BlogDbContext context)
        {
            _context = context;
        }

        public void AddComment(int postId, string text)
        {
            // Validate input
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Comment text cannot be empty.");
            }

            // Check if the post exists
            var post = _context.Posts.FirstOrDefault(p => p.PostId == postId);
            if (post == null)
            {
                throw new ArgumentException($"Post with ID {postId} not found.");
            }

            // Create comment
            var comment = new Comment
            {
                PostId = postId,
                Text = text,
                CreatedAt = DateTime.UtcNow
            };

            // Add comment to database
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public List<Comment> GetCommentsForPost(int postId)
        {
            return _context.Comments.Where(c => c.PostId == postId).ToList();
        }

        public void UpdateComment(int id, string text)
        {
            // Retrieve comment
            var comment = _context.Comments.FirstOrDefault(c => c.CommentId == id);
            if (comment == null)
            {
                throw new ArgumentException($"Comment with ID {id} not found.");
            }

            // Validate input
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Comment text cannot be empty.");
            }

            // Update comment
            comment.Text = text;
            _context.SaveChanges();
        }

        public void DeleteComment(int id)
        {
            // Retrieve comment
            var comment = _context.Comments.FirstOrDefault(c => c.CommentId == id);
            if (comment == null)
            {
                throw new ArgumentException($"Comment with ID {id} not found.");
            }

            // Delete comment
            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }
    }
}
