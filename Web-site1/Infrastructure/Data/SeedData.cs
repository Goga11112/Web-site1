using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web_site1.Domain.Entities;

namespace Web_site1.Infrastructure.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            // Проверка - если в базе данных уже есть данные
            if (context.Products.Any())
            {
                return; // Выходим, если есть данные
            }

            // Создание данных (Admin, Users, Products etc.)


            context.Warehouses.AddRange(
                new Warehouse { Name = "Central Warehouse", Address = "Central City" },
                new Warehouse { Name = "London Warehouse", Address = "London, UK" }
            );

            context.Products.AddRange(
                new Product
                {
                    Name = "T-Shirt",
                    Price = 10,
                    Description = "Classic T-Shirt, white cotton, large size",
                    ProductImageUrl = "/images/uploads/212511a5-499f-409c-8a57-8d924c5704af_dress2.jpg"
                },
                new Product
                {
                    Name = "Shoes",
                    Price = 50,
                    Description = "Sneakers, leather, blue color, size 10",
                    ProductImageUrl = "/images/uploads/3ee3d58c-7df9-4de4-bc20-91b3256ca328_dress1.jpg"
                },
                new Product
                {
                    Name = "Jacket",
                    Price = 100,
                    Description = "Windproof Jacket, black, medium size",
                    ProductImageUrl = "/images/uploads/7e0b3e3c-77c0-411a-ac15-d0e4391f5c51_shirt1.jpg"
                }
            );

            // (Create stock items for products and warehouses )
            context.ProductWarehouses.AddRange(
                new ProductWarehouse { ProductId = 1, WarehouseId = 1, Quantity = 10 }, // 10 T-shirts in Central Warehouse
                new ProductWarehouse { ProductId = 1, WarehouseId = 2, Quantity = 5 },  // 5 T-shirts in London Warehouse
                new ProductWarehouse { ProductId = 2, WarehouseId = 1, Quantity = 15 }, // 15 shoes in Central Warehouse
                new ProductWarehouse { ProductId = 3, WarehouseId = 1, Quantity = 2 }  // 2 jackets in Central Warehouse
            );

            context.SaveChanges(); // Сохранение изменений
        }
    }
}