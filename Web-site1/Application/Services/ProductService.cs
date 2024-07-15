using Web_site1.Domain.Entities; // Импортируем  model Product (и    прочие    модели, которые    будут   использоваться) 
using Web_site1.Domain.Repositories;
using Web_site1.Domain.Services; // Импортируем   репозитории  (например,  IProductRepository) 

namespace Web_site1.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository; // Поле   для    взаимодействия  с    репозиторием  

        // Конструктор,    инициализирующий   поля  
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        // Примеры  методов  (CRUD    операций):

        // 1.   Получение    списка  продуктов
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }


        // 2.   Получение   одного    продукта   по   ID
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }


        // 3.  Создание    нового   продукта
        public async Task CreateProductAsync(Product product)
        {
            await _productRepository.CreateAsync(product);
        }


        // 4.  Обновление  существующего  продукта
        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }


        // 5.   Удаление  продукта
        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}