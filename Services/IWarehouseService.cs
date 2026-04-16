using warehouse_management_api.DTOs.Warehouse;

namespace warehouse_management_api.Services
{
    public interface IWarehouseService
    {
        Task<List<WarehouseResponseDto>> GetAllAsync();
        Task<WarehouseResponseDto> CreateAsync(CreateWarehouseDto dto);
    }
}