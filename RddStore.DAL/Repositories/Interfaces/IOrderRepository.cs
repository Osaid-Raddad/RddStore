using RddStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.DAL.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderByUserAsync(int OrderId);
        Task<Order?> AddOrderAsync(Order order);
    }
}
