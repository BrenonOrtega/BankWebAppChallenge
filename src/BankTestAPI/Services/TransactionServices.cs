using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using BankTestAPI.Dtos;
using BankTestAPI.Models;
using BankTestAPI.Models.Enum;
using BankTestAPI.Data.Repositories.Interfaces;
using AutoMapper;

namespace BankTestAPI.Services
{
    public class TransactionServices
    {
        private readonly ITransactionRepository _transactions;

        private readonly IAccountRepository _accounts;

        private readonly Mapper _mapper;

        public TransactionServices(ITransactionRepository transactions, IAccountRepository accounts, Mapper mapper)
        {
            _transactions = transactions;
            _accounts = accounts;
            _mapper = mapper;
        }

        public async Task<bool> Deposit(TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);
            transaction.Account = await _accounts.GetById(transactionDto.AccountId);

            var executed = transaction.Account.Deposit(transaction);

            if (executed)
            {
                await _transactions.Create(transaction);
                await _accounts.Update(transaction.Account);
            }

            return executed;
        }

        public async Task<bool> Withdraw(TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);
            transaction.Account = await _accounts.GetById(transactionDto.AccountId);

            var executed = transaction.Account.Withdraw(transaction);

            if (executed)
            {
                await _transactions.Create(transaction);
                await _accounts.Update(transaction.Account);
            }

            return executed;
        }

        public async Task<bool> Transfer(int sourceAccountId, int destinationAccountId, decimal value)
        {
            var sourceAccount = await _accounts.GetById(sourceAccountId);
            var destinationAccount = await _accounts.GetById(destinationAccountId);

            var debitTransaction = new Transaction()
            {
                CreatedAt = DateTime.Now,
                Account = sourceAccount,
                RelatedAccount = destinationAccount,
                Type = TransactionType.Debit,
                Value = value,
            };

            var creditTransaction = new Transaction()
            {
                CreatedAt = DateTime.Now,
                Account = destinationAccount,
                RelatedAccount = sourceAccount,
                Type = TransactionType.Credit,
                Value = value,
            };

            var executed = sourceAccount.Withdraw(debitTransaction)
                && destinationAccount.Deposit(creditTransaction);

            if (executed)
            {
                await _transactions.Create(debitTransaction);
                await _transactions.Create(creditTransaction);
                await _accounts.Update(sourceAccount);
                await _accounts.Update(destinationAccount);
            }

            return executed;
        }

        public async Task<IEnumerable<Transaction>> GetAllByUser(int accountId)
        {
            var userTransactions = await _transactions.GetByAccountId(accountId);

            return userTransactions;
        }

        public async Task<IEnumerable<Transaction>> GetByDate(DateTime transactionsDate)
        {
            var datedTransactions = await _transactions.GetByDate(transactionsDate);

            return datedTransactions;
        }
    }
}