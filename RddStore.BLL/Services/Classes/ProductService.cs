using Mapster;
using Microsoft.AspNetCore.Http;
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
        private readonly IProductRepository _producRepository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository iproductRepository,IFileService fileService) : base(iproductRepository)
        {
            _producRepository = iproductRepository;
            _fileService = fileService;
        }

        public async Task<int> CreateProductAsync(ProductRequest request)
        {
           var entity = request.Adapt<Product>();
           entity.CreatedAt = DateTime.UtcNow;
           
           if (request.MainImage != null)
           {
              var imagePath = await _fileService.UploadAsync(request.MainImage);
              entity.MainImage = imagePath;
           }
           if(request.SubImages != null )
            {
                var subImagePaths = await _fileService.UploadManyAsync(request.SubImages);
                 entity.SubImages=  subImagePaths.Select(img => new ProductImage { ImageName = img }).ToList();
                

            }
         return _producRepository.Add(entity);
        }

        public async Task<List<ProductResponse>> GetAllProductsAsync(HttpRequest request,bool onlyActive = false)
        {
            var products = await _producRepository.GetAllProductsWithImageAsync();
            if (onlyActive)
            {
                products = products.Where(p => p.Status == Status.Active).ToList();
            }
            return  products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Quantity = p.Quantity,
                MainImageUrl = $"{request.Scheme}://{request.Host}/images/subImages/{p.MainImage}",
                SubImagesUrls = p.SubImages.Select(img => $"{request.Scheme}://{request.Host}/images/subImages/{img.ImageName}").ToList(),

            }).ToList();
        }

    }
}
