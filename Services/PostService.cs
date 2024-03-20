using BlogApi.Data;
using BlogApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogApi.Services
{
    public class PostService
    {
        private readonly BlogDbContext _context;

        public PostService(BlogDbContext context)
        {
            _context = context;
        }

        public (Post, int) CreatePost(string title, string content)
        {
            try
            {
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
                {
                    return (null, StatusCodes.Status400BadRequest);
                }

                var post = new Post
                {
                    Title = title,
                    Content = content,
                    Created = DateTime.UtcNow
                };

                _context.Posts.Add(post);
                _context.SaveChanges();

                return (post, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating post: {ex.InnerException.Message}");
                return (null, StatusCodes.Status500InternalServerError);
            }
        }

        public (List<Post>, int) GetAllPosts()
        {
            try
            {
                var posts = _context.Posts.ToList();
                return (posts, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving posts: {ex.Message}");
                return (null, StatusCodes.Status500InternalServerError);
            }
        }

        public (Post, int) GetPost(int id)
        {
            try
            {
                var post = _context.Posts.FirstOrDefault(p => p.Id == id);
                if (post == null)
                {
                    return (null, StatusCodes.Status404NotFound);
                }
                return (post, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving post: {ex.Message}");
                return (null, StatusCodes.Status500InternalServerError);
            }
        }

        public (Post, int) UpdatePost(int id, string title, string content)
        {
            try
            {
                var post = _context.Posts.FirstOrDefault(p => p.Id == id);
                if (post == null)
                {
                    return (null, StatusCodes.Status404NotFound);
                }

                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
                {
                    return (null, StatusCodes.Status400BadRequest);
                }

                post.Title = title;
                post.Content = content;
                _context.SaveChanges();

                return (post, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating post: {ex.Message}");
                return (null, StatusCodes.Status500InternalServerError);
            }
        }

        public int DeletePost(int id)
        {
            try
            {
                var post = _context.Posts.FirstOrDefault(p => p.Id == id);
                if (post == null)
                {
                    return StatusCodes.Status404NotFound;
                }

                _context.Posts.Remove(post);
                _context.SaveChanges();

                return StatusCodes.Status204NoContent;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting post: {ex.Message}");
                return StatusCodes.Status500InternalServerError;
            }
        }
    }
}
