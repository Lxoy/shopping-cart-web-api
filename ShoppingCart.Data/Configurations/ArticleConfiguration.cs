using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCart.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Data.Configurations
{
    internal class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("articles");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Price)
                .HasColumnType("decimal(10,2)");
        }
    }
}
