using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Web_site1.Domain.Repositories;
using Web_site1.Domain.Services;
using Web_site1.Infrastructure.Data;
using Web_site1.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlServer("Server=(localdb)\\DbWeb-site1;Database=Products;Trusted_Connection=True;MultipleActiveResultSets=true"));

builder.Services.AddScoped<IProductService, ProductService>();
// 2.  Регистрация  вашего  репозитория  (IProductRepository):
builder.Services.AddScoped<IProductRepository, ProductRepository>();
// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    // Добавляем ваш кастомный путь в начало списка
    options.ViewLocationFormats.Insert(0, "/Presentation/Views/Product/{0}.cshtml");
    options.ViewLocationFormats.Insert(1, "/Presentation/Views/Shared/{0}.cshtml");
});


var app = builder.Build();//Всегда замыкает билдер, потому как его формирует


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
