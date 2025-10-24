using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RddStore.BLL.Services.Interfaces;

namespace RddStore.PL.Areas.Customer.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize (Roles="Customer")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService ibrandService )
        {  
            _brandService = ibrandService;
        }
       

        [HttpGet("GetAll")]
        public IActionResult GetAllCategories()
        {
            return Ok(_brandService.GetAll(true));
        }


        [HttpGet("Get/{id}")]
        public IActionResult GetbrandById(int id)
        {
            var brand = _brandService.GetById(id,true);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }


    }
}
