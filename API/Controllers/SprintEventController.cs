using Application.Interfaces.Services;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SprintEventController : ControllerBase
    {
        private readonly ISprintEventService _sprintEventService;
        public SprintEventController(ISprintEventService sprintEventService)
        {
            _sprintEventService = sprintEventService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SprintEvent>>> GetSprintEvents()
        {
            return Ok(await _sprintEventService.GetSprintEventsAsync());
        }

        [HttpGet("team/{teamId}")]
        public async Task<ActionResult<IEnumerable<SprintEvent>>> GetSprintEventsByTeamId(int teamId)
        {
            return Ok(await _sprintEventService.GetSprintEventsByTeamIdAsync(teamId));
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<SprintEvent>>> GetSprintEventsByUserId(int userId)
        {
            return Ok(await _sprintEventService.GetSprintEventsByUserIdAsync(userId));
        }

        [HttpGet("team/closest/{teamId}")]
        public async Task<ActionResult<IEnumerable<SprintEvent>>> GetClosestThreeEvents(int teamId)
        {
            return Ok(await _sprintEventService.GetClosestThreeEventsAsync(teamId));
        }
    }
}
