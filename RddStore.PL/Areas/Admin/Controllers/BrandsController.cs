using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RddStore.BLL.Services.Interfaces;
using RddStore.DAL.DTO.Requests;

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
        public IActionResult GetAllBrands()
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

        [HttpPost]
        public async Task<IActionResult> CreatebrandAsync([FromForm] BrandRequest request)
        {
            var id = await _brandService.CreateFileAsync(request);

            return CreatedAtAction(nameof(GetbrandById), new { id },new { message = "Brand Created Successfully" });
        }


        [HttpPatch("Update/{id}")]
        public IActionResult Updatebrand(int id, [FromBody] BrandRequest request)
        {
            var updated = _brandService.Update(id, request);
            return updated > 0 ? Ok() : NotFound();
        }

        [HttpPatch("ToogleStatus/{id}")]
        public IActionResult ToggleStatus(int id)
        {
            var updated = _brandService.ToogleStatus(id);
            return updated ? Ok(new { message = "Status Toggled" }) : NotFound(new { message = "brand not found" });
        }



        [HttpDelete("Delete")]
        public IActionResult Deletebrand(int id)
        {
            var deleted = _brandService.Delete(id);
            return deleted > 0 ? Ok() : NotFound();
        }

    }
}
