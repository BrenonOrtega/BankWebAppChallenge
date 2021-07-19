using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BankTestAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BankTestAPI.Services.Interfaces;
using BankTestAPI.Data.Repositories.Interfaces;

namespace BankTestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountServices;
        private readonly IUserRepository _userRepository;

        public AccountController(ILogger<AccountController> logger, IAccountService accountServices, IUserRepository userRepository)
        {
            _logger = logger;
            _accountServices = accountServices;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Account = await _accountServices.GetAllAccounts();

            Func<IActionResult> actionResult = Account?.Count() > 0
                ? () => Ok(Account)
                : () => NoContent();

            return actionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var account = await _accountServices.GetAccountById(id);
            var user = await _userRepository.GetById(account.OwnerId);

            return account.Id > 0 ? Ok(new { account , user }) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AccountDto accountDto)
        {
            var executed = await _accountServices.RegisterAccount(accountDto);
            return executed
                ? CreatedAtAction(nameof(Post), new { accountDto.Id }, accountDto)
                : BadRequest("This user does not exist");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _accountServices.DeleteAccount(id))
                    return NoContent();

                return BadRequest();

            }
            catch (InvalidOperationException ioe)
            {
                return Forbid(ioe.Message);
            }
        }
    }
}