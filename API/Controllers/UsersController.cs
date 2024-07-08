using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Persistence;
using Application.Services;
using Application.DTOs;
using Persistence.Repositories;
using Application.Interfaces.Services;
using Domain.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;

        public UsersController(IUserService userService, IPasswordHasher passwordHasher)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserGetDto>>> GetUsersAsync()
        {
            return Ok(await _userService.GetUsersAsync());  
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGetDto>> GetUserAsync(int id)
        {
            return Ok(await _userService.GetUserByIdAsync(id));
        }

        // PUT: api/Users/updatePassword
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("/updatePassword")]
        public async Task<IActionResult> UpdateUserPasswordAsync([FromBody] UserUpdatePasswordDto userUpdatePasswordData)
        {
            await _userService.UpdateUserPasswordAsync(userUpdatePasswordData);
           
            return NoContent();
        }

        [HttpPut("/updateEmail")]
        public async Task<IActionResult> UpdateUserEmailAsync([FromBody] UserUpdateEmailDto userUpdateEmailData)
        {
            await _userService.UpdateUserEmailAsync(userUpdateEmailData);

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUserAsync(UserCreateDto userCreateData)
        {
            var user = await _userService.GetUserByNameAsync(userCreateData.Username);

            if (user == null)
            {
                await _userService.InsertUserAsync(userCreateData);
            }

            return CreatedAtAction("GetUser", new { id = userCreateData.UserId }, userCreateData);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            await _userService.DeleteUserAsync(id);

            return NoContent();
        }


        [HttpPost("/login")]
        public async Task<ActionResult<int>> LoginAsync([FromBody] UserLoginDto userLoginData)
        {
            int userId = await _userService.LoginUserAsync(userLoginData);

            if (userId != -1)
            {
                return userId;
            }
            else
            {
                return NoContent();
            }
        }
    }
}
