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
        public DbSet<ApplicationUserProduct> ApplicationUsersProducts { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

		public PCBuilderDbContext(DbContextOptions<PCBuilderDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUserProduct>()
                .HasKey(cr => new { cr.ApplicationUserId, cr.ProductId });
            
          
            modelBuilder.Entity<OrderProduct>()
	            .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderProduct>()
	            .HasOne(op => op.Order)
	            .WithMany(o => o.OrderProducts) 
	            .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
	            .HasOne(op => op.Product)
	            .WithMany(p => p.OrderProducts)  
	            .HasForeignKey(op => op.ProductId);


			modelBuilder.ApplyConfiguration(new ManufacturerEntityConfigurations());
            modelBuilder.ApplyConfiguration(new CategoryEntityConfigurations());
            modelBuilder.ApplyConfiguration(new ProductEntityConfigurations());
        }
    }
}
