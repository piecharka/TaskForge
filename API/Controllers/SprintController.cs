using Application.Interfaces.Services;
using Application.Services;
using Domain;
using Domain.DTOs;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SprintController : ControllerBase
    {
        private readonly ISprintService _sprintService;

        public SprintController(ISprintService sprintService)
        {
            _sprintService = sprintService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sprint>> GetSprintById(int id)
        {
            return Ok(await _sprintService.GetSprintByIdAsync(id));
        }

        [HttpGet("team-current/{teamId}")]
        public async Task<ActionResult<Sprint>> GetCurrentSprint(int teamId)
        {
            return Ok(await _sprintService.GetCurrentSprintTeamAsync(teamId));
        }

        [HttpGet("team/{teamId}")]
        public async Task<ActionResult<IEnumerable<Sprint>>> GetTeamsSprints(int teamId)
        {
            return Ok(await _sprintService.GetSprintsAsync(teamId));
        }

        [HttpPost]
        public async Task<ActionResult> AddSprint(SprintDto sprintDto)
        {
            await _sprintService.AddSprintAsync(sprintDto);

            return CreatedAtAction("AddSprint", new { id = sprintDto.SprintId }, sprintDto);
        }

        [HttpDelete("{sprintId}")]
        public async Task<ActionResult> DeleteSprint(int sprintId)
        {
            await _sprintService.DeleteSprintAsync(sprintId);

            return NoContent();
        }
    }
}
