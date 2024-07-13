using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Persistence;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskStatusController : ControllerBase
    {
        private readonly IProjectTaskStatusService _projectTaskStatusService;

        public ProjectTaskStatusController(IProjectTaskStatusService projectTaskStatusService)
        {
            _projectTaskStatusService = projectTaskStatusService;
        }

        // GET: api/ProjectTaskStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTaskStatus>>> GetProjectTaskStatuses()
        {
            return Ok(await _projectTaskStatusService.GetAllTaskStatusAsync());
        }

        // GET: api/ProjectTaskStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTaskStatus>> GetProjectTaskStatus(int id)
        {
            var projectTaskStatus = await _projectTaskStatusService.GetTaskStatusByIdAsync(id);

            if (projectTaskStatus == null)
            {
                return NotFound();
            }

            return projectTaskStatus;
        }

        // PUT: api/ProjectTaskStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectTaskStatus(int id, ProjectTaskStatus projectTaskStatus)
        {
            await _projectTaskStatusService.UpdateTaskStatusAsync(projectTaskStatus);

            return NoContent();
        }

        // POST: api/ProjectTaskStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectTaskStatus>> PostProjectTaskStatus(ProjectTaskStatus projectTaskStatus)
        {
            await _projectTaskStatusService.InsertTaskStatusAsync(projectTaskStatus);

            return CreatedAtAction("GetProjectTaskStatus", new { id = projectTaskStatus.StatusId }, projectTaskStatus);
        }

        // DELETE: api/ProjectTaskStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectTaskStatus(int id)
        {
            await _projectTaskStatusService.DeleteTaskStatusAsync(id);

            return NoContent();
        }
    }
}
