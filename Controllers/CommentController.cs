using BlogApi.Models;
using BlogApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/posts/{postId}/comments")]
    public class CommentController :ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public ActionResult<Comment> AddComment(int postId,[FromBody] string commentdto)
        {
            Console.WriteLine("We are here");
            var (comment, statusCode) = _commentService.AddComment(postId, commentdto);
            return StatusCode(statusCode, comment);
        }

        [HttpGet]
        public ActionResult<List<Comment>> GetCommentsForPost(int postId)
        {
            var (comments, statusCode) = _commentService.GetCommentsForPost(postId);
            return StatusCode(statusCode, comments);
        }

        [HttpPut("{id}")]
        public ActionResult<Comment> UpdateComment(int id,[FromBody] string text)
        {
            var (updatedComment, statusCode) = _commentService.UpdateComment(id,text);
            return StatusCode(statusCode, updatedComment);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            var statusCode = _commentService.DeleteComment(id);
            return StatusCode(statusCode);
        }
    }
}
