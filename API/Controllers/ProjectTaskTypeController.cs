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
using Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskTypeController : ControllerBase
    {
        private readonly IProjectTaskTypeService _projectTaskTypeService;

        public ProjectTaskTypeController(IProjectTaskTypeService projectTaskTypeService)
        {
            _projectTaskTypeService = projectTaskTypeService;
        }

        // GET: api/ProjectTaskTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTaskType>>> GetProjectTaskTypes()
        {
            return Ok(await _projectTaskTypeService.GetAllTaskTypesAsync());
        }

        // GET: api/ProjectTaskTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTaskType>> GetProjectTaskType(int id)
        {
            var projectTaskType = await _projectTaskTypeService.GetTaskTypeByIdAsync(id);

            if (projectTaskType == null)
            {
                return NotFound();
            }

            return projectTaskType;
        }

        // PUT: api/ProjectTaskTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectTaskType(int id, ProjectTaskType projectTaskType)
        {
            await _projectTaskTypeService.UpdateTaskTypeAsync(projectTaskType);

            return NoContent();
        }

        // POST: api/ProjectTaskTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectTaskType>> PostProjectTaskType(ProjectTaskType projectTaskType)
        {
            await _projectTaskTypeService.InsertTaskTypeAsync(projectTaskType);

            return CreatedAtAction("GetProjectTaskType", new { id = projectTaskType.TypeId }, projectTaskType);
        }

        // DELETE: api/ProjectTaskTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectTaskType(int id)
        {
            await _projectTaskTypeService.DeleteTaskTypeAsync(id);

            return NoContent();
        }
    }
}
