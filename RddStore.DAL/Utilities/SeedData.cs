using Microsoft.EntityFrameworkCore;
using RddStore.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.DAL.Utilities
{
    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext _context;

        public  SeedData(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedingData()
        {
            if(_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }
            if(!_context.Categories.Any())
            {
                _context.Categories.AddRange(
                    new Models.Category { Name = "Electronics" },
                    new Models.Category { Name = "Books" },
                    new Models.Category { Name = "Clothing" }
                );
                
            }
            if(!_context.Brands.Any())
            {
                _context.Brands.AddRange(
                    new Models.Brand { Name = "Apple" },
                    new Models.Brand { Name = "Samsung" },
                    new Models.Brand { Name = "Nike" }
                );
            }
                _context.SaveChanges();
        }
    }
}
