using Microsoft.AspNetCore.Mvc;
using warehouse_management_api.DTOs.Product;
using warehouse_management_api.Services;
using Microsoft.AspNetCore.Authorization;

namespace warehouse_management_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }
    }
}