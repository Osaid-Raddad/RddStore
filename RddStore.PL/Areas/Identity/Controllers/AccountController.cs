using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RddStore.BLL.Services.Interfaces;
using RddStore.DAL.DTO.Requests;
using RddStore.DAL.DTO.Responses;

namespace RddStore.PL.Areas.Identity.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Identity")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserResponse>> Register([FromBody] RegisterRequest registerRequest)
        {
            var result = await _authenticationService.RegisterAsync(registerRequest,Request);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await _authenticationService.LoginAsync(loginRequest);
            return Ok(result);
        }
        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult<string>> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            var result = await _authenticationService.ConfirmEmail(userId, token);
            return Ok(result);
        }

        [HttpPost("Forgot-Password")]
        public async Task<ActionResult<string>> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var result = await _authenticationService.ForgotPassword(request);
            return Ok(result);
        }

        [HttpPatch("Reset-Password")]
        public async Task<ActionResult<string>> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await _authenticationService.ResetPassword(request);
            return Ok(result);
        }
    }
}
