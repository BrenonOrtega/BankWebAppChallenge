using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using BankTestAPI.Dtos;
using BankTestAPI.Models;

namespace BankTestAPI.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> Deposit(TransactionDto transactionDto);
        Task<IEnumerable<TransactionDto>> GetAllByUser(int accountId);
        Task<IEnumerable<TransactionDto>> GetByDate(DateTime transactionsDate);
        Task<bool> Transfer(int sourceAccountId, int destinationAccountId, decimal value);
        Task<bool> Withdraw(TransactionDto transactionDto);
    }
}