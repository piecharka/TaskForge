using Application.Interfaces.Services;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeLogController : ControllerBase
    {
        private readonly ITimeLogService _timeLogService;
        public TimeLogController(ITimeLogService timeLogService)
        {
            _timeLogService = timeLogService;
        }

        [HttpGet]
        public async Task<ActionResult<TimeLog>> GetTimeLog(int timeLogId)
        {
            return Ok(await _timeLogService.GetTimeLogAsync(timeLogId));
        }

        [HttpGet("overdue-count/{sprintId}")]
        public async Task<ActionResult<int>> GetOverdueDeadlineLogs(int sprintId)
        {
            return Ok(await _timeLogService.GetOverdueDeadlineCountAsync(sprintId));
        }
    }
}
