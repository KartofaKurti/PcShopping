using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PcBuilder.Data.Models;
using System.Reflection;
using PcBuilder.Data.Configurations;

namespace PcBuilder.Data
{
    public class PCBuilderDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductsCategories { get; set; }

        public PCBuilderDbContext(DbContextOptions<PCBuilderDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductCategory>()
                .HasKey(cr => new { cr.ClientId, cr.ProductId });
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Manufacturer)
                .WithMany(m => m.Products)
                .HasForeignKey(p => p.ManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.ApplyConfiguration(new ManufacturerEntityConfigurations());
            modelBuilder.ApplyConfiguration(new CategoryEntityConfigurations());
            modelBuilder.ApplyConfiguration(new ProductEntityConfigurations());
        }
    }
}
