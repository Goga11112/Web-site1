using Web_site1.Domain.Entities;

namespace MyClothingApp.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }

    public interface IProductWarehouseRepository : IRepository<ProductWarehouse>
    {
        //  Дополнительные  методы  для  ProductWarehouse,  если  нужно
    }
}