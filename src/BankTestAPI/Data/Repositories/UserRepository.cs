using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BankTestAPI.Models;
using BankTestAPI.Data.Repositories.Interfaces;

namespace BankTestAPI.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BankContext _context;

        public UserRepository(BankContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(User entity)
        {
            try
            {
                await _context.Users.AddAsync(entity);
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
                _context.Users.Remove(_context.Users.Single(x => x.Id.Equals(id)));
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.FromResult(_context.Users.ToList());
        }

        public async Task<User> GetById(int id)
        {
            try
            {
                var user = _context.Users.Single(account => account.Id.Equals(id));
                return await Task.FromResult(user);
            }
            catch
            {
                return new User();
            }
        }

        public async Task<User> GetByAccountId(int id)
        {
            try
            {
                var user = _context.Users.Single(user => user.Account.Id.Equals(id));
                return await Task.FromResult(user);
            }
            catch 
            {
                return new User();
            }
        }

        public async Task<bool> Update(User updatedEntity)
        {
            try
            {
                var toBeUpdatedEntity = _context.Users.Single(account => account.Id.Equals(updatedEntity.Id));
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
    }
}