using Application.Interfaces.Services;
using Application.Services;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;
        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet("team/{id}")]
        public async Task<ActionResult<int>> AverageTaskCountPerSprint(int teamId)
        {
            return Ok(await _analyticsService.AverageTasksCountPerSprint(teamId));
        }

        [HttpGet("team/user/{id}")]
        public async Task<ActionResult<int>> AverageTaskCountPerUser(int teamId)
        {
            return Ok(await _analyticsService.AverageTaskCountPerUser(teamId));
        }

        [HttpGet("team/task/{id}")]
        public async Task<ActionResult<double>> AverageTaskTimeForTask(int teamId)
        {
            return Ok(await _analyticsService.AverageTimeForTask(teamId));
        }
    }
}
