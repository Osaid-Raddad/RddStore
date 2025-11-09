using RddStore.BLL.Services.Interfaces;
using RddStore.DAL.Models;
using RddStore.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.BLL.Services.Classes
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orederRepository;

        public OrderService(IOrderRepository orederRepository) 
        {
            _orederRepository = orederRepository;
        }

        public async Task<Order?> AddOrderAsync(Order order)
        {
            return await _orederRepository.AddOrderAsync(order);
        }

        public async Task<bool> ChangeOrderStatusAsync(int orderId, OrderStatusEnum newStatus)
        {
            return await _orederRepository.ChangeOrderStatusAsync(orderId, newStatus);
        }

        public async Task<List<Order>> GetUserOrdersAsync(string id)
        {
            return await _orederRepository.GetUserOrdersAsync(id);
        }

        public async Task<List<Order>> GetOrdersByStatusAsync(OrderStatusEnum status)
        {
            return await _orederRepository.GetOrdersByStatusAsync(status);
        }

        public async Task<Order?> GetOrderByUserAsync(int OrderId)
        {
            return await _orederRepository.GetOrderByUserAsync(OrderId);
        }

    }
}
