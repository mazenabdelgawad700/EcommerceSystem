using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderId);

            builder.Property(o => o.UserId)
                .IsRequired();

            builder.Property(o => o.PaymentMethodId)
                .IsRequired();

            builder.Property(o => o.TotalPrice)
                .IsRequired()
                .HasPrecision(10, 2);

            builder.Property(o => o.PlacedAt)
                .IsRequired();

            // Many-to-one relationship with User - updated to use the new navigation property
            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-many relationship with OrderItems
            builder.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-one relationship with ShippingAddress
            builder.HasOne(o => o.ShippingAddress)
                .WithOne(sa => sa.Order)
                .HasForeignKey<ShippingAddress>(sa => sa.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-one relationship with Transaction
            builder.HasOne(o => o.Transaction)
                .WithOne(t => t.Order)
                .HasForeignKey<Transaction>(t => t.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-one relationship with PaymentMethod
            builder.HasOne(o => o.PaymentMethod)
                .WithMany(pm => pm.Orders)
                .HasForeignKey(o => o.PaymentMethodId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
