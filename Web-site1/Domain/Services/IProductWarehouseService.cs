using Web_site1.Domain.Entities;

namespace MyClothingApp.Domain.Services
{
    public interface IProductWarehouseService
    {
        Task<IEnumerable<ProductWarehouse>> GetAllProductWarehousesAsync();
        Task<ProductWarehouse> GetProductWarehouseByIdAsync(int id);
        Task<ProductWarehouse> CreateProductWarehouseAsync(ProductWarehouse productWarehouse);
        Task UpdateProductWarehouseAsync(ProductWarehouse productWarehouse);
        Task DeleteProductWarehouseAsync(int id);
    }
}