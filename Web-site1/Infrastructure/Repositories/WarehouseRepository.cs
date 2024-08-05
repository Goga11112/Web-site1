using Microsoft.EntityFrameworkCore;
using Web_site1.Domain.Entities;
using Web_site1.Domain.Repositories;
using Web_site1.Infrastructure.Data; // Add using for AppDbContext


namespace Web_site1.Infrastructure.Repositories
{ 

    public class WarehouseRepository : IRepository<Warehouse>
    {
        private readonly AppDbContext _context;

        public WarehouseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Warehouse>> GetAllAsync()
        {
            return await _context.Warehouses.ToListAsync();
        }

        public async Task<Warehouse> GetByIdAsync(int id)
        {
            return await _context.Warehouses.FindAsync(id);
        }

        public async Task CreateAsync(Warehouse Warehouse)
        {
            _context.Warehouses.Add(Warehouse);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Warehouse Warehouse)
        {
            _context.Warehouses.Update(Warehouse);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Warehouse = await _context.Warehouses.FindAsync(id);
            if (Warehouse != null)
            {
                _context.Warehouses.Remove(Warehouse);
                await _context.SaveChangesAsync();
            }
        }
    }
}
