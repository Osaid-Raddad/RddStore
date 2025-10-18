using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RddStore.BLL.Services;
using RddStore.DAL.DTO.Requests;

namespace RddStore.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _icategoryService;

        public CategoriesController(ICategoryService icategoryService )
        {
            _icategoryService = icategoryService;
        }


        [HttpGet("GetAll")]
        public IActionResult GetAllCategories()
        {
            return Ok(_icategoryService.GetAllCategories());
        }


        [HttpGet ("Get/{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _icategoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryRequest request)
        {
            var id = _icategoryService.CreateCategory(request);
          
            return CreatedAtAction(nameof(GetCategoryById), new { id });
        }


        [HttpPatch ("Update/{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryRequest request)
        {
            var updated = _icategoryService.UpdateCategory(id, request);
            return  updated > 0 ? Ok() : NotFound();
        }

        [HttpPatch("ToogleStatus/{id}")]
        public IActionResult ToggleStatus(int id)
        {
            var updated = _icategoryService.ToogleStatus(id);
            return updated  ? Ok( new {message = "Status Toggled"}) : NotFound(new {message="Category not found"});
        }



        [HttpDelete ("Delete")]
        public IActionResult DeleteCategory(int id)
        {
           var deleted = _icategoryService.DeleteCategory(id);
            return deleted > 0 ? Ok() : NotFound();
        }

    }
}
