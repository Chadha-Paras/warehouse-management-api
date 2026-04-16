using Microsoft.EntityFrameworkCore;
using warehouse_management_api.Data;
using warehouse_management_api.Models;

namespace warehouse_management_api.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<InventoryTransaction> GetQueryable()
        {
            return _context.InventoryTransactions.AsQueryable();
        }
    }
}