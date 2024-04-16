using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Persistence;
using Domain.Interfaces.Services;
using Application.Services;
using Application.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
        {
            return Ok(await _userService.GetUsersAsync());  
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserAsync(int id)
        {
            return Ok(await _userService.GetUserByIdAsync(id));
        }

        // PUT: api/Users/updatePassword
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("/updatePassword")]
        public async Task<IActionResult> UpdateUserPasswordAsync([FromBody] UserUpdatePasswordDto user)
        {
            await _userService.UpdateUserPasswordAsync(user);
           
            return NoContent();
        }

        [HttpPut("/updateEmail")]
        public async Task<IActionResult> UpdateUserEmailAsync([FromBody] UserUpdateEmailDto user)
        {
            await _userService.UpdateUserEmailAsync(user);

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUserAsync(UserCreateDto user)
        {
            await _userService.InsertUserAsync(user);

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            await _userService.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
