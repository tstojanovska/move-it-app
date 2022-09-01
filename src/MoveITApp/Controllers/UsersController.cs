using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoveITApp.Services.Interfaces;
using MoveITApp.Shared.CustomExceptions;
using MovieITApp.Dtos.Users;
using System.Net;

namespace MoveITApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous] //no token needed (we can not be logged in before registration)
        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                await _userService.RegisterUser(registerUserDto);
                return StatusCode(StatusCodes.Status201Created, "User created!");
            }
            catch (BadDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [AllowAnonymous] //no token needed (we can not be logged in before login)
        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginDto)
        {
            try
            {
                string token = await _userService.LoginUser(loginDto);
                return Ok(token);
            }
            catch (BadDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

    }
}
