using warehouse_management_api.DTOs.Product;
using warehouse_management_api.Models;
using warehouse_management_api.Repositories;

namespace warehouse_management_api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductResponseDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();

            return products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }

        public async Task<ProductResponseDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name
            };

            var created = await _repository.AddAsync(product);

            return new ProductResponseDto
            {
                Id = created.Id,
                Name = created.Name
            };
        }
    }
}