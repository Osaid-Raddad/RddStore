using RddStore.DAL.DTO.Requests;
using RddStore.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RddStore.BLL.Services
{
    public interface ICategoryService
    {
        int CreateCategory(CategoryRequest request);
        IEnumerable<CategoryResponse> GetAllCategories();
        CategoryResponse? GetCategoryById(int id);

        int DeleteCategory(int id);
        int UpdateCategory(int id, CategoryRequest category);
        public bool ToogleStatus(int id);
    }
}
