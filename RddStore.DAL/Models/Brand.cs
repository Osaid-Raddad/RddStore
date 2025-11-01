using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.DAL.Models
{
    public class Brand : BaseModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Product> Products { get; set; }
    }
}
