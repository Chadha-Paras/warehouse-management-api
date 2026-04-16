using warehouse_management_api.Models;

namespace warehouse_management_api.Repositories
{
    public interface IWarehouseRepository
    {
        Task<List<Warehouse>> GetAllAsync();
        Task<Warehouse> AddAsync(Warehouse warehouse);
    }
}

