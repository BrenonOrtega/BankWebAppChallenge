using System;
using System.Linq;
using System.Threading.Tasks;
using BankTestAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BankTestAPI.Services.Interfaces;
using System.Net;

namespace BankTestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAllUsers();

            Func<IActionResult> actionResult = users?.Count() > 0
                ? () => Ok( users.Select(user => new { user.Id, user.Email, user.FirstName, user.LastName }))
                : () => NoContent();

            return actionResult();
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUserById(id);
            return user.Id > 0 ? Ok(user) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] UserDto userDto)
        {
            var executed = await _userService.RegisterUser(userDto);
            return executed
                ? CreatedAtAction(nameof(Post), new { userDto.Id }, userDto)
                : BadRequest();
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] UserDto userDto)
        {
            if (await _userService.UpdateUser(userDto.Id, userDto))
            {
                return NoContent();
            }
            return BadRequest(new { message = "Unnable to update user", userDto });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _userService.DeleteUser(id))
            {
                return Ok();
            }
            return Problem();
        }
    }
}
