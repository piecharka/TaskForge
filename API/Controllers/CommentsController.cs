using Application.Interfaces.Services;
using Application.Services;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
    }
}
