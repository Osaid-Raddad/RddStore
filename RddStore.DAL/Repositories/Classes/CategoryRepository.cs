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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
       
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
           
        }

      
    }
}
