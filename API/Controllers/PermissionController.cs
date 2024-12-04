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
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        //[HttpGet]
        //public async Task<ActionResult<Permission>> GetPermissionById(int id)
        //{
        //    return Ok( await _permissionService.GetPermissionByIdAsync(id));
        //}

        [HttpGet("user")]
        public async Task<ActionResult<Permission>> GetPermissionByUserId(int userId, int teamId)
        {
            return Ok(await _permissionService.GetPermissionByUserIdAsync(userId, teamId));
        }

        [HttpPut("user")]
        public async Task<ActionResult> UpdateUsersPermissionAsync(int userId, int teamId, int permissionId)
        {
            return Ok(await _permissionService.UpdateUsersPermissionAsync(userId, teamId, permissionId));
        }

        [HttpGet]
        public async Task<ActionResult<Permission>> GetPermissions()
        {
            return Ok(await _permissionService.GetPermissionsAsync());
        }
    }
}
