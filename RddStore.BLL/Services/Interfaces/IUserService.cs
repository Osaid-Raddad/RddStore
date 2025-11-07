using RddStore.DAL.DTO.Responses;
using RddStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(string id);

        Task<bool> BlockUserAsync(string id, int days);
        Task<bool> UnblockUserAsync(string id);
        Task<bool> IsBlockedAsync(string id);

        Task<bool> ChangeUserRoleAsync(string userId, string newRole);

    }
}
