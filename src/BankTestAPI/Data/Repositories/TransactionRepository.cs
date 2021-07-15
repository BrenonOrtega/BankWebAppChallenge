using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BankTestAPI.Models;
using BankTestAPI.Data.Repositories.Interfaces;

namespace BankTestAPI.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankContext _context;

        public TransactionRepository(BankContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAll()
        {
            return await Task.FromResult(_context.Transactions.ToList());
        }

        public async Task<IEnumerable<Transaction>> GetByDate(DateTime transactionDate)
        {
            var transactions = _context.Transactions.Where(transaction =>
                transaction.CreatedAt.Date.Equals(transactionDate.Date)
            );

            return await Task.FromResult(transactions);
        }

        public async Task<Transaction> GetById(int id)
        {
            try
            {
                return await Task.FromResult(_context.Transactions.Single(account => account.Id.Equals(id)));
            }
            catch
            {
                return new Transaction();
            }
        }

        public async Task<bool> Create(Transaction entity)
        {
            try
            {
                await _context.Transactions.AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Transaction updatedEntity)
        {
            try
            {
                var toBeUpdatedEntity = _context.Transactions.Single(account => account.Id.Equals(updatedEntity.Id));
                toBeUpdatedEntity = updatedEntity;

                _context.Update(toBeUpdatedEntity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                _context.Transactions.Remove(_context.Transactions.Single(x => x.Id.Equals(id)));
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Transaction>> GetByAccountId(int accountId)
        {
            var userTransactions = _context.Transactions.Where(transaction => accountId.Equals(transaction.Account.Id));
            return await Task.FromResult(userTransactions);
        }
    }
}