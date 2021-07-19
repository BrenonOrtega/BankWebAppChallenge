using System;
using System.Threading.Tasks;
using BankTestAPI.Dtos;
using BankTestAPI.Models.Enum;
using BankTestAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankTestAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<TransactionsController> _logger;
        public TransactionsController(ITransactionService transactionService, ILogger<TransactionsController> logger)
        {
            _logger = logger;
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DateTime? transactionsDate)
        {
            DateTime queryDate = transactionsDate ?? DateTime.Now.Date;
            var transactions = await _transactionService.GetByDate(queryDate);

            return Ok(transactions);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var userTransactions = await _transactionService.GetAllByUser(userId);

            return Ok(userTransactions);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransactionDto transactionDto)
        {
            Func<TransactionDto, Task<bool>> operation = 
                transactionDto.TransactionType.ToLower() == TransactionType.Credit.ToString().ToLower() 
                ? _transactionService.Deposit 
                : _transactionService.Withdraw;
            
                if (await operation(transactionDto))
                {
                    return CreatedAtAction(nameof(Get), new { transactionDto.AccountId }, transactionDto);
                }

                return BadRequest();
        }

    }
}