using Mapster;
using RddStore.DAL.DTO.Requests;
using RddStore.DAL.DTO.Responses;
using RddStore.DAL.Models;
using RddStore.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.BLL.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _icategoryRepository;

        public CategoryService(ICategoryRepository icategoryRepository)
        {
            _icategoryRepository = icategoryRepository;
        }


        public int CreateCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            return _icategoryRepository.Add(category);
        }

        public int DeleteCategory(int id)
        {
            var category = _icategoryRepository.GetById(id);
            if (category == null)
            {
                return 0;
            }
            return _icategoryRepository.Remove(category);
        }

        public IEnumerable<CategoryResponse> GetAllCategories()
        {
            var categories = _icategoryRepository.GetAll();
            return categories.Adapt<IEnumerable<CategoryResponse>>();
        }

        public CategoryResponse? GetCategoryById(int id)
        {
            var category = _icategoryRepository.GetById(id);
            return category == null ? null : category.Adapt<CategoryResponse>();
        }

        public int UpdateCategory(int id, CategoryRequest request)
        {
           var category = _icategoryRepository.GetById(id);
            if (category == null)
            {
                return 0;
            }
            category.Name = request.Name;
            return _icategoryRepository.Update(category);
        }

        public bool ToogleStatus(int id)
        {
            var category = _icategoryRepository.GetById(id);
            if (category == null)
            {
                return false;
            } 
            category.Status = category.Status == Status.Active ? Status.Inactive : Status.Active;
            _icategoryRepository.Update(category);
            return true;
        }



    }
}
