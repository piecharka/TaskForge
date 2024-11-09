﻿using Application.Interfaces.Services;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
    }
}
