using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RddStore.BLL.Services.Interfaces;
using RddStore.DAL.DTO.Requests;
using RddStore.DAL.DTO.Responses;
using RddStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.BLL.Services.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _confige;

        public AuthenticationService(UserManager<ApplicationUser> userManager, IConfiguration confige)
        {
            _userManager = userManager;
            _confige = confige;
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
                Token = await CreateTokenAsync(user)
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
                    Token = newUser.Email
                };
            }
            else
            {
               throw new Exception($"{result.Errors}");
            }

        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new Claim("id",user.Id),
                new Claim("username",user.UserName),
                new Claim("Email",user.Email),
            };
            
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_confige.GetSection("JwtOption")["SecretKey"]));
            var Credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                claims: Claims,
                expires: DateTime.Now.AddDays(15),
                signingCredentials: Credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
