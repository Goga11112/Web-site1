using Web_site1.Infrastructure.Repositories;
using Web_site1.Domain.Entities;
using MyClothingApp.Domain.Repositories;
using Web_site1.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MyClothingApp.Infrastructure.Repositories
{
    public class ProductWarehouseRepository : IProductWarehouseRepository
    {
        private readonly AppDbContext _context;

        public ProductWarehouseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductWarehouse>> GetAllAsync()
        {
            return await _context.ProductWarehouses.ToListAsync();
        }

        public async Task<ProductWarehouse> GetByIdAsync(int id)
        {
            return await _context.ProductWarehouses.FindAsync(id);
        }

        public async Task<ProductWarehouse> CreateAsync(ProductWarehouse productWarehouse)
        {
            _context.ProductWarehouses.Add(productWarehouse);
            await _context.SaveChangesAsync();
            return productWarehouse;
        }

        public async Task UpdateAsync(ProductWarehouse productWarehouse)
        {
            _context.ProductWarehouses.Update(productWarehouse);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var productWarehouse = await _context.ProductWarehouses.FindAsync(id);
            if (productWarehouse != null)
            {
                _context.ProductWarehouses.Remove(productWarehouse);
                await _context.SaveChangesAsync();
            }
        }
    }
}