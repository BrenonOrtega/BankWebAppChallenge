using AutoMapper;
using BankTestAPI.Dtos;
using BankTestAPI.Models;

namespace BankTestAPI.Profiles
{
    public class BankingProfile : Profile
    {
        public BankingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<AccountDto, Account>();
            CreateMap<Account, AccountDto>();
            
            CreateMap<TransactionDto, Transaction>();
            CreateMap<Transaction, TransactionDto>();
        }
    }
}