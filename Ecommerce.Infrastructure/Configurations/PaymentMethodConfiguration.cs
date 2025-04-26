using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(pm => pm.Id);

            builder.Property(pm => pm.PaymentMethodName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pm => pm.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(pm => pm.CreatedAt)
                .IsRequired();

            builder.Property(pm => pm.UpdatedAt)
                .IsRequired();

            // One-to-many relationship with Orders
            builder.HasMany(pm => pm.Orders)
                .WithOne(o => o.PaymentMethod)
                .HasForeignKey(o => o.PaymentMethodId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
