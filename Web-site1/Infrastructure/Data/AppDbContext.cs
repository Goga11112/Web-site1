using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Web_site1.Domain.Entities;
using Web_site1.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<OrderCollector> OrderCollectors { get; set; }
        public DbSet<Collector> Collectors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Order> Orders { get; set; }

        // ... (другие DbSet для других моделей)

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products"); 
            modelBuilder.Entity<Product>().Property(p => p.ProductImageUrl).HasMaxLength(255);


           //Stocks
            modelBuilder.Entity<Stock>()
                 .HasOne(s => s.Product)
                 .WithMany(p => p.Stocks)
                 .HasForeignKey(s => s.ProductID);

            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Warehouse)
                .WithMany(w => w.Stocks)
                .HasForeignKey(s => s.WarehouseID);
           //Users 
            modelBuilder.Entity<User>()
                .HasOne(u => u.Administrator)
                .WithOne(a => a.User)
                .HasForeignKey<Administrator>(a => a.UserID); // Relation with Administrator 

            modelBuilder.Entity<User>()
                .HasOne(u => u.Collector)
                .WithOne(c => c.User)
                .HasForeignKey<Collector>(c => c.UserID);  // Relation with Collector 
           //Other 
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserID);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.ProductID);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.OrderCollector)
                .WithOne(oc => oc.Order)
                .HasForeignKey<OrderCollector>(oc => oc.OrderID);
          //OtherCollector
            modelBuilder.Entity<OrderCollector>()
                .HasOne(oc => oc.Collector)
                .WithMany(c => c.OrderCollectors)
                .HasForeignKey(oc => oc.CollectorID);

            //  .... (other relationships: User and Administrator, Collector, Order etc) 

            // ... (Any additional settings for entities) 

            base.OnModelCreating(modelBuilder);
        }
    }
}
