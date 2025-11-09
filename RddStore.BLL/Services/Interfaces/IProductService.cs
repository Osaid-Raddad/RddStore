using Microsoft.AspNetCore.Http;
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
    public interface IProductService : IGenericService<ProductRequest, ProductResponse, Product> 
    {
        Task<int> CreateProductAsync(ProductRequest request);
        Task<List<ProductResponse>> GetAllProductsAsync(HttpRequest request, bool onlyActive = false);
    }
}
