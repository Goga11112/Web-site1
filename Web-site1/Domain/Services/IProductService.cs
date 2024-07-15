using Web_site1.Domain.Entities;

namespace Web_site1.Domain.Services
{
    public interface IProductService  //Интерфейс для Сервиса продукта
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
