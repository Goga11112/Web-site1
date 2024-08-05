using Microsoft.EntityFrameworkCore;
using Web_site1.Domain.Entities;
using Web_site1.Domain.Repositories;
using Web_site1.Infrastructure.Data; // Add using for AppDbContext

namespace Web_site1.Infrastructure.Repositories
{
    
    public class OrderCollectorRepository : IRepository<OrderCollector>
    {
        private readonly AppDbContext _context;

        public OrderCollectorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderCollector>> GetAllAsync()
        {
            return await _context.OrderCollectors.ToListAsync();
        }

        public async Task<OrderCollector> GetByIdAsync(int id)
        {
            return await _context.OrderCollectors.FindAsync(id);
        }

        public async Task CreateAsync(OrderCollector OrderCollector)
        {
            _context.OrderCollectors.Add(OrderCollector);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderCollector OrderCollector)
        {
            _context.OrderCollectors.Update(OrderCollector);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var OrderCollector = await _context.OrderCollectors.FindAsync(id);
            if (OrderCollector != null)
            {
                _context.OrderCollectors.Remove(OrderCollector);
                await _context.SaveChangesAsync();
            }
        }
    }
}
