using RddStore.DAL.DTO.Requests;
using RddStore.DAL.DTO.Responses;
using RddStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.BLL.Services.Interfaces
{
    public interface IBrandService : IGenericService<BrandRequest,BrandResponse,Brand>
    {
        Task<int> CreateFileAsync(BrandRequest request);
    }
}
