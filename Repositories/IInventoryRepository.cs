using warehouse_management_api.Models;

namespace warehouse_management_api.Repositories
{
    public interface IInventoryRepository
    {
        Task<Inventory?> GetAsync(int productId, int warehouseId);
        Task<Inventory> AddAsync(Inventory inventory);
        Task UpdateAsync(Inventory inventory);
    }
}