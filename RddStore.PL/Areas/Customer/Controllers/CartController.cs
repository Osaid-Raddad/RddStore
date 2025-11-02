using Microsoft.AspNetCore.Authorization;
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
    [Authorize (Roles="Customer")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService) {
            _cartService = cartService;
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCartAsync( CartRequest cartRequest)
        {
           var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           var result = await _cartService.AddToCartAsync(cartRequest, userId);
           if (result)
           {
             return Ok(new { Message = "Product added to cart successfully." });
           }
           else
           {
             return BadRequest(new { Message = "Failed to add product to cart." });
           }
        }

        [HttpGet("GetUserCart")]
        public async Task<IActionResult> GetUserCartAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartSummary = await _cartService.GetSummaryResponseAsync(userId);
            return Ok(cartSummary);
        }
    }
}
