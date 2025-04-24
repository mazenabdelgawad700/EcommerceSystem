using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // Composite primary key
            builder.HasKey(oi => oi.Id);

            builder.Property(o => o.ProductId)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(oi => oi.Quantity)
                .IsRequired();

            builder.Property(oi => oi.PiecePrice)
                .IsRequired()
                .HasPrecision(10, 2);

            builder.Property(oi => oi.AddedAt)
                .IsRequired();

            // Many-to-one relationship with Order
            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-one relationship with Product
            builder.HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
