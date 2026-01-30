using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCart.Data.Models;

namespace ShoppingCart.Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(45);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);


            builder.HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
