using RddStore.DAL.DTO.Responses;
using RddStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string id);
         Task<bool> BlockUserAsync(string id, int days);
         Task<bool> UnblockUserAsync(string id);
        Task<bool> IsBlockedAsync(string id);

    }
}
