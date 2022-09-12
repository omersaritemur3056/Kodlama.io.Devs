using Application.Features.Users.Commands.CreateUsers;
using Application.Features.Users.DTOs;
using Application.Features.Users.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand registerUserCommand)
        {
            CreatedUserDto result = await Mediator.Send(registerUserCommand);
            return Created("", result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginQuery userLoginQuery)
        {
            AccessTokenDto result = await Mediator.Send(userLoginQuery);
            return Created("", result);
        }
    }
}
