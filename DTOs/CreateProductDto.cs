using System.ComponentModel.DataAnnotations;

namespace warehouse_management_api.DTOs
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Range(1, 1000000)]
        public decimal Price { get; set; }
        
        [Range(0, 10000)]
        public int StockQuantity { get; set; }
    }
}