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
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService )
        {
            this.categoryService = categoryService;
        }


        [HttpGet("")]
        public IActionResult GetAllCategories()
        {
            return Ok(categoryService.GetAllCategories());
        }


        [HttpGet ("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryRequest request)
        {
            var id = categoryService.CreateCategory(request);
          
            return CreatedAtAction(nameof(GetCategoryById), new { id });
        }


        [HttpPatch ("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryRequest request)
        {
            var updated = categoryService.UpdateCategory(id, request);
            return  updated > 0 ? Ok() : NotFound();
        }

        [HttpPatch("ToogleStatus/{id}")]
        public IActionResult ToggleStatus(int id)
        {
            var updated = categoryService.ToogleStatus(id);
            return updated  ? Ok( new {message = "Status Toggled"}) : NotFound(new {message="Category not found"});
        }



        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
           var deleted = categoryService.DeleteCategory(id);
            return deleted > 0 ? Ok() : NotFound();
        }

    }
}
