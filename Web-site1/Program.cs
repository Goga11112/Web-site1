using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using MyClothingApp.Domain.Repositories;
using MyClothingApp.Domain.Services;
using MyClothingApp.Infrastructure.Repositories;
using Web_site1.Domain.Entities;
using Web_site1.Domain.Repositories;
using Web_site1.Domain.Services;
using Web_site1.Infrastructure.Data;
using Web_site1.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPASSWORD = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var connectionString = $"Server={dbHost},1433; Database={dbName};User Id = sa; Password = {dbPASSWORD};Encrypt=True;TrustServerCertificate=True; ";
var connectionString1 = "Server=localhost,1433; Database=Web-site2; User Id = sa; Password = 191202G@v;Encrypt=True;TrustServerCertificate=True; ";


builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlServer(connectionString1));

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();  // Добавляем обработчик REST API


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
       .AddEntityFrameworkStores<AppDbContext>()
       .AddDefaultTokenProviders();

builder.Services.AddScoped<IProductWarehouseService, ProductWarehouseService>();
builder.Services.AddScoped<IProductService, ProductService>();
//   Регистрация   репозитория  (IProductRepository):
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductWarehouseRepository, ProductWarehouseRepository>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    // Добавляем ваш кастомный путь в начало списка
    options.ViewLocationFormats.Insert(0, "/Presentation/Views/Product/{0}.cshtml");
    options.ViewLocationFormats.Insert(1, "/Presentation/Views/Shared/{0}.cshtml"); 
    options.ViewLocationFormats.Insert(2, "/Presentation/Views/Warehouse/{0}.cshtml");
    options.ViewLocationFormats.Insert(2, "/Presentation/Views/Account/{0}.cshtml");

});


var app = builder.Build();//Всегда замыкает билдер, потому как его формирует
// Конфигурация маршрутов после создания приложения


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    //  Добавьте SeedData:
    SeedData.Initialize(services);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
// Тестовый маршрут  (необязательный)    app.MapGet("/", () => "Hello World!"); 
app.MapControllers();  //   Маршрутизация для  API

app.UseAuthorization();

app.MapControllerRoute(
    name: "warehouses",
    pattern: "Warehouse/{action=Index}/{id?}",
    defaults: new { controller = "Warehouse" }); // Обратите внимание на имя контроллера!
app.MapControllerRoute(
    name: "createWarehouse",
    pattern: "Warehouse/Create_w",
    defaults: new { controller = "Warehouse", action = "Create_w" });
app.MapControllerRoute(
    name: "createWarehouse",
    pattern: "Warehouse/Details_w",
    defaults: new { controller = "Warehouse", action = "Details_w" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");


app.Run();
