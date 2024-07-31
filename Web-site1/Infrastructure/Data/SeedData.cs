using System;
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
        // В  этом  методе    добавляется    инициализационное    содержимое.
        //    Запускать  его  можно   при  первом  запуске   проекта (или  через    логику  в    `Startup.cs` ).
        //  метод,   используется   класс   `Product`,  также   реализуемый   вас    в   `Domain` (папка `Models` или   `Entities`).
        private static Product CreateProduct(string name, decimal price, string description, string size, string style, string ProductImageUrl)
        {
            return new Product()
            {
                Name = name,
                Price = price,
                Description = description,
                ProductImageUrl = ProductImageUrl,
                // НЕ указывайте "Size", "Type" и "Style", т.к. у вас нет соответствующего метода с этими параметрами
                // (Size, Type) - в классе "Product" свойства.
                // (Style) - в классе "Dress".
                // Вы должны создать разные методы CreateShirt, CreateDress или с одной параметрами и свойствами
            };
        }
        private static Product CreateDress(string name,decimal price, string description, string style, string ProductImageUrl)
        {
            return new Dress()
            {
                Name = name,
                Price = price,
                Description = description,
                Style = style,
                ProductImageUrl = ProductImageUrl,
                // НЕ указывайте "Size", "Type" и "Style", т.к. у вас нет соответствующего метода с этими параметрами
                // (Size, Type) - в классе "Product" свойства.
                // (Style) - в классе "Dress".
                // Вы должны создать разные методы CreateShirt, CreateDress или с одной параметрами и свойствами
            };
        }
        private static Product CreateSize(string name,decimal price, string description, string size, string ProductImageUrl)
        {
            return new Shirt()
            {
                Name = name,
                Price = price,
                Description = description,
                Size = size,
                ProductImageUrl = ProductImageUrl,
            };
        }
        // Метод   инициализации,   который   запускается    при    создании    базы    данных    
        public static async Task InitializeAsync(AppDbContext context)
        {
            if (await context.Products.AnyAsync() == false)
            {
                context.Products.AddRange(
                    new Shirt { Name = "Рубашка 1", Price = 19.99m, Description = "Классическая рубашка", Size = "M", ProductImageUrl = "/images/uploads/shirt1.jpg" },
                    new Dress { Name = "Платье 1", Price = 49.99m, Description = "Летнее платье", Style = "Casual", ProductImageUrl = "/images/uploads/dress1.jpg" },
                    new Shirt { Name = "Рубашка 2", Price = 24.99m, Description = "Поло", Size = "L", ProductImageUrl = "/images/uploads/shirt2.jpg" },
                    new Dress { Name = "Платье 2", Price = 59.99m, Description = "Вечернее платье", Style = "Elegant", ProductImageUrl = "/images/uploads/dress2.jpg" }
             );
                await context.SaveChangesAsync();
            }
        }
    }
}