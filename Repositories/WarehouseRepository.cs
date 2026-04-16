using Microsoft.EntityFrameworkCore;
using warehouse_management_api.Data;
using warehouse_management_api.Models;

namespace warehouse_management_api.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly AppDbContext _context;

        public WarehouseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Warehouse>> GetAllAsync()
        {
            return await _context.Warehouses.ToListAsync();
        }

        public async Task<Warehouse> AddAsync(Warehouse warehouse)
        {
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }
    }
}