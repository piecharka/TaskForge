using Application.Interfaces.Services;
using Domain;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SprintEventTypeController : ControllerBase
    {
        private readonly ISprintEventTypeService _sprintEventTypeService;

        public SprintEventTypeController(ISprintEventTypeService sprintEventTypeService)
        {
            _sprintEventTypeService = sprintEventTypeService;
        }

        // GET: api/ProjectTaskTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SprintEventType>>> GetAllSprintEventTypes()
        {
            return Ok(await _sprintEventTypeService.GetAllEventTypes());
        }
    }
}
