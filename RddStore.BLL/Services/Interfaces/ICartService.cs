using RddStore.DAL.DTO.Requests;
using RddStore.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.BLL.Services.Interfaces
{
    public interface ICartService
    {
        bool AddToCart(CartRequest cartRequest, string UserId);

        CartSummaryResponse GetSummaryResponse(string UserId);
    }
}
