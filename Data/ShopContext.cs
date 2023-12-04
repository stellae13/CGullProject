using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CGullProject;
using CGullProject.Models;

namespace CGullProject.Data
{
    /// <summary>
    /// DbContext for the online store
    /// </summary>
    public class ShopContext : DbContext
    {
        public ShopContext (DbContextOptions<ShopContext> options) : base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Item> Inventory { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Bundle> Bundle { get; set; }
        public DbSet<BundleItem> BundleItem { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Item>().ToTable("Inventory");
            modelBuilder.Entity<Cart>().ToTable("Cart");
            modelBuilder.Entity<CartItem>().ToTable("CartItem");

            modelBuilder.Entity<Bundle>().ToTable("Bundle");
            modelBuilder.Entity<BundleItem>().ToTable("BundleItem");

            modelBuilder.Entity<Review>()
                .Property(c => c.Created)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Review>()
              .Property(c => c.LastUpdated)
              .HasDefaultValueSql("getdate()");
                
            modelBuilder.Entity<Review>().ToTable("Reviews");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItems");
        }
    }
}
