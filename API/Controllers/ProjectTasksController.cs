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
using Application.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTasksController : ControllerBase
    {
        private readonly IProjectTaskService _projectTaskService;

        public ProjectTasksController(IProjectTaskService projectTaskService)
        {
            _projectTaskService = projectTaskService;
        }

        // GET: api/ProjectTasks
        [HttpGet("{teamId}")]
        public async Task<ActionResult<IEnumerable<ProjectTask>>> GetProjectTasks(int teamId)
        {
            return Ok(await _projectTaskService.GetAllProjectTasksInTeamAsync(teamId));
        }

        //// GET: api/ProjectTasks/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<ProjectTask>> GetProjectTask(int id)
        //{
        //    var projectTask = await _context.ProjectTasks.FindAsync(id);

        //    if (projectTask == null)
        //    {
        //        return NotFound();
        //    }

        //    return projectTask;
        //}

        //// PUT: api/ProjectTasks/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProjectTask(int id, ProjectTask projectTask)
        //{
        //    if (id != projectTask.TaskId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(projectTask).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProjectTaskExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/ProjectTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectTask>> PostProjectTask(ProjectTaskInsertDto projectTaskInsert)
        {
            await _projectTaskService.AddProjectTaskAsync(projectTaskInsert);

            return CreatedAtAction("GetTeam", new { id = projectTaskInsert.TaskId }, projectTaskInsert);
        }

        // DELETE: api/ProjectTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectTask(int id)
        {
            await _projectTaskService.DeleteProjectTaskAsync(id);

            return NoContent();
        }
    }
}
