using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BankTestAPI.Dtos;
using BankTestAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BankTestAPI.Services.Interfaces;

namespace BankTestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserServices _services;

        public UsersController(ILogger<UsersController> logger, IUserServices services)
        {
            _logger = logger;
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _services.GetAllUsers();
            
            Func<IActionResult> actionResult = users?.Count() > 0 
                ? () => Ok(users) 
                : () => NoContent();
            
            return actionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _services.GetUserById(id);
            return user.Id > 0 ? Ok(user) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserDto userDto)
        {
            var executed = await _services.RegisterUser(userDto);
            return executed 
                ? CreatedAtAction(nameof(Post), new { userDto.Id }, userDto)
                : Problem();
        }
        

    }
}
