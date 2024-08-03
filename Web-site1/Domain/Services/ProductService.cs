using Microsoft.EntityFrameworkCore;
using Web_site1.Domain.Entities;
using Web_site1.Domain.Services;
using Web_site1.Infrastructure.Data; // Убедитесь, что пространство имен верное

namespace Web_site1.Domain.Services // Используйте то же пространство имен, что и интерфейс
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _dbContext; //  <- Предполагается, что у вас есть DbContext

        public ProductService(AppDbContext context)
        {
            _dbContext = context;
        }


        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
                Console.WriteLine("В IProductService все хорошо");
            
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task CreateProductAsync(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}