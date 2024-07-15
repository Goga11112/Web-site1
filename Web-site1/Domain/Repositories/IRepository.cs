using Web_site1.Domain.Entities;

namespace Web_site1.Domain.Repositories
{
    //  Определение  интерфейса  "generic" 
    public interface IRepository<T> where T : class  //  универсальный  интерфейс
    {
        Task<IEnumerable<T>> GetAllAsync();  //  Общий  метод    для   получения    всех   элементов 
        Task<T> GetByIdAsync(int id); // Общий   метод    для   получения   элемента   по  ID
        Task CreateAsync(T entity);  // Общий   метод   для  создания   объекта   
        Task UpdateAsync(T entity);  // Общий   метод  для   обновления 
        Task DeleteAsync(int id);  //  Общий   метод  для  удаления 
    }
}