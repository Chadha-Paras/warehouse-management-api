using Microsoft.EntityFrameworkCore;
using warehouse_management_api.Data;
using warehouse_management_api.Models;

namespace warehouse_management_api.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly AppDbContext _context;

        public InventoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Inventory?> GetAsync(int productId, int warehouseId)
        {
            return await _context.Inventories
                .FirstOrDefaultAsync(i => i.ProductId == productId && i.WarehouseId == warehouseId);
        }

        public async Task<Inventory> AddAsync(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }

        public async Task UpdateAsync(Inventory inventory)
        {
            _context.Inventories.Update(inventory);
            await _context.SaveChangesAsync();
        }
    }
}