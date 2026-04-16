using warehouse_management_api.Models;

namespace warehouse_management_api.Repositories
{
    public interface ITransactionRepository
    {
        IQueryable<InventoryTransaction> GetQueryable();
    }
}