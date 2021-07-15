using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BankTestAPI.Dtos;
using BankTestAPI.Models;
using BankTestAPI.Data.Repositories.Interfaces;
using AutoMapper;

namespace BankTestAPI.Services
{
    public class AccountServices
   {
        private readonly IUserRepository _users;
        private readonly IAccountRepository _accounts;
        private readonly Mapper _mapper;
        public AccountServices(IUserRepository users, IAccountRepository accounts, Mapper mapper)
        {
            _users = users;
            _accounts = accounts;
            _mapper = mapper;
        }

        public async Task RegisterAccount(AccountDto accountDto)
        {
            var account = _mapper.Map<Account>(accountDto);
            account.Owner = await _users.GetByAccountId(accountDto.Id);

            await _accounts.Create(account);
        }

        public async Task<AccountDto> GetAccountById(int id)
        {
            var account = await _accounts.GetById(id);
            
            var mappedAccount = _mapper.Map<AccountDto>(account);
            mappedAccount.OwnerId = account.Owner.Id;

            return mappedAccount;
        }

        public async Task<IEnumerable<AccountDto>> GetAllAccounts()
        {
            var accounts = await _accounts.GetAll();
            var mappedAccounts = accounts.Select(account => 
            {
                var mappedAccount = _mapper.Map<AccountDto>(account);
                mappedAccount.OwnerId = account.Owner.Id;
                
                return mappedAccount;
            });

           return mappedAccounts;
        }

        public async Task<bool> UpdateAccount(int id, AccountDto accountDto)
        {
            var updatedAccount = _mapper.Map<Account>(accountDto);
            updatedAccount.Owner = await _users.GetByAccountId(id);

            return await _accounts.Update(updatedAccount);
        }

        public async Task<bool> DeleteAccount(int id)
        {
            return await _accounts.Delete(id);
        }
    }
}