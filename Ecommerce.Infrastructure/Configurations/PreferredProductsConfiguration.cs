using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations
{
    public class PreferredProductsConfiguration : IEntityTypeConfiguration<PreferredProducts>
    {
        public void Configure(EntityTypeBuilder<PreferredProducts> builder)
        {
            builder.HasKey(pp => new { pp.UserId, pp.ProductId });

            builder.Property(pp => pp.SavedAt)
                .IsRequired();

            // Many-to-one relationship with User
            builder.HasOne(pp => pp.User)
                .WithMany(u => u.PreferredProducts)  // Updated to use the new collection
                .HasForeignKey(pp => pp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-one relationship with Product
            builder.HasOne(pp => pp.Product)
                .WithMany(p => p.PreferredProducts)
                .HasForeignKey(pp => pp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
