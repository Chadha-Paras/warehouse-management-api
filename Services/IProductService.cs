using warehouse_management_api.DTOs.Product;

namespace warehouse_management_api.Services
{
    public interface IProductService
    {
        Task<List<ProductResponseDto>> GetAllAsync();
        Task<ProductResponseDto> CreateAsync(CreateProductDto dto);
    }
}