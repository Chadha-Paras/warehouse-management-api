using Microsoft.AspNetCore.Mvc;
using warehouse_management_api.DTOs.Warehouse;
using warehouse_management_api.Services;
using Microsoft.AspNetCore.Authorization;

namespace warehouse_management_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _service;

        public WarehouseController(IWarehouseService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateWarehouseDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }
    }
}