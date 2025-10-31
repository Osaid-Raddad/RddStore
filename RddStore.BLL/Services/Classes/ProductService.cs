using Mapster;
using RddStore.BLL.Services.Interfaces;
using RddStore.DAL.DTO.Requests;
using RddStore.DAL.DTO.Responses;
using RddStore.DAL.Models;
using RddStore.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.BLL.Services.Classes
{
    public class ProductService : GenericService<ProductRequest, ProductResponse, Product>, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository iproductRepository,IFileService fileService) : base(iproductRepository)
        {
            _repository = iproductRepository;
            _fileService = fileService;
        }

        public async Task<int> CreateFileAsync(ProductRequest request)
        {
           var entity = request.Adapt<Product>();
           entity.CreatedAt = DateTime.UtcNow;
           
           if (request.MainImage != null)
           {
              var imagePath = await _fileService.UploadAsync(request.MainImage);
              entity.MainImage = imagePath;
            }
            return _repository.Add(entity);
        }


    }
}
