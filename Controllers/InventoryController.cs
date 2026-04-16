using Microsoft.AspNetCore.Mvc;
using warehouse_management_api.DTOs.Inventory;
using warehouse_management_api.Services;
using Microsoft.AspNetCore.Authorization;

namespace warehouse_management_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        [HttpPost("add-stock")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddStock(CreateInventoryDto dto)
        {
            var result = await _service.AddOrUpdateStockAsync(dto);
            return Ok(result);
        }

        [HttpPost("transfer")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Transfer(TransferStockDto dto)
        {
            await _service.TransferStockAsync(dto);
            return Ok("Stock transferred successfully");
        }
        [HttpPost("bulk")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Bulk(List<BulkInventoryDto> items)
        {
            var result = await _service.BulkAddOrUpdateAsync(items);
            return Ok(result);
        }
    }
}