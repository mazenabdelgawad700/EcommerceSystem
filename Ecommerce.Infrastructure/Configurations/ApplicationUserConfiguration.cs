using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // One-to-one relationship with Cart
            builder.HasOne(u => u.Cart)
                .WithOne()
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-one relationship with ShippingAddress
            builder.HasOne(u => u.ShippingAddress)
                .WithOne(sa => sa.User)
                .HasForeignKey<ShippingAddress>(sa => sa.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-many relationships
            builder.HasMany(u => u.Orders)
                .WithOne(o => o.User)  // Updated to reference the new navigation property
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-many relationships
            builder.HasMany(u => u.RefreshTokens)
                .WithOne(o => o.User)  // Updated to reference the new navigation property
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.RecentSearches)  // Fixed spelling
                .WithOne(rs => rs.User)
                .HasForeignKey(rs => rs.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.ProductSellers)  // Pluralized for consistency
                .WithOne(ps => ps.User)
                .HasForeignKey(ps => ps.SellerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Added missing collection configuration
            builder.HasMany(u => u.PreferredProducts)
                .WithOne(pp => pp.User)
                .HasForeignKey(pp => pp.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
