using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using Web_site1.Domain.Entities;

namespace Web_site1.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<ProductWarehouse> ProductWarehouses { get; set; }
        // ... (другие DbSet для других моделей)

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductWarehouse>()
           .HasKey(pw => new { pw.ProductId, pw.WarehouseId });

            // ... (другие  конфигурации)

            // Relation Product - ProductWarehouse (one-to-many)
            modelBuilder.Entity<ProductWarehouse>()
                .HasOne(pw => pw.Product)
                .WithMany(p => p.ProductWarehouses)
                .HasForeignKey(pw => pw.ProductId)
                .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление при удалении продукта

            // Relation Warehouse - ProductWarehouse (one-to-many)
            modelBuilder.Entity<ProductWarehouse>()
                .HasOne(pw => pw.Warehouse)
                .WithMany(w => w.ProductWarehouses)
                .HasForeignKey(pw => pw.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);


            Console.WriteLine("AppDbContext в норме");
        }
    }
}
