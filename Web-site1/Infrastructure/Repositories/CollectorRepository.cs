using Microsoft.EntityFrameworkCore;
using Web_site1.Domain.Entities;
using Web_site1.Domain.Repositories;
using Web_site1.Infrastructure.Data; // Add using for AppDbContext

namespace Web_site1.Infrastructure.Repositories
{
   
    public class CollectorRepository : IRepository<Collector>
    {
        private readonly AppDbContext _context;

        public CollectorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Collector>> GetAllAsync()
        {
            return await _context.Collectors.ToListAsync();
        }

        public async Task<Collector> GetByIdAsync(int id)
        {
            return await _context.Collectors.FindAsync(id);
        }

        public async Task CreateAsync(Collector Collector)
        {
            _context.Collectors.Add(Collector);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Collector Collector)
        {
            _context.Collectors.Update(Collector);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Collector = await _context.Collectors.FindAsync(id);
            if (Collector != null)
            {
                _context.Collectors.Remove(Collector);
                await _context.SaveChangesAsync();
            }
        }
    }
}
