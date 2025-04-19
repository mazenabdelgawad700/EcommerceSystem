using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(d => d.Id);

            // Removed ProductId property configuration as it was removed from the class

            builder.Property(d => d.Percentage)
                .IsRequired()
                .HasPrecision(5, 2);

            builder.Property(d => d.StartDate)
                .IsRequired();

            builder.Property(d => d.EndDate)
                .IsRequired();

            // One-to-many relationship with Products - updated to use a direct foreign key
            builder.HasMany(d => d.Products)
                .WithOne(p => p.Discount)
                .HasForeignKey("DiscountId")  // Using shadow property for the foreign key
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
