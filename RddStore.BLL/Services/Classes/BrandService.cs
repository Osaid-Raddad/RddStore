using Mapster;
using RddStore.BLL.Services.Interfaces;
using RddStore.DAL.DTO.Requests;
using RddStore.DAL.DTO.Responses;
using RddStore.DAL.Models;
using RddStore.DAL.Repositories.Classes;
using RddStore.DAL.Repositories.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.BLL.Services.Classes
{
    public class BrandService : GenericService<BrandRequest, BrandResponse, Brand>, IBrandService
    {
        private readonly IBrandRepository _ibrandRepository;
        private readonly IFileService _fileService;

        public BrandService(IBrandRepository ibrandRepository,IFileService fileService) : base(ibrandRepository)
        {
            _ibrandRepository = ibrandRepository;
            _fileService = fileService;
        }


        public async Task<int> CreateFileAsync(BrandRequest request)
        {
            var entity = request.Adapt<Brand>();
            entity.CreatedAt = DateTime.UtcNow;

            if (request.Image != null)
            {
                var imagePath = await _fileService.UploadAsync(request.Image);
                entity.Image = imagePath;
            }
            return _ibrandRepository.Add(entity);
        }

    }
}
