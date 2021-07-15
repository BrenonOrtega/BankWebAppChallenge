using System.Threading.Tasks;
using BankTestAPI.Models;

namespace BankTestAPI.Data.Repositories.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetByOwnerId(int ownerId);
    }
}