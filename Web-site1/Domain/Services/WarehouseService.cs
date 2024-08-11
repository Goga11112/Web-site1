using Microsoft.EntityFrameworkCore;
using Web_site1.Domain.Entities;
using Web_site1.Domain.Services;
using Web_site1.Infrastructure.Data;

public class WarehouseService : IWarehouseService
{
    private readonly AppDbContext _context;

    public WarehouseService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Warehouse>> GetAllWarehousesAsync()
    {
        return await _context.Warehouses.ToListAsync();
    }

    public async Task<Warehouse> GetWarehouseByIdAsync(int id)
    {
        return await _context.Warehouses
        .Include(w => w.ProductWarehouses) //  Добавить  Include  для  загрузки  ProductWarehouses
        .ThenInclude(pw => pw.Product) //  Добавить  ThenInclude  для  загрузки  Product
        .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<Warehouse> CreateWarehouseAsync(Warehouse warehouse)
    {
        _context.Warehouses.Add(warehouse);
        await _context.SaveChangesAsync();
        return warehouse;
    }

    public async Task<Warehouse> UpdateWarehouseAsync(int id, Warehouse warehouse)
    {
        var existingWarehouse = await _context.Warehouses.FindAsync(id);

        if (existingWarehouse == null)
        {
            return null; // Или можно выбросить исключение
        }

        // Обновление свойств склада
        existingWarehouse.Name = warehouse.Name;
        existingWarehouse.Address = warehouse.Address;
        // ... другие свойства

        await _context.SaveChangesAsync();
        return existingWarehouse;
    }

    public async Task DeleteWarehouseAsync(int id)
    {
        var warehouse = await _context.Warehouses.FindAsync(id);

        if (warehouse != null)
        {
            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
        }
    }
}