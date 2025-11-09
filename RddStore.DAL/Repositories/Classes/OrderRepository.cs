using Microsoft.EntityFrameworkCore;
using RddStore.DAL.Data;
using RddStore.DAL.Models;
using RddStore.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.DAL.Repositories.Classes
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Order?> AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> GetOrderByUserAsync(int OrderId)
        {
           return await _context.Orders.Include(o => o.User).FirstOrDefaultAsync(o => o.Id == OrderId);
        }

        public async Task<List<Order>> GetOrdersByStatusAsync(OrderStatusEnum status)
        {
            return await _context.Orders.Where(o => o.Status == status)
                .OrderByDescending(O => O.OrderDate).ToListAsync();
        }

        public async Task<List<Order>> GetUserOrdersAsync(string id)
        {
            return await _context.Orders.Include(o => o.UserId).OrderByDescending(o=>o.OrderDate).ToListAsync();
        }

        public async Task<bool> ChangeOrderStatusAsync(int orderId, OrderStatusEnum newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return false;
            }
            order.Status = newStatus;
            _context.Orders.Update(order);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

    }
}
