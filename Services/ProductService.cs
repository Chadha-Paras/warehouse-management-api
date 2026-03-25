using warehouse_management_api.DTOs;
using warehouse_management_api.Models;
using warehouse_management_api.Repositories;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public async Task<Product> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity
        };

        return await _repo.CreateAsync(product);
    }

    public async Task<Product?> UpdateAsync(int id, CreateProductDto dto)
    {
        var updatedProduct = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity
        };

        return await _repo.UpdateAsync(id, updatedProduct);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repo.DeleteAsync(id);
    }
}