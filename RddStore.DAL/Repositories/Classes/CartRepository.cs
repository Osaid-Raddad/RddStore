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
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public int Add(Cart cart)
        {
            _context.Carts.Add(cart);
            return _context.SaveChanges();
        }

        public List<Cart> GetUserCart(string UserId)
        {
            return _context.Carts.Include(c => c.Product).Where(c => c.UserId == UserId).ToList();
        }
    }
}
