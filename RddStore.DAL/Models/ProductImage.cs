using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.DAL.Models
{
    public class ProductImage
    {
        public int id { get; set; }
        public string ImageName { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
