using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RddStore.BLL.Services.Interfaces;
using RddStore.DAL.Models;

namespace RddStore.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
   // [Authorize(Roles = "Admin,SuperAdmin")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("Get-Order-ByStatus/{status}")]
        public async Task<IActionResult> GetOrdersByStatus(OrderStatusEnum status)
        {
            var orders = await _orderService.GetOrdersByStatusAsync(status);
            return Ok(orders);
        }

        [HttpPatch("Change-Order-Status/{orderId}")]
        public async Task<IActionResult> ChangeOrderStatus(int orderId, OrderStatusEnum newStatus)
        {
            var result = await _orderService.ChangeOrderStatusAsync(orderId, newStatus);
            if (!result)
            {
                return NotFound();
            }
            return Ok(new {message="Order Status Changed Successfully"});
        }




    }

}
