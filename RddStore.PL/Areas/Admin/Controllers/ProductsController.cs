using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RddStore.BLL.Services.Classes;
using RddStore.BLL.Services.Interfaces;
using RddStore.DAL.DTO.Requests;

namespace RddStore.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductRequest productCreate)
        {
            var result = await _productService.CreateFileAsync(productCreate);
            return Ok(result);
        }

        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts() => Ok(_productService.GetAll());
    }
}
