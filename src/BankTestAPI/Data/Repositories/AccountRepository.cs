using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BankTestAPI.Models;
using BankTestAPI.Data.Repositories.Interfaces;

namespace BankTestAPI.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankContext _context;

        public AccountRepository(BankContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Account entity)
        {
            try
            {
                await _context.Accounts.AddAsync(entity);
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
                _context.Accounts.Remove(_context.Accounts.Single(x => x.Id.Equals(id)));
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await Task.FromResult(_context.Accounts.ToList());
        }

        public async Task<Account> GetById(int id)
        {
            try
            {
                return await Task.FromResult(_context.Accounts.Single(account => account.Id.Equals(id)));
            }
            catch
            {
                return new Account();
            }
        }

        public async Task<bool> Update(Account updatedEntity)
        {
            try
            {
                var toBeUpdatedEntity = _context.Accounts.Single(account => account.Id.Equals(updatedEntity.Id));
                toBeUpdatedEntity = updatedEntity;
                            
                _context.Accounts.Update(toBeUpdatedEntity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Account> GetByOwnerId(int ownerId)
        {
            try
            {
                var account = _context.Accounts.Single(account => account.Owner.Id == ownerId);
                return await Task.FromResult(account);
            }
            catch 
            {
                return new Account();
            }

        }
    }
}