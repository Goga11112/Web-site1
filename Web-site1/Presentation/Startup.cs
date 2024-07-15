using Microsoft.Extensions.Configuration;
using System.Runtime.Intrinsics.X86;
using Web_site1.Domain.Repositories;
using Web_site1.Domain.Services;
using Web_site1.Infrastructure.Data;

namespace Web_site1.Presentation
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //  .. (остальные    сервисы) 

            services.AddScoped<IProductRepository, Web_site1.Infrastructure.Repositories.ProductRepository>(); //  Правильный    "namespace",  убедитесь   что    импортировано    соответствующее  "using" 
            
            //  Внимательно   указывайте    пространство   имен
            services.AddScoped<IProductService, Web_site1.Application.Services.ProductService>(); //   Правильное  "namespace".  Проверьте    пространства   имен   `using`  в  `Startup.cs` 

            
}
    }
}
