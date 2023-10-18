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
        public DbSet<Product> Product { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }

        public DbSet<CartDetail> CartDetails => Set<CartDetail>();
        public DbSet<CartTotals> CartTotals => Set<CartTotals>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Cart>().ToTable("Cart");
            modelBuilder.Entity<CartItem>().ToTable("CartItem");

            modelBuilder.Entity<Bundle>().ToTable("Bundle");
            modelBuilder.Entity<BundleItem>().ToTable("BundleItem");

            // stored procedure results
            modelBuilder.Entity<CartDetail>().HasNoKey();
            modelBuilder.Entity<CartTotals>().HasNoKey();
        }


    }
}
