using MyClothingApp.Domain.Repositories;
using MyClothingApp.Infrastructure.Repositories;
using Web_site1.Domain.Entities;

namespace MyClothingApp.Domain.Services
{
    public class ProductWarehouseService : IProductWarehouseService
    {
        private readonly IProductWarehouseRepository _productWarehouseRepository;

        public ProductWarehouseService(IProductWarehouseRepository productWarehouseRepository)
        {
            _productWarehouseRepository = productWarehouseRepository;
        }

        public async Task<IEnumerable<ProductWarehouse>> GetAllProductWarehousesAsync()
        {
            return await _productWarehouseRepository.GetAllAsync();
        }

        public async Task<ProductWarehouse> GetProductWarehouseByIdAsync(int id)
        {
            return await _productWarehouseRepository.GetByIdAsync(id);
        }

        public async Task<ProductWarehouse> CreateProductWarehouseAsync(ProductWarehouse productWarehouse)
        {
            return await _productWarehouseRepository.CreateAsync(productWarehouse);
        }

        public async Task UpdateProductWarehouseAsync(ProductWarehouse productWarehouse)
        {
            await _productWarehouseRepository.UpdateAsync(productWarehouse);
        }

        public async Task DeleteProductWarehouseAsync(int id)
        {
            await _productWarehouseRepository.DeleteAsync(id);
        }
    }
}