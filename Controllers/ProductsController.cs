using Microsoft.AspNetCore.Mvc;
using warehouse_management_api.Data;
using warehouse_management_api.Models;
using warehouse_management_api.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace warehouse_management_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductsController(IProductService service)
        {
            _service = service;
        }

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }

        // GET: api/products/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        //POST: api/products
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var product = await _service.CreateAsync(dto);
            return Ok(product);
        }

        // PUT: api/products/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateProductDto dto)
        {
            var product = await _service.UpdateAsync(id, dto);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // DELETE: api/products/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _service.DeleteAsync(id);
            if (!isDeleted)
                return NotFound();

            return Ok("Deleted Succesfully");
        }
    }
}