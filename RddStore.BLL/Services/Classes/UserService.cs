using Mapster;
using RddStore.BLL.Services.Interfaces;
using RddStore.DAL.DTO.Responses;
using RddStore.DAL.Models;
using RddStore.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.BLL.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public async Task<List<UserDto>> GetAllUsersAsync()
        {
           var users = await _userRepository.GetAllUsersAsync();
           return users.Adapt<List<UserDto>>();
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return user.Adapt<UserDto>();
        }

        public async Task<bool> IsBlockedAsync(string id) 
        {
            return await _userRepository.IsBlockedAsync(id);
        }
        
        public async Task<bool> BlockUserAsync(string id, int days) 
        {
            return await _userRepository.BlockUserAsync(id, days);
        }

        public async Task<bool> UnblockUserAsync(string id) 
        {
            return await _userRepository.UnblockUserAsync(id);
        }

    }
}
