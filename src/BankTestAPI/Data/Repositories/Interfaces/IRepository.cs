using BankTestAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BankTestAPI.Data.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        Task<bool> Create(T entity);

        Task<bool> Update(T updatedEntity);

        Task<bool> Delete(int id);
    }
}