using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RddStore.BLL.Services.Interfaces;

namespace RddStore.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles= "Admin,SuperAdmin")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService ibrandService)
        {
            _brandService = ibrandService;
        }


        [HttpGet("GetAll")]
        public IActionResult GetAllCategories()
        {
            return Ok(_brandService.GetAll());
        }


        [HttpGet("Get/{id}")]
        public IActionResult GetbrandById(int id)
        {
            var brand = _brandService.GetById(id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }

    }
}
