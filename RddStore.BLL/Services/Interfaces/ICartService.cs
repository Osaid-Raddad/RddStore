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
        Task<bool> AddToCartAsync(CartRequest cartRequest, string UserId);

        Task<CartSummaryResponse> GetSummaryResponseAsync(string UserId);

        Task<bool> ClearCartAsync(string UserId);
    }
}
