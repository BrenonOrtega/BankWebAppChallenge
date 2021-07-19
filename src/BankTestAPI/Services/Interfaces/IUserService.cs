using System.Collections.Generic;
using System.Threading.Tasks;
using BankTestAPI.Dtos;

namespace BankTestAPI.Services.Interfaces
{
    public interface IUserService
    {
        
        Task<IEnumerable<UserDto>> GetAllUsers();
        
        Task<UserDto> GetUserById(int id);
       
        Task<bool> RegisterUser(UserDto userDto);
        
        Task<bool> UpdateUser(int id, UserDto userDto);
       
        Task<bool> DeleteUser(int id);
    }
}