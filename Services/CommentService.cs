using BlogApi.Data;
using BlogApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogApi.Services
{
        public class CommentService
        {
            private readonly BlogDbContext _context;

            public CommentService(BlogDbContext context)
            {
                _context = context;
            }

            public (Comment,int) AddComment(int postId, string text)
            {
            // Validate input
            Console.WriteLine("We are here");
            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    return (null, StatusCodes.Status400BadRequest);
                }

                // Check if the post exists
                Console.WriteLine($"{postId},{text}");
                var post = _context.Posts.FirstOrDefault(p => p.Id == postId);
                Console.WriteLine(post);
                if (post == null)
                {
                    //throw new ArgumentException($"Post with ID {postId} not found.");
                    return (null, StatusCodes.Status404NotFound);
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
                return (comment, StatusCodes.Status201Created);
            }catch (Exception ex)
            {
                Console.WriteLine($"Error adding comment: {ex.InnerException.Message}");
                return (null, StatusCodes.Status500InternalServerError);
            }
            }

            public (List<Comment>,int) GetCommentsForPost(int postId)
            {
            try
            {
                var comments = _context.Comments.Where(c => c.PostId == postId).ToList();
                return (comments, StatusCodes.Status200OK);
              }catch(Exception e) { 
                return (null, StatusCodes.Status500InternalServerError);
            }
            }

            public (Comment,int) UpdateComment(int id, string text)
            {
            // Retrieve comment
            try
            {
                var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
                if (comment == null)
                {
                    return (null, StatusCodes.Status404NotFound);
                }

                // Validate input
                if (string.IsNullOrEmpty(text))
                {
                    return (null, StatusCodes.Status400BadRequest);
                }

                // Update comment
                comment.Text = text;
                _context.SaveChanges();
                return (comment, StatusCodes.Status200OK);
            }catch(Exception ex)
            {
                return (null, StatusCodes.Status500InternalServerError);
            }
            }

            public int DeleteComment(int id)
            {
            // Retrieve comment
            try
            {
                var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
                if (comment == null)
                {
                    return StatusCodes.Status404NotFound;
                }

                // Delete comment
                _context.Comments.Remove(comment);
                _context.SaveChanges();
                return StatusCodes.Status200OK;
            }catch(Exception ex)
            {
                return StatusCodes.Status500InternalServerError;
            }
            }
        }
    }


