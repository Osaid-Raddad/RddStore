using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IEmailSender _emailSender;

        public AuthenticationService(UserManager<ApplicationUser> userManager, IConfiguration confige, IEmailSender emailSender)
        {
            _userManager = userManager;
            _confige = confige;
            _emailSender = emailSender;
        }

        public async Task<bool> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("User Not Found");
            }
            if (user.CodeResetPassword != request.Code) return false;
            if(user.CodeResetPasswordExpiration < DateTime.UtcNow) return false;
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
            if(result.Succeeded)
            {
                await _emailSender.SendEmailAsync(request.Email, "Change Password", "<h1>Password Reset Successful<h1>");
            }
            return true;
        }

        public async Task<bool> ForgotPassword(ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("User Not Found");
            }

            var random = new Random();
            var code = random.Next(1000, 9999).ToString();

            user.CodeResetPassword = code;
            user.CodeResetPasswordExpiration = DateTime.UtcNow.AddMinutes(15);
            await _userManager.UpdateAsync(user);
            await _emailSender.SendEmailAsync(request.Email, "Password Reset Code",
                $"<h1>Password Reset Code</h1>" +
                $"<p>Your password reset code is: <strong>{code}</strong></p>" +
                $"<p>This code will expire in 15 minutes.</p>");

            return true;
        }

        public async Task<string> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Invalid user ID.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                throw new Exception("Email confirmation failed.");
            }
            return "Email confirmed successfully.";
        }


        public async Task<UserResponse> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                throw new Exception("Invalid email or password.");
            }
            if (await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new Exception("Email is not confirmed.");
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
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var escapeToken = Uri.EscapeDataString(token);
                var emailUrl = $"https://localhost:7042/api/Identity/Account/ConfirmEmail?token={escapeToken}&userId={newUser.Id}";

                await _emailSender.SendEmailAsync(newUser.Email, "Confirm your email",
                 $"<h1>Welcome to RddStore</h1>" +
                 $"<p>Please confirm your email by clicking the link below:</p>" +
                 $"<a href='{emailUrl}'>Confirm Email</a>");

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
