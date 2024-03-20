using BlogApi.Models;
using BlogApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BlogApi.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public ActionResult<Post> CreatePost([FromBody] CreatePostDto postDto)
        {
            if (postDto == null || string.IsNullOrEmpty(postDto.Title) || string.IsNullOrEmpty(postDto.Content))
            {
                return BadRequest("Title and content cannot be empty.");
            }

            var (createdPost, statusCode) = _postService.CreatePost(postDto.Title, postDto.Content);
            return StatusCode(statusCode, createdPost);
        }

        [HttpGet]
        public ActionResult<List<Post>> GetAllPosts()
        {
            var (posts, statusCode) = _postService.GetAllPosts();
            return StatusCode(statusCode, posts);
        }

        [HttpGet("{id}")]
        public ActionResult<Post> GetPost(int id)
        {
            var (post, statusCode) = _postService.GetPost(id);
            return StatusCode(statusCode, post);
        }

        [HttpPut("{id}")]
        public ActionResult<Post> UpdatePost(int id, [FromBody] CreatePostDto post)
        {
            var (updatedPost, statusCode) = _postService.UpdatePost(id, post.Title, post.Content);
            return StatusCode(statusCode, updatedPost);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            var statusCode = _postService.DeletePost(id);
            return StatusCode(statusCode);
        }
    }
}
