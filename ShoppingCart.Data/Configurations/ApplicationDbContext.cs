using Microsoft.EntityFrameworkCore;
using ShoppingCart.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Data.Configurations
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<Article> Articles => Set<Article>();
        public DbSet<CartItem> CartItems => Set<CartItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly
            );
        }
    }
}
