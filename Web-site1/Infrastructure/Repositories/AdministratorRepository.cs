using Microsoft.EntityFrameworkCore;
using Web_site1.Domain.Entities;
using Web_site1.Domain.Repositories;
using Web_site1.Infrastructure.Data; // Add using for AppDbContext

namespace Web_site1.Infrastructure.Repositories
{
    public class AdministratorRepository : IRepository<Administrator>
    {
        private readonly AppDbContext _context;

        public AdministratorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Administrator>> GetAllAsync()
        {
            return await _context.Administrators.ToListAsync();
        }

        public async Task<Administrator> GetByIdAsync(int id)
        {
            return await _context.Administrators.FindAsync(id);
        }

        public async Task CreateAsync(Administrator Administrator)
        {
            _context.Administrators.Add(Administrator);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Administrator Administrator)
        {
            _context.Administrators.Update(Administrator);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Administrator = await _context.Administrators.FindAsync(id);
            if (Administrator != null)
            {
                _context.Administrators.Remove(Administrator);
                await _context.SaveChangesAsync();
            }
        }
    }
}
