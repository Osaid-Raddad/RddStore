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
    public class OrderItemRepositroy : IOrderItemRepositroy
    {
        private readonly ApplicationDbContext _context;

        public OrderItemRepositroy(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddOrderItemAsync(List<OrderItem> Items)
        {
            await _context.OrderItems.AddRangeAsync(Items);
            await _context.SaveChangesAsync();
        }

        
    }
}
