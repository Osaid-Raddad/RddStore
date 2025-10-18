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
    public class CategoryService : GenericService<CategoryRequest, CategoryResponse, Category>, ICategoryService
    {

        private readonly ICategoryRepository _icategoryRepository;

        public CategoryService(ICategoryRepository icategoryRepository) : base(icategoryRepository)
        {
            
        }




    }
}
