using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using warehouse_management_api.Services;

namespace warehouse_management_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;

        public TransactionController(ITransactionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10, int? productId = null, int? userId = null)
        {
            var result = await _service.GetAllAsync(page, pageSize, productId, userId);
            return Ok(result);
        }    
    }
}