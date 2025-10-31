using RddStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.DAL.Repositories.Interfaces
{
    public interface ICartRepository 
    {
        public int Add(Cart cart);
        
        public List<Cart> GetUserCart(string UserId);

    }
}
