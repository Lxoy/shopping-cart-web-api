using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCart.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Data.Configurations
{
    internal class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("cart_items");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Quantity)
                   .IsRequired();

            builder.HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);

            builder.HasOne(ci => ci.Article)
                .WithMany(a => a.CartItems)
                .HasForeignKey(ci => ci.ArticleId);

            builder.HasIndex(ci => new { ci.CartId, ci.ArticleId })
               .IsUnique();
        }
    }
}
