using Application.DTOs;
using Application.Interfaces.Services;
using Application.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.DTOs;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // GET: api/Teams
        [HttpGet("{taskId}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetTaskComments(int taskId)
        {
            return Ok(await _commentService.GetTasksCommentsAsync(taskId));
        }

        [HttpPost]
        public async Task<ActionResult> PostComment(CommentInsertDto comment)
        {
            await _commentService.AddCommentAsync(comment);

            return CreatedAtAction("PostComment", new { id = comment.CommentId }, comment);
        }
    }
}
