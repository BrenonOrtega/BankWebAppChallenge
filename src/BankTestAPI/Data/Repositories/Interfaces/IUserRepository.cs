using System.Threading.Tasks;
using BankTestAPI.Models;

namespace BankTestAPI.Data.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByAccountId(int id);
    }
}