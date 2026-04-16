using warehouse_management_api.DTOs.Transaction;

namespace warehouse_management_api.Services
{
    public interface ITransactionService
    {
        Task<object> GetAllAsync(int page, int pageSize, int? productId, int? userId);
    }
}