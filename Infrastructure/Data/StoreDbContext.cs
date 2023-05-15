using Core.entites;
using Core.entites.OrderAggragate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        public  DbSet<Product> Products { get; set; } 

        public  DbSet<ProductBrand> ProductBrands { get; set; }

        public  DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<OrderItem> orderItems { get; set; }

        public DbSet<Order> orders { get; set; }

        public DbSet<DelivaryMethod> delivaryMethods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
