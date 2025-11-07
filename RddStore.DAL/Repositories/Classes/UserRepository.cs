using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RddStore.DAL.Data;
using RddStore.DAL.Models;
using RddStore.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.DAL.Repositories.Classes
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager) 
        {
            _userManager = userManager;
        }
        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<bool> BlockUserAsync(string id, int days)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return false;
            }
            user.LockoutEnd = DateTime.UtcNow.AddDays(days);

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;

        }

        public async Task<bool> UnblockUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return false;
            }
            user.LockoutEnd = null;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> IsBlockedAsync(string id) 
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            return user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.UtcNow;

        }

    }
}
