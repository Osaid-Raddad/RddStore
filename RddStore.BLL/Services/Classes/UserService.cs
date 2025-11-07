using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager) 
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }
        public async Task<List<UserDto>> GetAllUsersAsync()
        {
           var users = await _userRepository.GetAllUsersAsync();
            var userDto = new List<UserDto>();
            foreach (var user in users)
            {
                var userRole = await _userManager.GetRolesAsync(user);
                userDto.Add(new UserDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    EmailConfirmed = user.EmailConfirmed,
                    UserRole = userRole.FirstOrDefault()
                });

            }
            
            return userDto;
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

        public async Task<bool> ChangeUserRoleAsync(string userId, string newRole)
        {
            return await _userRepository.ChangeUserRoleAsync(userId, newRole);
        }

    }
}
