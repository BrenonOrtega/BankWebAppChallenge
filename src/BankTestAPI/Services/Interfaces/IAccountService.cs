using System.Collections.Generic;
using System.Threading.Tasks;
using BankTestAPI.Dtos;

namespace BankTestAPI.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDto>> GetAllAccounts();
        
        Task<AccountDto> GetAccountById(int id);
       
        Task<bool> RegisterAccount(AccountDto accountDto);
       
        Task<bool> DeleteAccount(int id);
    }
}