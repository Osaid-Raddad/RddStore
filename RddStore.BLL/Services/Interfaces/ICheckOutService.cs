using Microsoft.AspNetCore.Http;
using RddStore.DAL.DTO.Requests;
using RddStore.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.BLL.Services.Interfaces
{
    public interface ICheckOutService
    {
        Task <CheckOutResponse> ProcessPaymentAsync(CheckOutRequest request, string UserId, HttpRequest httpRequest);
    }
}
