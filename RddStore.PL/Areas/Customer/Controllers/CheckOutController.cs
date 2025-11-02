using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RddStore.BLL.Services.Interfaces;
using RddStore.DAL.DTO.Requests;
using System.Security.Claims;

namespace RddStore.PL.Areas.Customer.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CheckOutController : ControllerBase
    {
        private readonly ICheckOutService _checkOutService;

        public CheckOutController(ICheckOutService checkOutService)
        {
            _checkOutService = checkOutService;
        }

        [HttpPost("Payment")]
        public async Task<IActionResult> Payment([FromBody] CheckOutRequest request)
        {
           var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _checkOutService.ProcessPaymentAsync(request, userId, Request);
            return Ok(response);
        }


        [HttpGet("Success/{orderid}")]
        [AllowAnonymous]
        public async Task<IActionResult> Success([FromRoute] int OrderId)
        {
           var result = await _checkOutService.HandlePaymentSuccessAsync(OrderId);
            return Ok(result);
        }
    }
}
