using Application.DTOs;
using Application.Interfaces.Services;
using Application.Services;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IAccountService _accountService;

        public AccountController(IJwtTokenService jwtTokenService, IAccountService accountService)
        {
            _jwtTokenService = jwtTokenService;
            _accountService = accountService;
        }

        [HttpPost("/login")]
        public async Task<ActionResult> LoginAsync([FromBody] UserLoginDto userLoginData)
        {

            var user = await _accountService.LoginUserAsync(userLoginData);

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(new { token = _jwtTokenService.GenerateToken(user) });
        }

        [HttpPost("/register")]
        public async Task<ActionResult> Register([FromBody] UserRegisterDto userRegisterData)
        {
            if(!await _accountService.ValidateEmailAsync(userRegisterData.Email))
            {
                return BadRequest("Email already exists");
            }

            if(!await _accountService.ValidateUsernameAsync(userRegisterData.Username))
            {
                return BadRequest("Username already exists");
            }
            
            var user = await _accountService.RegisterUserAsync(userRegisterData);

            return Ok(new { token = _jwtTokenService.GenerateToken(user) });
        }
    }
}
