using Blog.Data;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Core
{
    public class PostManager
    {
        private readonly BlogDbContext _context;

        public PostManager(BlogDbContext context)
        {
            _context = context;
        }

        public void CreatePost(string title, string content)
        {
            // Validate input
            try
            {
                Console.WriteLine("Creating post...");
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
                {
                    throw new ArgumentException("Title and content cannot be empty.");
                }
                Console.WriteLine("Input validated.");

                // Create post
                var post = new Post
                {
                    Title = title,
                    Content = content,
                    Created = DateTime.UtcNow
                };

                // Add post to database
                _context.Posts.Add(post);
                _context.SaveChanges();
                Console.WriteLine($"Post with ID {post.PostId} created.");
            }catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    // Print the inner exception details
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public Post GetPost(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.PostId == id);
        }

        public void UpdatePost(int id, string title, string content)
        {
            // Retrieve post
            var post = _context.Posts.FirstOrDefault(p => p.PostId == id);
            if (post == null)
            {
                throw new ArgumentException($"Post with ID {id} not found.");
            }

            // Validate input
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
            {
                throw new ArgumentException("Title and content cannot be empty.");
            }

            // Update post
            post.Title = title;
            post.Content = content;
            _context.SaveChanges();
        }

        public void DeletePost(int id)
        {
            // Retrieve post
            var post = _context.Posts.FirstOrDefault(p => p.PostId == id);
            if (post == null)
            {
                throw new ArgumentException($"Post with ID {id} not found.");
            }

            // Delete post
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }
    }
}
