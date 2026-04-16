using Microsoft.EntityFrameworkCore;
using warehouse_management_api.DTOs.Transaction;
using warehouse_management_api.Repositories;

namespace warehouse_management_api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;

        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> GetAllAsync(int page, int pageSize, int? productId, int? userId)
        {
            var query = _repository.GetQueryable();

            // Filters
            if (productId.HasValue)
                query = query.Where(t => t.ProductId == productId);

            if (userId.HasValue)
                query = query.Where(t => t.UserId == userId);

            // Total count
            var totalRecords = await query.CountAsync();

            // Pagination
            var data = await query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Map to DTO
            var result = data.Select(t => new TransactionResponseDto
            {
                Id = t.Id,
                ProductId = t.ProductId,
                FromWarehouseId = t.FromWarehouseId,
                ToWarehouseId = t.ToWarehouseId,
                Quantity = t.Quantity,
                Type = t.Type,
                UserId = t.UserId,
                CreatedAt = t.CreatedAt
            });

            return new
            {
                page,
                pageSize,
                totalRecords,
                data = result
            };
        }
    }
}