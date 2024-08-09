using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Web_site1.Domain.Entities;

namespace Web_site1.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        // ... (другие DbSet для других моделей)

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products")
                .Property(p => p.ProductImageUrl)
                .HasMaxLength(255); //  Добавьте  этот  код

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Warehouse) // У товара один склад 
                .WithMany(w => w.Products) // У склада много товаров
                .HasForeignKey(p => p.WarehouseId) // Внешний ключ в таблице Products
                .OnDelete(DeleteBehavior.Cascade); // Действие при удалении склада (здесь - ограничение) 


            base.OnModelCreating(modelBuilder);
            Console.WriteLine("AppDbContext в норме");
        }
    }
}
