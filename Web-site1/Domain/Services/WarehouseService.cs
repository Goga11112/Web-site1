using Microsoft.EntityFrameworkCore;
using Web_site1.Domain.Entities;
using Web_site1.Domain.Services;
using Web_site1.Infrastructure.Data;
using Web_site1.Presentation.Controllers;

public class WarehouseService : IWarehouseService
{
    private readonly AppDbContext _context;
    private readonly ILogger<WarehouseController> _logger;

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
        try
        {
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Создан новый склад: {@warehouse}", warehouse); // <--- Логирование
            return warehouse;
        }
        catch (DbUpdateException ex) // <--- Перехват исключений базы данных
        {
            _logger.LogError(ex, "Ошибка при создании склада: {@warehouse}", warehouse);
            throw; // Передаем исключение дальше для обработки в контроллере
        }
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
        existingWarehouse.Longitude = warehouse.Longitude;
        existingWarehouse.Latitude = warehouse.Latitude;
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