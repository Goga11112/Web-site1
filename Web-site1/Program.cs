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
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlServer("Server=(localdb)\\DbWeb-site1;Database=Web-site1;Trusted_Connection=True;MultipleActiveResultSets=true"));

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();  // ��������� ���������� REST API


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
       .AddEntityFrameworkStores<AppDbContext>()
       .AddDefaultTokenProviders();

builder.Services.AddScoped<IProductWarehouseService, ProductWarehouseService>();
builder.Services.AddScoped<IProductService, ProductService>();
//   �����������   �����������  (IProductRepository):
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductWarehouseRepository, ProductWarehouseRepository>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    // ��������� ��� ��������� ���� � ������ ������
    options.ViewLocationFormats.Insert(0, "/Presentation/Views/Product/{0}.cshtml");
    options.ViewLocationFormats.Insert(1, "/Presentation/Views/Shared/{0}.cshtml"); 
    options.ViewLocationFormats.Insert(2, "/Presentation/Views/Warehouse/{0}.cshtml");
    options.ViewLocationFormats.Insert(2, "/Presentation/Views/Account/{0}.cshtml");

});


var app = builder.Build();//������ �������� ������, ������ ��� ��� ���������
// ������������ ��������� ����� �������� ����������


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
// �������� �������  (��������������)    app.MapGet("/", () => "Hello World!"); 
app.MapControllers();  //   ������������� ���  API

app.UseAuthorization();

app.MapControllerRoute(
    name: "warehouses",
    pattern: "Warehouse/{action=Index}/{id?}",
    defaults: new { controller = "Warehouse" }); // �������� �������� �� ��� �����������!
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
