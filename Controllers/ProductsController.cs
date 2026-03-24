using Microsoft.AspNetCore.Mvc;
using warehouse_management_api.Models;
using warehouse_management_api.DTOs;

namespace warehouse_management_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // Temporary in-memory list (we will replace with DB later)
        private static List<Product> products = new List<Product>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(products);
        }

        [HttpPost]
        public IActionResult Create(CreateProductDto dto)
        {
            var product = new Product
            {
                Id = products.Count + 1,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity
            };

            products.Add(product);

            return Ok(product);
        }
    }
}