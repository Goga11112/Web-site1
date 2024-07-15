using Web_site1.Domain.Services;

namespace Web_site1.Presentation
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductService, Web_site1.Domain.Services.ProductService>(); // Исправленное пространство имен
                                                                                                 // ... другие ваши сервисы
        }
    }
}
