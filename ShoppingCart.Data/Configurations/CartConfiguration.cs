using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCart.Data.Models;

namespace ShoppingCart.Data.Configurations
{
    internal class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("carts");

            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.User)
                   .WithOne(u => u.Cart)
                   .HasForeignKey<Cart>(c => c.UserId);

            builder.Property(c => c.CreatedAt)
               .IsRequired();

            builder.Property(c => c.ModifiedAt)
                .IsRequired();
        }
    }
}
