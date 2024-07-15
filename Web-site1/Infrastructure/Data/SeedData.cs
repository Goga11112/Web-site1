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
        private static Product CreateProduct(string name, decimal price, string description, string size, string style)
        {
            return new Product()
            {
                Name = name,
                Price = price,
                Description = description,
                // НЕ указывайте "Size", "Type" и "Style", т.к. у вас нет соответствующего метода с этими параметрами
                // (Size, Type) - в классе "Product" свойства.
                // (Style) - в классе "Dress".
                // Вы должны создать разные методы CreateShirt, CreateDress или с одной параметрами и свойствами
            };
        }
        private static Product CreateDress(string name, decimal price, string description, string style)
        {
            return new Dress()
            {
                Name = name,
                Price = price,
                Description = description,
                Style = style,
                // НЕ указывайте "Size", "Type" и "Style", т.к. у вас нет соответствующего метода с этими параметрами
                // (Size, Type) - в классе "Product" свойства.
                // (Style) - в классе "Dress".
                // Вы должны создать разные методы CreateShirt, CreateDress или с одной параметрами и свойствами
            };
        }
        private static Product CreateSize(string name, decimal price, string description, string size)
        {
            return new Shirt()
            {
                Name = name,
                Price = price,
                Description = description,
                Size = size,
            };
        }
        // Метод   инициализации,   который   запускается    при    создании    базы    данных    
        public static async Task InitializeAsync(AppDbContext context)
        {
            if (await context.Products.AnyAsync() == false)
            {
                context.Products.AddRange(
                 CreateProduct("Blue  T-shirt", 15, "A  basic blue  T-shirt.", "Medium", null),
                 CreateProduct("Black Dress", 50, "Simple    little  black    dress.", null, "Evening Dress"),
                 CreateDress("Чупаня", 100,"Хороший банана", "DevStyle")
             );
                await context.SaveChangesAsync();
            }
        }
    }
}