using Microsoft.AspNetCore.Http;
using RddStore.DAL.DTO.Requests;
using RddStore.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.BLL.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserResponse> RegisterAsync(RegisterRequest registerRequest, HttpRequest httpRequest);
        Task<UserResponse> LoginAsync(LoginRequest loginRequest);
        Task<string> ConfirmEmail(string userId, string token);
        Task<bool> ForgotPassword(ForgotPasswordRequest request);
        Task<bool> ResetPassword(ResetPasswordRequest request);
    }
}
