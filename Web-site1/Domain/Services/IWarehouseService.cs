using Web_site1.Domain.Entities;

namespace Web_site1.Domain.Services
{
    public interface IWarehouseService
    {
        Task<IEnumerable<Warehouse>> GetAllWarehousesAsync();
        Task<Warehouse> GetWarehouseByIdAsync(int id);
        Task<Warehouse> CreateWarehouseAsync(Warehouse warehouse);
        Task<Warehouse> UpdateWarehouseAsync(int id, Warehouse warehouse);
        Task DeleteWarehouseAsync(int id);
    }
}