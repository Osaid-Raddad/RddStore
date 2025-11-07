using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RddStore.BLL.Services.Interfaces;
using RddStore.DAL.DTO.Requests;

namespace RddStore.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userservice;

        public UsersController(IUserService userservice)
        {
            _userservice = userservice;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userservice.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var user = await _userservice.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPatch("BlockUser/{id}")]   
        public async Task<IActionResult> BlockUser([FromRoute] string id, [FromQuery] int days)
        {
            var result = await _userservice.BlockUserAsync(id, days);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpPatch("UnblockUser/{id}")]
        public async Task<IActionResult> UnblockUser([FromRoute] string id)
        {
            var result = await _userservice.UnblockUserAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPatch("IsBlocked/{id}")]
        public async Task<IActionResult> IsBlocked([FromRoute] string id)
        {
            var isBlocked = await _userservice.IsBlockedAsync(id);
            if (!isBlocked)
            {
                return Ok(new { message = "No Blocked" });
            }
            return Ok(new { message = "Block" });
        }

        [HttpPatch("changeRole/{id}")]
        public async Task<IActionResult> ChangeUserRole([FromRoute] string id, [FromBody] ChangeRoleRequest request)
        {
            var result = await _userservice.ChangeUserRoleAsync(id, request.RoleName);
            return Ok(new {message = "Role Changed Successfylly" });
        }

    }
}
