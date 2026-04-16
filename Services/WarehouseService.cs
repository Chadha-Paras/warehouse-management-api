using warehouse_management_api.DTOs.Warehouse;
using warehouse_management_api.Models;
using warehouse_management_api.Repositories;

namespace warehouse_management_api.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _repository;

        public WarehouseService(IWarehouseRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<WarehouseResponseDto>> GetAllAsync()
        {
            var warehouses = await _repository.GetAllAsync();

            return warehouses.Select(w => new WarehouseResponseDto
            {
                Id = w.Id,
                Name = w.Name
            }).ToList();
        }

        public async Task<WarehouseResponseDto> CreateAsync(CreateWarehouseDto dto)
        {
            var warehouse = new Warehouse
            {
                Name = dto.Name
            };

            var created = await _repository.AddAsync(warehouse);

            return new WarehouseResponseDto
            {
                Id = created.Id,
                Name = created.Name
            };
        }
    }
}