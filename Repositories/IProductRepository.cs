using warehouse_management_api.Models;

namespace warehouse_management_api.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> AddAsync(Product product);
    }
}