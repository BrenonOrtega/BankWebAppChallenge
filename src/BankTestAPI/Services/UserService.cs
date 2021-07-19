using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BankTestAPI.Dtos;
using BankTestAPI.Models;
using BankTestAPI.Data.Repositories.Interfaces;
using BankTestAPI.Services.Interfaces;

namespace BankTestAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _users;
        private readonly IAccountRepository _accounts;
        private readonly IMapper _mapper;

        public UserService(IUserRepository users, IAccountRepository accounts, IMapper mapper)
        {
            _users = users;
            _accounts = accounts;
            _mapper = mapper;
        }

        public async Task<bool> RegisterUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Account = await _accounts.GetById(userDto.AccountId);
            return await _users.Create(user);
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _users.GetById(id);
            var mappedUser = _mapper.Map<UserDto>(user);
            return mappedUser;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _users.GetAll();

            var mappedUsers = users.Select(user =>
            {
                var mappedUser = _mapper.Map<UserDto>(user);
                return mappedUser;
            });

            return mappedUsers;
        }

        public async Task<bool> UpdateUser(int id, UserDto userDto)
        {
            var mapped = _mapper.Map<User>(userDto);
            mapped.Id = id;
            mapped.Account = await _accounts.GetByOwnerId(id);

            return await _users.Update(mapped);
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _users.Delete(id);
        }


    }
}