using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CGullProject;
using CGullProject.Models;

namespace CGullProject.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext (DbContextOptions<ShopContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Bundle > Bundle { get; set; }
        public DbSet<BundleItem> BundleItem { get; set; }

        public DbSet<Review> Review { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Inventory>().ToTable("Inventory");
            modelBuilder.Entity<Cart>().ToTable("Cart");
            modelBuilder.Entity<CartItem>().ToTable("CartItem");


            modelBuilder.Entity<Bundle>().ToTable("Bundle");
            modelBuilder.Entity<BundleItem>().ToTable("BundleItem");

            modelBuilder.Entity<Review>()
                .Property(c => c.Created)
                .HasDefaultValueSql("getdate()");


            modelBuilder.Entity<Review>()
              .Property(c => c.lastUpdated)
              .HasDefaultValueSql("getdate()");
                
            modelBuilder.Entity<Review>().ToTable("reviews");
        }


    }
}
