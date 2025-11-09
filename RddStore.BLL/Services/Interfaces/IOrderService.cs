using RddStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> AddOrderAsync(Order order);
        Task<bool> ChangeOrderStatusAsync(int orderId, OrderStatusEnum newStatus);
        Task<List<Order>> GetUserOrdersAsync(string id);
        Task<List<Order>> GetOrdersByStatusAsync(OrderStatusEnum status);
        Task<Order?> GetOrderByUserAsync(int OrderId);
    }
}
