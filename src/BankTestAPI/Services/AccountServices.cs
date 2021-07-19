using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BankTestAPI.Dtos;
using BankTestAPI.Models;
using BankTestAPI.Services.Interfaces;
using BankTestAPI.Data.Repositories.Interfaces;
using System;

namespace BankTestAPI.Services
{
    public class AccountService : IAccountService
   {
        private readonly IUserRepository _users;

        private readonly IAccountRepository _accounts;

        private readonly ITransactionRepository _transactions;

        private readonly IMapper _mapper;

        public AccountService(IUserRepository users, IAccountRepository accounts, IMapper mapper)
        {
            _users = users;
            _accounts = accounts;
            _mapper = mapper;
        }

        public async Task<bool> RegisterAccount(AccountDto accountDto)
        {
            var owner = await _users.GetById(accountDto.OwnerId);

            if(owner.Id != 0)
            {
                var createdAccount = new Account(){ Owner = owner };
                await _accounts.Create(createdAccount);
                
                owner.Account = createdAccount;
                await _users.Update(owner);
                return true;
            }
            return false;
        }

        public async Task<AccountDto> GetAccountById(int id)
        {
            var account = await _accounts.GetById(id);
            
            var mappedAccount = _mapper.Map<AccountDto>(account);
            mappedAccount.OwnerId = account.OwnerId;

            return mappedAccount;
        }

        public async Task<IEnumerable<AccountDto>> GetAllAccounts()
        {
            var accounts = await _accounts.GetAll();
            var mappedAccounts = accounts.Select(account => 
            {
                var mappedAccount = _mapper.Map<AccountDto>(account);
                mappedAccount.OwnerId = account.OwnerId;

                return mappedAccount;
            });

           return mappedAccounts;
        }

        public async Task<bool> DeleteAccount(int id)
        {
            var account = await _accounts.GetById(id);
            if (account.Balance != decimal.Zero)
                throw new InvalidOperationException("Unnable to delete an account that has Balance");

            return await _accounts.Delete(id);
        }
    }
}