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
        private static Product CreateProduct(string name, int price, string description,  string ProductImageUrl)
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
        
       
        
    }
}