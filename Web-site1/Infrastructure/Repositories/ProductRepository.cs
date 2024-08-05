using Microsoft.EntityFrameworkCore;
using Web_site1.Domain.Entities;
using Web_site1.Domain.Repositories;
using Web_site1.Infrastructure.Data; // Add using for AppDbContext
using Microsoft.EntityFrameworkCore;
using static Web_site1.Domain.Entities.ApplicationUser;

namespace Web_site1.Infrastructure.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        private decimal CalculateDiscount(ApplicationUser user)  //   Обновить   логику 
        {
            switch (user.Level)
            {
                case UserLevel.Bronze:
                    return user.PurchaseCount >= 5 ? 0.05m : 0; //   5%  при 5  или  более  покупок 
                case UserLevel.Silver:
                    return user.PurchaseCount >= 10 ? 0.10m : 0;  //  10% при  10  или  более   покупок 
                case UserLevel.Gold:
                    return 0.15m; //  15% скидка для   gold   users
                default:
                    return 0;
            }
        }
    }
}