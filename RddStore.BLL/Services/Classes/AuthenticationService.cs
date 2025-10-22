using Microsoft.AspNetCore.Identity;
using RddStore.BLL.Services.Interfaces;
using RddStore.DAL.DTO.Requests;
using RddStore.DAL.DTO.Responses;
using RddStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.BLL.Services.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager; 
        }

        public async Task<UserResponse> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                throw new Exception("Invalid email or password.");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!isPasswordValid)
            {
                throw new Exception("Invalid email or password.");
            }
            return new UserResponse()
            {
                Email = user.Email
            };
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            var newUser = new ApplicationUser()
            {
                FullName = registerRequest.FullName,
                UserName = registerRequest.UserName,
                Email = registerRequest.Email
            };
            var result = await _userManager.CreateAsync(newUser, registerRequest.Password);
            if (result.Succeeded )
            {
                return new UserResponse()
                {
                    Email = newUser.Email
                };
            }
            else
            {
               throw new Exception($"{result.Errors}");
            }

        }
    }
}
