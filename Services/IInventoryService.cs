using warehouse_management_api.DTOs.Inventory;

namespace warehouse_management_api.Services
{
    public interface IInventoryService
    {
        Task<InventoryResponseDto> AddOrUpdateStockAsync(CreateInventoryDto dto);
        Task TransferStockAsync(TransferStockDto dto);
        Task<BulkResponseDto> BulkAddOrUpdateAsync(List<BulkInventoryDto> items);
    }
}