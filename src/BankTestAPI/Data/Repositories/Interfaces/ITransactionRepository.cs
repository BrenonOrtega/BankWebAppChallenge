using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankTestAPI.Models;

namespace BankTestAPI.Data.Repositories.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetByDate(DateTime transactionDate);
        
        Task<IEnumerable<Transaction>> GetByAccountId(int accountId);
    }
}