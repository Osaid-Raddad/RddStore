using Microsoft.EntityFrameworkCore;
using RddStore.DAL.Data;
using RddStore.DAL.DTO.Requests;
using RddStore.DAL.DTO.Responses;
using RddStore.DAL.Models;
using RddStore.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.DAL.Repositories.Classes
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsWithImageAsync()
        {
            return await _context.Products.Include(p => p.SubImages).ToListAsync();
        }

        public async Task DecreaseQuantityAsync(List<(int productId, int quantity)> Items)
        {
      
            var productIds = Items.Select(i => i.productId).ToList();
            var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();
            foreach (var product in products)
            {
                var item = Items.FirstOrDefault(i => i.productId == product.Id);
                if (product.Quantity < item.quantity)
                {
                    throw new Exception("not enough stock available");
                }
                product.Quantity -= item.quantity;
                
            }
            await _context.SaveChangesAsync();
       
        }
    }
}
