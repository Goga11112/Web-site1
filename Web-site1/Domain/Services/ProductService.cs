using Web_site1.Domain.Repositories;
using Web_site1.Domain.Entities;

namespace Web_site1.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() //получание Всех продуктов
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id) //получение по ID продукта    
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task CreateProductAsync(Product product)  //Создание продукта
        {
            await _productRepository.CreateAsync(product);
        }

        public async Task UpdateProductAsync(Product product)  //Изменение продукта
        {
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)  //удаление продукта
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}
