using warehouse_management_api.DTOs.Inventory;
using warehouse_management_api.Models;
using warehouse_management_api.Repositories;
using warehouse_management_api.Data;
using Microsoft.EntityFrameworkCore;

namespace warehouse_management_api.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InventoryService(IInventoryRepository repository, AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<InventoryResponseDto> AddOrUpdateStockAsync(CreateInventoryDto dto)
        {
            var username = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                throw new Exception("User not authenticated");

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                throw new Exception("User not found");
            // START TRANSACTION
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existing = await _repository.GetAsync(dto.ProductId, dto.WarehouseId);

                Inventory result;

                if (existing == null)
                {
                    var newInventory = new Inventory
                    {
                        ProductId = dto.ProductId,
                        WarehouseId = dto.WarehouseId,
                        Quantity = dto.Quantity
                    };

                    result = await _repository.AddAsync(newInventory);
                }
                else
                {
                    existing.Quantity += dto.Quantity;
                    await _repository.UpdateAsync(existing);
                    result = existing;
                }

                // SAVE TRANSACTION LOG
                _context.InventoryTransactions.Add(new InventoryTransaction
                {
                    ProductId = dto.ProductId,
                    FromWarehouseId = dto.WarehouseId,
                    ToWarehouseId = dto.WarehouseId,
                    Quantity = dto.Quantity,
                    Type = "ADD",
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow
                });

                // SAVE ALL CHANGES (Inventory + Log)
                await _context.SaveChangesAsync();

                // COMMIT TRANSACTION
                await transaction.CommitAsync();

                // RETURN RESPONSE
                return new InventoryResponseDto
                {
                    Id = result.Id,
                    ProductId = result.ProductId,
                    WarehouseId = result.WarehouseId,
                    Quantity = result.Quantity
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task TransferStockAsync(TransferStockDto dto)
        {
            var username = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                throw new Exception("User not authenticated");

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                throw new Exception("User not found");

            // VALIDATIONS
            if (dto.FromWarehouseId == dto.ToWarehouseId)
                throw new Exception("Source and destination cannot be same");

            if (dto.Quantity <= 0)
                throw new Exception("Quantity must be greater than zero");

            // START TRANSACTION
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var source = await _repository.GetAsync(dto.ProductId, dto.FromWarehouseId);

                if (source == null || source.Quantity < dto.Quantity)
                    throw new Exception("Insufficient stock");

                // Deduct from source
                source.Quantity -= dto.Quantity;
                await _repository.UpdateAsync(source);

                // Add to destination
                var destination = await _repository.GetAsync(dto.ProductId, dto.ToWarehouseId);

                if (destination == null)
                {
                    await _repository.AddAsync(new Models.Inventory
                    {
                        ProductId = dto.ProductId,
                        WarehouseId = dto.ToWarehouseId,
                        Quantity = dto.Quantity
                    });
                }
                else
                {
                    destination.Quantity += dto.Quantity;
                    await _repository.UpdateAsync(destination);
                }

                // SAVE TRANSACTION LOG 
                _context.InventoryTransactions.Add(new Models.InventoryTransaction
                {
                    ProductId = dto.ProductId,
                    FromWarehouseId = dto.FromWarehouseId,
                    ToWarehouseId = dto.ToWarehouseId,
                    Quantity = dto.Quantity,
                    Type = "TRANSFER",
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow
                });
                await _context.SaveChangesAsync();

                // COMMIT
                await transaction.CommitAsync();
            }
            catch
            {
                // ROLLBACK
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<BulkResponseDto> BulkAddOrUpdateAsync(List<BulkInventoryDto> items)
        {
            var result = new BulkResponseDto();

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var username = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

                foreach (var dto in items)
                {
                    try
                    {
                        // Validation
                        var productExists = await _context.Products.AnyAsync(p => p.Id == dto.ProductId);
                        var warehouseExists = await _context.Warehouses.AnyAsync(w => w.Id == dto.WarehouseId);

                        if (!productExists || !warehouseExists)
                            throw new Exception("Invalid ProductId or WarehouseId");

                        var existing = await _repository.GetAsync(dto.ProductId, dto.WarehouseId);

                        if (existing == null)
                        {
                            await _repository.AddAsync(new Inventory
                            {
                                ProductId = dto.ProductId,
                                WarehouseId = dto.WarehouseId,
                                Quantity = dto.Quantity
                            });
                        }
                        else
                        {
                            existing.Quantity += dto.Quantity;
                            await _repository.UpdateAsync(existing);
                        }

                        // LOG
                        _context.InventoryTransactions.Add(new InventoryTransaction
                        {
                            ProductId = dto.ProductId,
                            FromWarehouseId = dto.WarehouseId,
                            ToWarehouseId = dto.WarehouseId,
                            Quantity = dto.Quantity,
                            Type = "ADD",
                            UserId = user!.Id
                        });

                        result.Success.Add(dto);
                    }
                    catch (Exception ex)
                    {
                        result.Failed.Add(new
                        {
                            dto.ProductId,
                            dto.WarehouseId,
                            dto.Quantity,
                            Error = ex.Message
                        });
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}