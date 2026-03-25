using warehouse_management_api.DTOs;
using warehouse_management_api.Models;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> CreateAsync(CreateProductDto dto);
    Task<Product?> UpdateAsync(int id, CreateProductDto dto);
    Task<bool> DeleteAsync(int id);
}