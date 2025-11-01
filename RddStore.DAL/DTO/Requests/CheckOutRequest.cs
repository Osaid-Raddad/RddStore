using RddStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.DAL.DTO.Requests
{
    public class CheckOutRequest
    {
        public PaymentMethodEnum PaymentMethod { get; set; }
    }
}
