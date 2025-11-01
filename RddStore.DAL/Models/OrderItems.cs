using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.DAL.Models
{
    [PrimaryKey(nameof(OrderId),nameof(ProductId))]
    public class OrderItems
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
      
        public decimal totalPrice { get; set; }
    }
}
