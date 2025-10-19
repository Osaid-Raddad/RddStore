using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RddStore.DAL.Data;
using RddStore.DAL.Models;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public  SeedData(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedingDataAsync()
        {
            if( (await _context.Database.GetPendingMigrationsAsync()).Any())
            {
              await  _context.Database.MigrateAsync();
            }
            if( ! await _context.Categories.AnyAsync())
            {
                await _context.Categories.AddRangeAsync(
                    new Models.Category { Name = "Electronics" },
                    new Models.Category { Name = "Books" },
                    new Models.Category { Name = "Clothing" }
                );
                
            }
            if(!await _context.Brands.AnyAsync())
            {
                await _context.Brands.AddRangeAsync(
                    new Models.Brand { Name = "Apple" },
                    new Models.Brand { Name = "Samsung" },
                    new Models.Brand { Name = "Nike" }
                );
            }
              await  _context.SaveChangesAsync();
        }

        public async Task IdentityDataSeedingAsync()
        {
            if(! await _roleManager.Roles.AnyAsync())
            {
              await  _roleManager.CreateAsync(new IdentityRole ("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            if( ! await _userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUser
                {
                    Email = "osaid@gmail.com",
                    FullName = "Osaid Islam",
                    PhoneNumber = "1234567890",
                    UserName = "OSAID"
                };
                var user2 = new ApplicationUser
                {
                    Email = "osmar@gmail.com",
                    FullName = "Omar Jamil",
                    PhoneNumber = "1234587890",
                    UserName = "Omar12"
                };
                var user3 = new ApplicationUser
                {
                    Email = "adam@gmail.com",
                    FullName = "Adam Jamal",
                    PhoneNumber = "1234567590",
                    UserName = "Adam23"
                };
                await _userManager.CreateAsync(user1, "P@ssw0rd1");
                await _userManager.CreateAsync(user2, "P@ssw0rd2");
                await _userManager.CreateAsync(user3, "P@ssw0");

                await _userManager.AddToRoleAsync(user1, "Admin");
                await _userManager.AddToRoleAsync(user2, "SuperAdmin");
                await _userManager.AddToRoleAsync(user3, "Customer");
            }
            await _context.SaveChangesAsync();
        }

    }
}
