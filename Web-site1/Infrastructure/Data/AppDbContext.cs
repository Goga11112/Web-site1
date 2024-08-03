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
            modelBuilder.Entity<Product>().ToTable("Products"); 

            modelBuilder.Entity<Product>().Property(p => p.ProductImageUrl).HasMaxLength(255); //  Добавьте  этот  код
            base.OnModelCreating(modelBuilder);
            Console.WriteLine("AppDbContext в норме");
        }
    }
}
