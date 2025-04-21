using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations
{
    public class ShippingAddressConfiguration : IEntityTypeConfiguration<ShippingAddress>
    {
        public void Configure(EntityTypeBuilder<ShippingAddress> builder)
        {
            builder.HasKey(sa => sa.Id);  // Changed to use the new Id property

            builder.Property(sa => sa.UserId)
                .IsRequired();

            builder.Property(sa => sa.OrderId)
                .IsRequired();

            builder.Property(sa => sa.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(sa => sa.PostalCode)
                .IsRequired();

            builder.Property(sa => sa.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(sa => sa.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(sa => sa.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(sa => sa.Street)
                .IsRequired()
                .HasMaxLength(255);

            // One-to-one relationship with User
            builder.HasOne(sa => sa.User)
                .WithOne(u => u.ShippingAddress)
                .HasForeignKey<ShippingAddress>(sa => sa.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-one relationship with Order
            builder.HasOne(sa => sa.Order)
                .WithOne(o => o.ShippingAddress)
                .HasForeignKey<ShippingAddress>(sa => sa.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
